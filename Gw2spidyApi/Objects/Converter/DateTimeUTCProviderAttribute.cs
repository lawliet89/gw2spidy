using System;
using System.Globalization;

namespace Gw2spidyApi.Objects.Converter
{
    public class DateTimeUtcProviderAttribute : FormatProviderAttribute
    {
        public override IFormatProvider Provider
        {
            get
            {
                const string format = @"yyyy-MM-dd HH\:mm\:ss UTC";
                return new DateTimeFormatInfo
                {
                    FullDateTimePattern = format
                };
            }
        }
    }
}
