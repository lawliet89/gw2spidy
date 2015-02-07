using System;
using Gw2spidyApi.Requests;
namespace PromotionViability
{
    class Program
    {
        static void Main(string[] args)
        {
            var request = new ItemRequest(24295);
            Console.WriteLine(request.Get().ToString());

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
