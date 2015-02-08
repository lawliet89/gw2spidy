using System;
using System.Collections.Generic;
using System.Linq;
using Gw2spidyApi.Requests;

namespace PromotionViability
{
    internal class Program
    {
        public static IEnumerable<Promotion> Promotions
        {
            get
            {
                // Ancient bone
                yield return new Promotion
                {
                    Promoted = new ItemRequest(ItemIds.AncientBone),
                    QuantityYield = Promotion.Tier5PromotionYield,
                    Ingredients = new Dictionary<ItemRequest, int>
                    {
                        {new ItemRequest(ItemIds.AncientBone), 1},
                        {new ItemRequest(ItemIds.LargeBone), 50},
                        {new ItemRequest(ItemIds.CrystallineDust), 5}
                    }
                };
            }
        }

        private static void Main(string[] args)
        {
            // Get all requests
            var allItems = new AllItemsRequest();
            allItems.Get();
            foreach (var promotion in Promotions)
            {
                Console.WriteLine("Promoting {0}:", promotion.Name);
                Console.WriteLine("\tCost of ingredients: {0}", promotion.CostOfIngridients);
                Console.WriteLine("\tProfit from selling an average yield of {0}: {1}", 
                    promotion.QuantityYield, promotion.ProfitOfProduct);
                Console.WriteLine("\tProfit overall: {0}", promotion.ProfitOfPromotion);
                Console.WriteLine("\tVerdict: {0}", promotion.Profitable ? "Profitable" : "Don't bother");
            }

            // Keep the console window open in debug modde.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
