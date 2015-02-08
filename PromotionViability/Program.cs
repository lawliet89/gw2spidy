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
                    QuantityYield = 5,
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
            var request = new ItemRequest(24295);
            Console.WriteLine(request.Get().Single().ToString());

            // Keep the console window open in debug modde.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
