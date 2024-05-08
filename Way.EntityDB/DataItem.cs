using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Way.EntityDB.DataBaseService;

namespace Way.EntityDB
{
    public interface IDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        void SetValue(string columnName, object value);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        object GetValue(string columnName);
    }
    [System.ComponentModel.DataAnnotations.Schema.NotMapped()]
    public class DataValueChangedItem
    {
        public object OriginalValue
        {
            get;
            set;
        }
    }
    public interface IDataValueChanged
    {
        DataValueChangedItemCollection ChangedProperties
        {
            get;
        }
    }

    public class DataValueChangedItemCollection : Dictionary<string, DataValueChangedItem>
    {
        public DataValueChangedItem this[string key]
        {
            get
            {
                if (string.IsNullOrEmpty(key))
                    return null;
                if (base.ContainsKey(key))
                    return (DataValueChangedItem)base[key];
                else
                    return null;
            }
            set
            {
                if (base.ContainsKey(key) == false)
                {
                    base.Add(key , value);
                }
            }
        }

        /// <summary>
        /// 先清空自己，然后从source导入数据
        /// </summary>
        /// <param name="source">数据源</param>
        public void ImportData(DataValueChangedItemCollection source)
        {
            this.Clear();
            foreach( var item in source )
            {
                base[item.Key] = item.Value;
            }
        }
    }

   

    class DataItemConverter : JsonConverter
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
            foreach( var p in properties )
            {
                if (p.GetCustomAttribute(typeof(NotMappedAttribute)) != null)
                    continue;
                object pvalue = p.GetValue(dataitem);
                if (pvalue == null && serializer.NullValueHandling.HasFlag(NullValueHandling.Ignore))
                    continue;
                writer.WritePropertyName(p.Name);
                serializer.Serialize(writer, pvalue);

            }
            if(dataitem.ChangedProperties.Count > 0)
            {
                writer.WritePropertyName("ChangedProperties");
                serializer.Serialize(writer , dataitem.ChangedProperties);
            }
            if (dataitem.BackupChangedProperties.Count > 0)
            {
                writer.WritePropertyName("BackupChangedProperties");
                serializer.Serialize(writer, dataitem.BackupChangedProperties);
            }
            writer.WriteEndObject();
        }
    }

    [Newtonsoft.Json.JsonConverter(typeof(DataItemConverter))]
    public abstract class DataItem : IDataItem, INotifyPropertyChanging, INotifyPropertyChanged, IDataValueChanged
    {
        internal bool m_notSendPropertyChanged = false;
        internal object UpdateExpression;
        public static DateTime getdate()
        {
            return DateTime.Now;
        }

        /// <summary>把字段的更新，设置为一个指定的表达式值</summary>
        /// <param name="exp">指定的更新表达式，如 m=&gt;m.age == m.age + 1 &amp;&amp; name == name + "aa"，相当于sql语句的 age=age+1,name=name + 'aa'</param>
        public virtual void SetValue<T>(Expression<Func<T,bool>> exp)
        {
            if (UpdateExpression is Expression<Func<T, bool>> preExp && exp != null)
            {
                ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
                UpdateExpression = Expression.Lambda<Func<T, bool>>(
                   Expression.AndAlso(
                       preExp.Body,
                      exp.Body
                    ),
                    parameter
                );
            }
            else 
            {
                UpdateExpression = exp;
            }
        }

        public virtual void SetValue(string columnName, object value)
        {
            PropertyInfo pinfo = this.GetType().GetTypeInfo().GetProperty(columnName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase);
            if (pinfo == null)
                throw new Exception(this + "不存在属性" + columnName);
            if (value == null)
            {
                pinfo.SetValue(this, null, null);
                return;
            }
            Type itemType = pinfo.PropertyType;
            if (pinfo.PropertyType.GetTypeInfo().IsGenericType)
            {
                itemType = itemType.GetGenericArguments()[0];
            }
            if (itemType.GetTypeInfo().IsEnum)
            {
                if (value == null)
                    throw new Exception("Enum类型不能赋予null值");
                pinfo.SetValue(this, Enum.Parse(itemType, value.ToString()), null);
            }
            else
            {
                pinfo.SetValue(this, Convert.ChangeType(value, itemType), null);
            }
        }
        public static object GetValue(object obj, string columnName)
        {
            string[] columnNameArr = columnName.Split('.');
            object currentData = obj;

            for (int i = 0; i < columnNameArr.Length; i++)
            {
                System.Reflection.PropertyInfo pinfo = currentData.GetType().GetProperty(columnNameArr[i], System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase);
                if (pinfo == null)
                {
                    throw new Exception(string.Format(currentData.GetType().FullName + "找不到属性{0}" , columnNameArr[i]));
                }
                if (i == columnNameArr.Length - 1)
                {
                    return pinfo.GetValue(currentData);
                }
                else
                {
                    currentData = pinfo.GetValue(currentData);
                }
            }
            return null;
        }
        public virtual object GetValue(string columnName)
        {
            return GetValue(this, columnName);
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        DataValueChangedItemCollection _ChangedProperties = new DataValueChangedItemCollection();
        /// <summary>
        /// 被修改的属性的记录
        /// </summary>
        [NotMapped()]
        public DataValueChangedItemCollection ChangedProperties
        {
            get { return _ChangedProperties ; }
        }

        Type _tableType;
        [NotMapped()]
        internal Type TableType
        {
            get
            {
                if (_tableType == null)
                {
                   _tableType = this.GetType();
                }
                return _tableType;
            }
        }

        string _KeyName;
        [NotMapped()]
        internal virtual string KeyName
        {
            get
            {
                if (_KeyName == null)
                {
                    _KeyName = SchemaManager.GetSchemaTable(this.TableType).KeyColumn.PropertyName;
                }
                return _KeyName;
            }
        }

        string _tableName;
        [NotMapped()]
        internal virtual string TableName
        {
            get
            {
                if (_tableName == null)
                {
                    _tableName = SchemaManager.GetSchemaTable(this.TableType).TableName;
                }
                return _tableName;
            }
        }

        object _pkvalue;
             [NotMapped()]
        internal virtual object PKValue
        {
            get
            {
                if (_pkvalue == null)
                {
                    var schema = SchemaManager.GetSchemaTable(this.TableType);
                    if (schema.KeyColumn == null)
                        return null;

                    _pkvalue = schema.KeyColumn.PropertyInfo.GetValue(this);
                }
                return _pkvalue;
            }
        }

      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isInsert"></param>
        /// <param name="insertAllField">是否insert所有字段，包括自增长字段</param>
        /// <returns></returns>
        internal virtual List<FieldValue> GetFieldValues(bool isInsert,bool insertAllField)
        {
            List<FieldValue> fields = new List<FieldValue>();
            var tableSchema = SchemaManager.GetSchemaTable(this.GetType());

            if (isInsert)
            {

                foreach (var column in tableSchema.Columns)
                {
                    if (insertAllField == false && column.IsDatabaseGenerated)
                        continue;
                    object value = column.PropertyInfo.GetValue(this);
                    if (value == null)
                        continue;

                    var typeinfo = column.PropertyInfo.PropertyType.GetTypeInfo();
                    if (typeinfo.IsEnum)
                        value = Convert.ToInt32(value);
                    else if (typeinfo.IsGenericType && typeinfo.GetGenericArguments()[0].GetTypeInfo().IsEnum)
                        value = Convert.ToInt32(value);


                    fields.Add(new FieldValue()
                    {
                        FieldName = column.Name,
                        Value = value,
                    });
                }
            }
            else
            {
                foreach (var changeItem in this.ChangedProperties)
                {
                    var column = tableSchema.Columns.FirstOrDefault(m=>m.PropertyName == changeItem.Key);

                    if (column == null)
                        continue;


                    object value = column.PropertyInfo.GetValue(this);
                    if (value != null)
                    {
                        var typeinfo = column.PropertyInfo.PropertyType.GetTypeInfo();
                        if (typeinfo.IsEnum)
                            value = Convert.ToInt32(value);
                        else if (typeinfo.IsGenericType && typeinfo.GetGenericArguments()[0].GetTypeInfo().IsEnum)
                            value = Convert.ToInt32(value);
                    }
                    fields.Add(new FieldValue()
                    {
                        FieldName = column.Name,
                        Value = value,
                    });
                }
            }
            return fields;
        }

        DataValueChangedItemCollection _BackupChangedProperties = new DataValueChangedItemCollection();
        /// <summary>
        /// 可以用来手动备份ChangedProperties
        /// </summary>
        [NotMapped()]
        public DataValueChangedItemCollection BackupChangedProperties
        {
            get { return _BackupChangedProperties; }
            set
            {
                _BackupChangedProperties = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void SendPropertyChanging(String propertyName  , object originalValue,object nowvalue)
        {
            if (m_notSendPropertyChanged)
                return;

            DataValueChangedItem changeditem = this.ChangedProperties[propertyName];
            if (changeditem != null)
            {
                if (changeditem.OriginalValue == null && nowvalue == null)
                {
                    ChangedProperties.Remove(propertyName);
                }
                else if (changeditem.OriginalValue != null && changeditem.OriginalValue.Equals(nowvalue))
                {
                    ChangedProperties.Remove(propertyName);
                }
            }
            else
            {
                this.ChangedProperties[propertyName] = new DataValueChangedItem()
                {
                    OriginalValue = originalValue,
                };
            }
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
      
        /// <summary>
        /// 把当前的值赋给指定的目标
        /// </summary>
        /// <param name="target"></param>
        /// <param name="copyNullValue">是否拷贝null值</param>
        /// <param name="copyPkValue">是否拷贝主键值</param>
        public void CopyValueTo(DataItem target,bool copyNullValue, bool copyPkValue = true)
        {
          
            var targetType = target.GetType();
            string targetKeyName = null;
            if (!copyPkValue)
            {
                targetKeyName = SchemaManager.GetSchemaTable(targetType).KeyColumn.PropertyName;
            }

            var properties = this.TableType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(m => m.GetCustomAttribute<System.ComponentModel.DataAnnotations.Schema.ColumnAttribute>() != null);
            foreach( var pro in properties )
            {
                try
                {
                    if(copyPkValue == false && targetKeyName == pro.Name)
                    {
                        continue;
                    }

                    var myval = pro.GetValue(this);
                    if (!copyNullValue && myval == null)
                        continue;

                    if (targetType == this.TableType)
                    {
                        pro.SetValue(target, myval);
                    }
                    else
                    {
                        var targetPro = targetType.GetProperty(pro.Name);
                        if (targetPro != null)
                        {
                            var att = targetPro.GetCustomAttribute<DatabaseGeneratedAttribute>();
                            if (att != null && att.DatabaseGeneratedOption != DatabaseGeneratedOption.None)
                                continue;
                            target.SetValue(pro.Name, myval);
                        }
                    }
                }
                catch 
                { 
                }
            }
        }

        public object Clone()
        {
            Type myType = this.GetType();
            TypeInfo myTypeInfo = myType.GetTypeInfo();
            var newObj = (IDataItem)Activator.CreateInstance(myType);
            var values = this.GetFieldValues(true,false);
            foreach (var field in values)
            {
                newObj.SetValue(field.FieldName, field.Value);
            }
            ((DataItem)newObj).ChangedProperties.Clear();
            return newObj;
        }
        /// <summary>
        /// 回滚当前对象的所有更改
        /// </summary>
        public void Rollback()
        {
            m_notSendPropertyChanged = true;
            try
            {
                Type thisType = this.GetType();
                foreach (var changeItem in this.ChangedProperties)
                {
                    thisType.GetProperty(changeItem.Key).SetValue(this, changeItem.Value.OriginalValue);
                }
                this.ChangedProperties.Clear();
            }
            catch
            {

            }
            m_notSendPropertyChanged = false;
        }
    }

    class FieldValue
    {
        public string FieldName;
        public object Value;
    }
}
