using NUnit.Framework;

namespace Gw2spidyApi.Objects.Wrapper
{
    class Results<T> : IWrapper<T[]>
    {
        public T[] results { get; set; }

        public T[] Unwrap()
        {
            return results;
        }
    }
}
