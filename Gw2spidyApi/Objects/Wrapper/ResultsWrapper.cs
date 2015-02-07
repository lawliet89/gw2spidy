using System.Collections.Generic;

namespace Gw2spidyApi.Objects.Wrapper
{
    public class ResultsWrapper<T> : IWrapper<T>
    {
        public int Count { get; set; }
        public List<T> Results { get; set; }

        public IEnumerable<T> Unwrap()
        {
            return Results;
        }
    }
}
