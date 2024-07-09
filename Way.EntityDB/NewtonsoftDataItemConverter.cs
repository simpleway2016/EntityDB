using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Way.EntityDB
{
    class NewtonsoftDataItemConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }
        public override bool CanRead
        {
            get
            {
                return true;
            }
        }
        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                DataItem dataitem = (DataItem)Activator.CreateInstance(objectType);
                dataitem.m_notSendPropertyChanged = true;
                //让serializer去读reader
                serializer.Populate(reader, dataitem);


                //这种是自己去读reader
                //string proName = null;
                //while (reader.TokenType != JsonToken.EndObject)
                //{
                //    if(reader.TokenType == JsonToken.PropertyName)
                //    {
                //        proName = reader.Value.ToString();
                //    }
                //    else if(reader.Value != null)
                //    {

                //    }
                //    reader.Read();
                //}
                dataitem.m_notSendPropertyChanged = false;
                return dataitem;
            }
            else
                return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DataItem dataitem = (DataItem)value;
            var properties = value.GetType().GetTypeInfo().GetProperties();
            writer.WriteStartObject();
            foreach (var p in properties)
            {
                if (p.GetCustomAttribute(typeof(NotMappedAttribute)) != null && p.DeclaringType == typeof(DataItem))
                    continue;
                object pvalue = p.GetValue(dataitem);
                if (pvalue == null && serializer.NullValueHandling.HasFlag(NullValueHandling.Ignore))
                    continue;
                writer.WritePropertyName(p.Name);
                serializer.Serialize(writer, pvalue);

            }
            if (dataitem.ChangedProperties.Count > 0)
            {
                writer.WritePropertyName("ChangedProperties");
                serializer.Serialize(writer, dataitem.ChangedProperties);
            }
            if (dataitem.BackupChangedProperties.Count > 0)
            {
                writer.WritePropertyName("BackupChangedProperties");
                serializer.Serialize(writer, dataitem.BackupChangedProperties);
            }
            writer.WriteEndObject();
        }
    }
}
