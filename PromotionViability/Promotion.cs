using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gw2spidyApi.Extensions;
using Gw2spidyApi.Objects;
using Gw2spidyApi.Requests;

namespace PromotionViability
{
    class Promotion
    {
        public static int Tier5PromotionYield = 5;

        public ItemRequest Promoted;
        public int QuantityYield;
        public Dictionary<ItemRequest, int> Ingredients;

        private bool Sent = false;
        private List<Task> Tasks = new List<Task>(); 

        public void Load()
        {
            if (Sent) return;
            Tasks.Add(Promoted.Send());
            Tasks.AddRange(Ingredients.Select(pair => pair.Key.Send()));
            Sent = true;
        }

        public void WaitAll()
        {
            Load();
            Task.WaitAll(Tasks.ToArray());
        }

        public string Name
        {
            get
            {
                WaitAll();
                return Promoted.Result.Single().Name;
            }
        }

        public Currency CostOfIngridients
        {
            get
            {
                WaitAll();
                return Ingredients.Select(pair => pair.Key.Result.Single().MaxOfferUnitPrice*pair.Value)
                    .Aggregate((sum, itemCost) => sum + itemCost);
            }
        }

        public Currency ProfitOfProduct
        {
            get
            {
                WaitAll();
                return Currency.ProfitSellingAt(Promoted.Result.Single()
                    .MinSaleUnitPrice*QuantityYield);
            }
        }

        public Currency ProfitOfPromotion
        {
            get
            {
                WaitAll();
                return ProfitOfProduct - CostOfIngridients;
            }
        }

        public bool Profitable
        {
            get
            {
                WaitAll();
                return ProfitOfPromotion > 0;
            }
        }
    }
}
