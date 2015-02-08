using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Gw2spidyApi.Requests;

namespace PromotionViability
{
    internal class Program
    {
        public static IEnumerable<Promotion> Promotions
        {
            get
            {
                // Dust
                yield return new Promotion
                {
                    Promoted = new ItemRequest(ItemIds.CrystallineDust),
                    QuantityYield = 23,
                    Ingredients = new Dictionary<ItemRequest, int>
                    {
                        {new ItemRequest(ItemIds.IncandescentDust), 250},
                        {new ItemRequest(ItemIds.CrystallineDust), 1}
                    }
                };
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
                // Claws
                yield return new Promotion
                {
                    Promoted = new ItemRequest(ItemIds.ViciousClaw),
                    QuantityYield = Promotion.Tier5PromotionYield,
                    Ingredients = new Dictionary<ItemRequest, int>
                    {
                        {new ItemRequest(ItemIds.ViciousClaw), 1},
                        {new ItemRequest(ItemIds.LargeClaws), 50},
                        {new ItemRequest(ItemIds.CrystallineDust), 5}
                    }
                };
                // Fangs
                yield return new Promotion
                {
                    Promoted = new ItemRequest(ItemIds.ViciousFang),
                    QuantityYield = Promotion.Tier5PromotionYield,
                    Ingredients = new Dictionary<ItemRequest, int>
                    {
                        {new ItemRequest(ItemIds.ViciousFang), 1},
                        {new ItemRequest(ItemIds.LargeFang), 50},
                        {new ItemRequest(ItemIds.CrystallineDust), 5}
                    }
                };
                // Scales
                yield return new Promotion
                {
                    Promoted = new ItemRequest(ItemIds.ArmoredScale),
                    QuantityYield = Promotion.Tier5PromotionYield,
                    Ingredients = new Dictionary<ItemRequest, int>
                    {
                        {new ItemRequest(ItemIds.ArmoredScale), 1},
                        {new ItemRequest(ItemIds.LargeScale), 50},
                        {new ItemRequest(ItemIds.CrystallineDust), 5}
                    }
                };
                // Totems
                yield return new Promotion
                {
                    Promoted = new ItemRequest(ItemIds.ElaborateTotem),
                    QuantityYield = Promotion.Tier5PromotionYield,
                    Ingredients = new Dictionary<ItemRequest, int>
                    {
                        {new ItemRequest(ItemIds.ElaborateTotem), 1},
                        {new ItemRequest(ItemIds.IntricateTotem), 50},
                        {new ItemRequest(ItemIds.CrystallineDust), 5}
                    }
                };
                // Venom sacs
                yield return new Promotion
                {
                    Promoted = new ItemRequest(ItemIds.PowerfulVenomSac),
                    QuantityYield = Promotion.Tier5PromotionYield,
                    Ingredients = new Dictionary<ItemRequest, int>
                    {
                        {new ItemRequest(ItemIds.PowerfulVenomSac), 1},
                        {new ItemRequest(ItemIds.PotentVenomSac), 50},
                        {new ItemRequest(ItemIds.CrystallineDust), 5}
                    }
                };
                // Blood
                yield return new Promotion
                {
                    Promoted = new ItemRequest(ItemIds.PowerfulBlood),
                    QuantityYield = Promotion.Tier5PromotionYield,
                    Ingredients = new Dictionary<ItemRequest, int>
                    {
                        {new ItemRequest(ItemIds.PowerfulBlood), 1},
                        {new ItemRequest(ItemIds.PotentBlood), 50},
                        {new ItemRequest(ItemIds.CrystallineDust), 5}
                    }
                };
            }
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Request all items data from GW2Spidy (limitation)");
            // Get all requests
            var allItems = new AllItemsRequest();
            var loadingTask = allItems.Send();
            while (!loadingTask.IsCompleted)
            {
                Console.Write(".");
                Thread.Sleep(500);
            }
            Console.WriteLine();
            foreach (var promotion in Promotions)
            {
                Console.WriteLine("Promoting to {0}:", promotion.Name);
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
