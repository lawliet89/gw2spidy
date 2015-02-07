using System.Collections.Generic;
using Gw2spidyApi.Extensions;

namespace Gw2spidyApi.Objects.Wrapper
{
    /// <summary>
    ///  A single result wrapper over an actual object
    /// </summary>
    public class ResultWrapper<T> : IWrapper<T>
    {
        public T Result { get; set; }

        public IEnumerable<T> Unwrap()
        {
            return Result.Yield();
        }
    }
}
