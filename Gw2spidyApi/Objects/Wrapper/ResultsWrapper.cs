﻿using System.Collections.Generic;

namespace Gw2spidyApi.Objects.Wrapper
{
    public class ResultsWrapper<T> : IWrapper<T>
        where T : Gw2Object
    {
        public int Count { get; set; }
        public IEnumerable<T> Results { get; set; }

        public IEnumerable<T> Unwrap()
        {
            return Results;
        }
    }
}
