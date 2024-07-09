using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;

namespace Way.EntityDB
{    
    public class DataItemJsonConverterAttribute : JsonConverterAttribute
    {
        static Type GenericType = typeof(DataItemTextJsonConverter<>);
        static Type BaseType = typeof(DataItem);
        public DataItemJsonConverterAttribute():base(null)
        {

        }
        public override JsonConverter CreateConverter(Type typeToConvert)
        {
            if (typeToConvert.IsSubclassOf(BaseType))
            {
                Type[] templateTypeSet = new[] { typeToConvert };

                Type implementType = GenericType.MakeGenericType(templateTypeSet);
                return (JsonConverter)Activator.CreateInstance(implementType);
            }
            else
            {
                return base.CreateConverter(typeToConvert);
            }
        }
    }

    class DataItemTextJsonConverter<T> : JsonConverter<T> where T : DataItem
    {
        static Type BaseType = typeof(DataItem);
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsSubclassOf(BaseType);
        }
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.TokenType == JsonTokenType.StartObject)
            {
                DataItem dataitem = (DataItem)Activator.CreateInstance(typeToConvert);
                dataitem.m_notSendPropertyChanged = true;

                //这种是自己去读reader
                string proName = null;
                while (reader.TokenType != JsonTokenType.EndObject)
                {
                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        proName = reader.GetString();
                        reader.Read();
                        var proInfo = typeToConvert.GetProperty(proName);
                        if(proInfo == null)
                            proInfo = typeToConvert.GetProperty(proName , BindingFlags.IgnoreCase |  BindingFlags.Public | BindingFlags.Instance);

                        if (proInfo == null)
                        {
                            if (reader.TokenType == JsonTokenType.StartObject)
                            {
                                int deepCount = 1;
                                while (true)
                                {
                                    reader.Read();
                                    if (reader.TokenType == JsonTokenType.EndObject)
                                    {
                                        deepCount--;
                                        if (deepCount == 0)
                                            break;
                                    }
                                    else if (reader.TokenType == JsonTokenType.StartObject)
                                    {
                                        deepCount++;
                                    }
                                }
                            }
                            else if (reader.TokenType == JsonTokenType.StartArray)
                            {
                                int deepCount = 1;
                                while (true)
                                {
                                    reader.Read();
                                    if (reader.TokenType == JsonTokenType.EndArray)
                                    {
                                        deepCount--;
                                        if (deepCount == 0)
                                            break;
                                    }
                                    else if (reader.TokenType == JsonTokenType.StartArray)
                                    {
                                        deepCount++;
                                    }
                                }
                            }
                            reader.Read();
                            continue;
                        }

                        try
                        {
                            var value = JsonSerializer.Deserialize(ref reader, proInfo.PropertyType, options);
                            if (value != null && proInfo != null && proInfo.CanWrite)
                                proInfo.SetValue(dataitem, value);
                        }
                        catch
                        {
                        }
                    }
                    reader.Read();
                }
                dataitem.m_notSendPropertyChanged = false;
                return (T)dataitem;
            }
            return null;
        }

        public override void Write(Utf8JsonWriter writer, T dataitem, JsonSerializerOptions options)
        {
            var properties = dataitem.GetType().GetTypeInfo().GetProperties();
            writer.WriteStartObject();
            foreach (var p in properties)
            {
                if (p.GetCustomAttribute(typeof(NotMappedAttribute)) != null && p.DeclaringType == typeof(DataItem))
                    continue;
                object pvalue = p.GetValue(dataitem);
                if (pvalue == null && options.DefaultIgnoreCondition.HasFlag(JsonIgnoreCondition.WhenWritingNull))
                    continue;
                writer.WritePropertyName(p.Name);

                JsonSerializer.Serialize(writer, pvalue, options);

            }
            if (dataitem.ChangedProperties.Count > 0)
            {
                writer.WritePropertyName("ChangedProperties");
                JsonSerializer.Serialize(writer, dataitem.ChangedProperties, options);
            }
            if (dataitem.BackupChangedProperties.Count > 0)
            {
                writer.WritePropertyName("BackupChangedProperties");
                JsonSerializer.Serialize(writer, dataitem.BackupChangedProperties, options);
            }
            writer.WriteEndObject();
        }
    }
}
