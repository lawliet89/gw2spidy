using System;
using System.Linq;
using Gw2spidyApi.Objects;
using Gw2spidyApi.Objects.Wrapper;

namespace Gw2spidyApi.Requests
{
    public class ItemRequest : Request<Item, ResultWrapper<Item>>
    {
        private string Path = "/item/{0}";

        [CachePrameter]
        public int ItemId { get; private set; }

        public ItemRequest(int itemId)
        {
            ItemId = itemId;
        }

        public override Uri RequestUri
        {
            get
            {
                var builder = ApiUrlBuilder;
                builder.Path += String.Format(Path, ItemId);
                return builder.Uri;
            }
        }

        public override CacheObject<ResultWrapper<Item>> FindInCache()
        {
            // First find it in all items cache
            var cached = AllItemsRequest.Cache
                .Where(c => c.Parameters.SequenceEqual(AllItemsRequest.AllCacheParameters))
                .OrderByDescending(c => c.Timestamp)
                .Select(c => c.Result.Unwrap())
                .FirstOrDefault();
            if (cached != null)
            {
                var item = cached.SingleOrDefault(i => i.DataId == ItemId);
                if (item != null) 
                   return MakeCacheWrapper(new ResultWrapper<Item>() {Result = item});
            }
            return base.FindInCache();
        }
    }
}
