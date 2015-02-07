using System;
using Gw2spidyApi.Objects;
using Gw2spidyApi.Objects.Wrapper;

namespace Gw2spidyApi.Requests
{
    public class ItemsRequest : Request<Item, ResultsWrapper<Item>>
    {
        private const string Path = "/items/{0}";
        private const string All = "all";

        public string Type { get; private set; }
        public ItemsRequest() : this(All)
        {
            
        }

        public ItemsRequest(int type) : this(type.ToString())
        {
            
        }

        public ItemsRequest(string type)
        {
            Type = type;
        }

        public override Uri RequestUri
        {
            get
            {
                var builder = ApiUrlBuilder;
                builder.Path += String.Format(Path, Type);
                return builder.Uri;
            }
        }
    }
}
