using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using ViaCepClient.Client;
using ViaCepClient.Extensions.DependencyInjection;
using ViaCepClient.Http;
using ViaCepClient.Models;

namespace ViaCepClient.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter CEP: ");
            string cep = Console.ReadLine();

            CallViaCepClientDirectly(cep).GetAwaiter().GetResult();
            CallViaCepClientFromDI  (cep).GetAwaiter().GetResult();
        }


        private static async Task CallViaCepClientDirectly(string cep)
        {
            HttpClient            httpClient   = new HttpClient();
            ViaCepClientOptions   options      = new ViaCepClientOptions();
            IViaCepRequestBuilder builder      = new ViaCepRequestBuilder(options);
            IRestClient           restClient   = new RestClient(httpClient);
            IViaCepClient         viaCepClient = new Client.ViaCepClient(restClient, builder);

            ResponseMessage<CepDetails> response = await viaCepClient.SendRequestAsync(new Cep(cep));
            CepDetails cepDetails = response.Content;

            string json = JsonSerializer.Serialize(cepDetails, new JsonSerializerOptions 
            {
                WriteIndented = true, 
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping 
            });

            Console.WriteLine(json);
        }

        private static async Task CallViaCepClientFromDI(string cep)
        {
            using ServiceProvider serviceProvider = new ServiceCollection()
                                                    .AddViaCepClient(opt => opt.SetBaseAddress("http://viacep.com.br"))
                                                    .BuildServiceProvider();

            IViaCepClient viaCepClient = serviceProvider.GetRequiredService<IViaCepClient>();

            ResponseMessage<CepDetails> response = await viaCepClient.SendRequestAsync(new Cep(cep));
            CepDetails cepDetails = response.Content;

            string json = JsonSerializer.Serialize(cepDetails, new JsonSerializerOptions 
            {
                WriteIndented   = true, 
                Encoder         = JavaScriptEncoder.UnsafeRelaxedJsonEscaping 
            });

            Console.WriteLine(json);
        }
    }
}
