using Gw2spidyApi.Objects.Converter;

namespace Gw2spidyApi.Objects
{
    [ObjectConverter]
    public class Item : Gw2Object
    {
        public int data_id { get; set; }
        public string name { get; set; }
        public int rarity { get; set; }
        public int restriction_level { get; set; }
        public string img { get; set; }
        public int type_id { get; set; }
        public int sub_type_id { get; set; }
        public string price_last_changed { get; set; }
        public Currency max_offer_unit_price { get; set; }
        public Currency min_sale_unit_price { get; set; }
        public int offer_availability { get; set; }
        public int sale_availability { get; set; }
        public int sale_price_change_last_hour { get; set; }
        public int offer_price_change_last_hour { get; set; }
    }
}
