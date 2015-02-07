using Gw2spidyApi.Objects.Converter;

namespace Gw2spidyApi.Objects.Wrapper
{
    /// <summary>
    ///  A single result wrapper over an actual object
    /// </summary>
    public class ResultWrapper<T> : IWrapper<T>
    {
        public T Result { get; set; }

        public T Unwrap()
        {
            return Result;
        }
    }
}
