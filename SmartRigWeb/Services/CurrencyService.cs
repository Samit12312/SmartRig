using System.Text.Json;

namespace SmartRigWeb.Services
{
    public class CurrencyService
    {
        private readonly HttpClient httpClient;

        public CurrencyService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Dictionary<string, string>> GetCurrencies()
        {
            string json = await this.httpClient.GetStringAsync("https://api.frankfurter.dev/v2/currencies");

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

        public async Task<double> GetRate(string fromCurrency, string toCurrency)
        {
            if (fromCurrency == toCurrency)
            {
                return 1;
            }

            string url = "https://api.frankfurter.dev/v2/rate/" + fromCurrency + "/" + toCurrency;
            string json = await this.httpClient.GetStringAsync(url);

            JsonDocument document = JsonDocument.Parse(json);

            double rate = document.RootElement.GetProperty("rate").GetDouble();

            return rate;
        }

        public string GetSymbol(string currencyCode)
        {
            if (currencyCode == "ILS")
            {
                return "₪";
            }

            if (currencyCode == "USD")
            {
                return "$";
            }

            if (currencyCode == "EUR")
            {
                return "€";
            }

            if (currencyCode == "GBP")
            {
                return "£";
            }

            return currencyCode + " ";
        }
    }
}