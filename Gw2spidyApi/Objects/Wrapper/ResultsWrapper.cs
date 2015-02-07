using System;
using NUnit.Framework;

namespace Gw2spidyApi.Objects.Wrapper
{
    public class ResultsWrapper<T> : IWrapper<T>
    {
        public int Count { get; set; }
        public T Results { get; set; }

        public T Unwrap()
        {
            return Results;
        }
    }
}
