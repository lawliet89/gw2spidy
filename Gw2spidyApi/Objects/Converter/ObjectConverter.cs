using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Gw2spidyApi.Objects.Converter
{
    public class ObjectConverter : JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type,
            JavaScriptSerializer serializer)
        {
            var result = Activator.CreateInstance(type);
            foreach (var property in type.GetProperties())
            {
                var value = dictionary[property.Name];
                var destinationType = property.PropertyType;
                var sourceType = value.GetType();

                if (destinationType == sourceType)
                {
                    property.SetValue(result, value, null);
                }
                else if (IsDictionary(sourceType))
                {
                    property.SetValue(result,
                        Deserialize((IDictionary<string, object>) value, destinationType, serializer),
                        null);
                }
                else if (destinationType.GetConstructor(new[] {sourceType}) != null)
                {
                    var constructor = destinationType.GetConstructor(new[] {sourceType});
                    property.SetValue(result,
                        constructor.Invoke(new object[] {value}),
                        null);
                }
                else
                {
                    throw new InvalidOperationException("Unable to bind property");
                }
            }

            return result;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
                return Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => t.GetCustomAttributes(typeof (ObjectConverterAttribute), true).Length > 0);
            }
        }

        private bool IsDictionary(Type type)
        {
            return type.IsGenericType
                   && type.GetGenericTypeDefinition() == typeof (Dictionary<,>);
        }
    }
}
