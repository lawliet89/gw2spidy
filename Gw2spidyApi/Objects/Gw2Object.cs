using System;
using System.Text;
using Gw2spidyApi.Objects.Wrapper;
using Gw2spidyApi.Requests;

namespace Gw2spidyApi.Objects
{
    public abstract class Gw2Object
    {
        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append(GetType().Name)
                .Append(":\n");

            // TODO: Not sure if this is the right way to get an object property
            foreach (var property in GetType().GetProperties())
            {
                const string propertyFormat = "{0}: {1}\n";
                result.Append(String.Format(propertyFormat, property.Name, property.GetValue(this, null)));
            }
            return result.ToString();
        }
    }
}
