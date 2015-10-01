using System.Collections.Generic;
using System.Reflection;
using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace RESerializable
{
    public static class SerializeFunction
    {
        public static Dictionary<string, object> Serialize<T>(this T toSerialize)
        {
            Dictionary<string, object> dataDictionary = new Dictionary<string, object>();
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                dataDictionary.Add(propertyInfo.Name, propertyInfo.GetValue(toSerialize, null));
            }
            return dataDictionary;
        }

        public static T Deserialize<T>(this Dictionary<string, object> toDeserialize) where T : new()
        {
            T result = new T();
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                if (propertyInfo.PropertyType.IsEnum)
                {
                    propertyInfo.SetValue(result, Enum.Parse(propertyInfo.PropertyType, toDeserialize[propertyInfo.Name].ToString()), null);
                }
                else
                {
                    propertyInfo.SetValue(result, Convert.ChangeType(toDeserialize[propertyInfo.Name], propertyInfo.PropertyType), null);
                }
            }
            return result;
        }
    }
}
