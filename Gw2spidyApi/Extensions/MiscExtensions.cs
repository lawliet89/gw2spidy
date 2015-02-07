using System.Collections.Generic;

namespace Gw2spidyApi.Extensions
{
    public static class MiscExtensions
    {
        public static IEnumerable<T> Yield<T>(this T obj)
        {
            yield return obj;
        }
    }
}
