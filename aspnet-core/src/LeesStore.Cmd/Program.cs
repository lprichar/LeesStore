using LeesStore.Cmd.ClientProxy;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LeesStore.Cmd
{
    class Program
    {
        static async Task Main()
        {
            using var httpClient = new HttpClient();
            var clientApiProxy = new ClientApiProxy("http://localhost:21021/", httpClient);
            Console.WriteLine("Enter a product id:");
            var key = Console.ReadKey();
            var productId = int.Parse(key.KeyChar.ToString());
            var product = await clientApiProxy.GetProductAsync(productId);
            Console.WriteLine();
            Console.WriteLine($"Your product is: '{product.Name}'");
        }
    }
}
