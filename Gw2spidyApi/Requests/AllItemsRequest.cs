using System;
using System.Collections.Generic;
using Gw2spidyApi.Objects;
using Gw2spidyApi.Objects.Wrapper;

namespace Gw2spidyApi.Requests
{
    public class AllItemsRequest : Request<Item, ResultsWrapper<Item>>
    {
        private const string Path = "/all-items/{0}";
        private const string All = "all";

        [CachePrameter]
        public string Type { get; private set; }

        public AllItemsRequest() : this(All)
        {
            
        }

        public AllItemsRequest(int type) : this(type.ToString())
        {
            
        }

        public AllItemsRequest(string type)
        {
            Type = type;
            // Don't recommend you use this request....
            JavaScriptSerializer.MaxJsonLength = 20000000; 
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

        public static IDictionary<string, object> AllCacheParameters
        {
            get
            {
                var dict = new Dictionary<string, object>();
                dict["Type"] = All;
                return dict;
            }
        } 
    }
}
