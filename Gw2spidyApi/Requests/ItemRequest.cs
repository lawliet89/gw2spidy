using System;
using Gw2spidyApi.Network;
using Gw2spidyApi.Objects;
using Gw2spidyApi.Objects.Wrapper;

namespace Gw2spidyApi.Requests
{
    public class ItemRequest : Request<Item, Result<Item>>
    {
        private string Path = "/item/{0}";
        public int ItemId { get; private set; }

        public ItemRequest(int itemId) : this(itemId, new HttpRequest())
        {
        }
        public ItemRequest(int itemId, IHttpRequest httpRequest) : base(httpRequest)
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
    }
}
