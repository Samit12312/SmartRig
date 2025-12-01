using Models;
using System.Net.Http.Headers;
using System.Text.Json;
namespace Tests
{
    internal class Program
    {
        static void Main(string[] args)
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
            Console.ReadLine();
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