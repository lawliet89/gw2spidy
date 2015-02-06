using Gw2spidyApi.Requests;
namespace PromotionViability
{
    class Program
    {
        static void Main(string[] args)
        {
            var request = new ItemRequest(24295);
            System.Console.WriteLine(request.Get().ToString());
        }
    }
}
