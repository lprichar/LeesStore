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
            Console.WriteLine("API Key:");
            var apiKey = Console.ReadLine();
            Console.WriteLine("Secret:");
            var secret = Console.ReadLine();

            var authenticateResultModel = await clientApiProxy.AuthenticateAsync(new ClientAuthenticateModel
            {
                ApiKey = apiKey,
                Secret = secret
            });

            clientApiProxy.AccessToken = authenticateResultModel.AccessToken;

            Console.WriteLine("Enter a product id:");
            var key = Console.ReadLine();
            var productId = int.Parse(key);
            var product = await clientApiProxy.GetProductAsync(productId);
            Console.WriteLine();
            Console.WriteLine($"Your product is: '{product.Name}'");
        }
    }
}
