using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;
using Gw2spidyApi.Extensions;

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
                var value = dictionary[property.Name.ToSnakeCase()];
                var destinationType = property.PropertyType;
                var sourceType = value.GetType();

                object valueToSet;
                if (destinationType == sourceType)
                {
                    valueToSet = value;
                }
                // Check we can use Convert
                else if (sourceType.GetInterfaces().Contains(typeof (IConvertible))
                         && destinationType.IsValueType)
                {
                    // Get FormatProvider, if it exists
                    var attribute = property.GetCustomAttributes(typeof(FormatProviderAttribute), true)
                        .SingleOrDefault() as FormatProviderAttribute;
                    IFormatProvider provider = null;
                    if (attribute != null)
                    {
                        provider = attribute.Provider;
                    }
                    valueToSet = Convert.ChangeType(value, destinationType, provider);
                }
                else if (destinationType.GetConstructor(new[] {sourceType}) != null)
                {
                    var constructor = destinationType.GetConstructor(new[] {sourceType});
                    valueToSet = constructor.Invoke(new object[] {value});
                }
                else
                {
                    throw new InvalidOperationException("Unable to bind property");
                }
                property.SetValue(result, valueToSet, null);
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
                   && type.GetGenericTypeDefinition() == typeof(Dictionary<,>);
        }
    }
}
