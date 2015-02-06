namespace Gw2spidyApi.Objects.Wrapper
{
    /// <summary>
    ///  A single result wrapper over an actual object
    /// </summary>
    public class Result<T> : IWrapper<T>
    {
        public T result { get; set; }

        public T Unwrap()
        {
            return result;
        }
    }
}
