using System;
using Gw2spidyApi.Objects.Converter;

namespace Gw2spidyApi.Objects
{
    [ObjectConverter]
    public class Item : Gw2Object
    {
        public int DataId { get; set; }
        public string Name { get; set; }
        public int Rarity { get; set; }
        public int RestrictionLevel { get; set; }
        public string Img { get; set; }
        public int TypeId { get; set; }
        public int SubTypeId { get; set; }
        [DateTimeUtcProvider]
        public DateTime PriceLastChanged { get; set; }
        public Currency MaxOfferUnitPrice { get; set; }
        public Currency MinSaleUnitPrice { get; set; }
        public int OfferAvailability { get; set; }
        public int SaleAvailability { get; set; }
        public int SalePriceChangeLastHour { get; set; }
        public int OfferPriceChangeLastHour { get; set; }
    }
}
