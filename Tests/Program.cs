using System.Text.Json;

namespace Tests
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            double price;
            Console.Write("Enter price: ");
            while (!double.TryParse(Console.ReadLine(), out price))
            {
                Console.Write("Invalid price. Enter number: ");
            }

            Dictionary<string, string> currencies = await GetCurrencies();
            List<string> codes = new List<string>(currencies.Keys);
            codes.Sort();

            if (codes.Count == 0)
            {
                Console.WriteLine("No currencies were loaded.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("========== CURRENCY LIST ==========");

            for (int i = 0; i < codes.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + codes[i] + " - " + currencies[codes[i]]);
            }

            Console.WriteLine("==================================");

            int fromChoice;
            Console.Write("Choose FROM currency number: ");
            while (!int.TryParse(Console.ReadLine(), out fromChoice) || fromChoice < 1 || fromChoice > codes.Count)
            {
                Console.Write("Invalid number. Choose number from 1 to " + codes.Count + ": ");
            }

            string fromCurrency = codes[fromChoice - 1];

            int toChoice;
            Console.Write("Choose TO currency number: ");
            while (!int.TryParse(Console.ReadLine(), out toChoice) || toChoice < 1 || toChoice > codes.Count)
            {
                Console.Write("Invalid number. Choose number from 1 to " + codes.Count + ": ");
            }

            string toCurrency = codes[toChoice - 1];

            double convertedPrice = await ConvertPrice(price, fromCurrency, toCurrency);

            Console.WriteLine();
            Console.WriteLine(price + " " + fromCurrency + " = " + convertedPrice.ToString("0.00") + " " + toCurrency);

            Console.ReadLine();
        }

        static async Task<Dictionary<string, string>> GetCurrencies()
        {
            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync("https://api.frankfurter.dev/v2/currencies");

            JsonDocument document = JsonDocument.Parse(json);
            Dictionary<string, string> currencies = new Dictionary<string, string>();

            foreach (JsonElement item in document.RootElement.EnumerateArray())
            {
                string code = item.GetProperty("iso_code").GetString();
                string name = item.GetProperty("name").GetString();

                if (!currencies.ContainsKey(code))
                {
                    currencies.Add(code, name);
                }
            }

            return currencies;
        }

        static async Task<double> ConvertPrice(double price, string fromCurrency, string toCurrency)
        {
            HttpClient client = new HttpClient();
            string url = "https://api.frankfurter.dev/v2/rate/" + fromCurrency + "/" + toCurrency;

            string json = await client.GetStringAsync(url);

            JsonDocument document = JsonDocument.Parse(json);

            double rate = document.RootElement.GetProperty("rate").GetDouble();

            return price * rate;
        }
    }
}