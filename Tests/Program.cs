using Models;
using System.Data;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text.Json;
using ApiClient;
using System.Net;
using Models.ViewModels;
namespace Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            TestWebClient();
            Console.ReadLine();
        }
        static void TestWebClient()
        { 
            WebClient<ComputerDetailsViewModel> webClient = new WebClient<ComputerDetailsViewModel>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 7249;
            webClient.Path = "api/Guest/GetComputerDetails";
            webClient.AddParameter("id", "14");
            ComputerDetailsViewModel computer = webClient.Get();
            Console.WriteLine(computer.computer.ComputerName);
        }

        static void CurrencyTest()
        {
            List<Currency> list = CurrencyTestList().Result;
            int count = 1;
            foreach (Currency currency in list)
            {
                Console.WriteLine($"{count}. {currency.symbol} - {currency.name}");
                count++;
            }
            Console.Write("Currency number");

            int from = int.Parse(Console.ReadLine());
            Console.WriteLine("select curncy number to");
            int to = int.Parse(Console.ReadLine());
            Console.Write("insert sum");
            int sum = int.Parse(Console.ReadLine());
            ConvertResult r = GetResult(list[from - 1].symbol, list[to - 1].symbol, sum).Result;
            Console.WriteLine($"{r.result.amountToConvert} {r.result.from} = {r.result.convertedAmount} {r.result.to}");
        }
        static void ViewHash()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Write("Enter password: ");
                string pass = Console.ReadLine();

                Console.Write("Enter salt: ");
                string salt = Console.ReadLine();

                string hash = CaculateHash(pass, salt);
                Console.WriteLine($"Hash #{i + 1}: {hash}");

                Console.WriteLine(); // spacing
            }
        }

        static string GenerateSalt() // generateing salt for the food :D
        {
            byte[] saltBytes = new byte[32];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }
        static string CaculateHash(string password, string salt)
        {
            string s = password + salt;
            byte[] pass = System.Text.Encoding.UTF8.GetBytes(s);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(pass);
                return Convert.ToBase64String(bytes);
            }
        }
        static async Task<ConvertResult> GetResult(string from, string to, double amount)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://currency-converter18.p.rapidapi.com/api/v1/convert?from={from}&to={to}&amount={amount}"),
                Headers =
    {
        { "x-rapidapi-key", "96334a59famsh63ef8779a2d5330p17c328jsndaf044530bd3" },
        { "x-rapidapi-host", "currency-converter18.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ConvertResult>(body);
            }
        }
        static void ModelValidation()
        {
            User u1 = new User();
            u1.UserName = "12";
            u1.UserEmail = "123";
            u1.UserPassword = "sda";
            u1.UserAddress = "";
            u1.UserPhoneNumber = "123";
            Dictionary<string, List<String>> errors = u1.AllErrors();
            if (u1.IsValid == false)
            {
                foreach (var error in errors)
                {
                    foreach (var errorsmsg in error.Value)
                    {
                        Console.WriteLine(errorsmsg);
                    }
                }
            }
        }
        static async Task<List<Currency>> CurrencyTestList()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://currency-converter18.p.rapidapi.com/api/v1/supportedCurrencies"),
                Headers =
    {
        { "x-rapidapi-key", "96334a59famsh63ef8779a2d5330p17c328jsndaf044530bd3" },
        { "x-rapidapi-host", "currency-converter18.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Currency>>(body);
            }
        }
    }
}