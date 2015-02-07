using System;
using System.Globalization;

namespace Gw2spidyApi.Objects.Converter
{
    public class DateTimeUtcProviderAttribute : FormatProviderAttribute
    {
        public override object Preprocess(object obj)
        {
            return obj.ToString().Replace("UTC", String.Empty);
        }

        public override IFormatProvider Provider
        {
            get
            {
                const string format = @"yyyy-MM-dd HH\:mm\:ss";
                return new DateTimeFormatInfo
                {
                    FullDateTimePattern = format
                };
            }
        }
    }
}
