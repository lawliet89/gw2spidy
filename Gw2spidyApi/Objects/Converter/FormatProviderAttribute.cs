using System;

namespace Gw2spidyApi.Objects.Converter
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class FormatProviderAttribute : Attribute
    {
        public virtual object Preprocess(object obj)
        {
            return obj;
        }
        public abstract IFormatProvider Provider { get; }
    }
}
