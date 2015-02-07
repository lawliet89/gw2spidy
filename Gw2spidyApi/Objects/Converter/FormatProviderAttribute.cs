using System;

namespace Gw2spidyApi.Objects.Converter
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class FormatProviderAttribute : Attribute
    {
        public abstract IFormatProvider Provider { get; }
    }
}
