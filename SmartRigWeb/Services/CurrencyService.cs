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

        private Dictionary<string, string> DefaultCurrencies()
        {
            return new Dictionary<string, string>
            {
                { "ILS", "Israeli New Shekel" },
                { "USD", "US Dollar" },
                { "EUR", "Euro" },
                { "GBP", "British Pound" }
            };
        }

        public async Task<Dictionary<string, string>> GetCurrencies()
        {
            try
            {
                string json = await this.httpClient.GetStringAsync("https://api.frankfurter.dev/v2/currencies");

                JsonDocument document = JsonDocument.Parse(json);
                Dictionary<string, string> currencies = new Dictionary<string, string>();

                if (document.RootElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement item in document.RootElement.EnumerateArray())
                    {
                        string code = item.GetProperty("iso_code").GetString();
                        string name = item.GetProperty("name").GetString();

                        if (!string.IsNullOrWhiteSpace(code) && !currencies.ContainsKey(code))
                        {
                            currencies.Add(code, name ?? code);
                        }
                    }
                }
                else if (document.RootElement.ValueKind == JsonValueKind.Object)
                {
                    foreach (JsonProperty item in document.RootElement.EnumerateObject())
                    {
                        if (!currencies.ContainsKey(item.Name))
                        {
                            currencies.Add(item.Name, item.Value.GetString() ?? item.Name);
                        }
                    }
                }

                if (!currencies.ContainsKey("ILS"))
                    currencies.Add("ILS", "Israeli New Shekel");

                if (currencies.Count == 0)
                    return DefaultCurrencies();

                return currencies;
            }
            catch
            {
                return DefaultCurrencies();
            }
        }

        public async Task<double> GetRate(string fromCurrency, string toCurrency)
        {
            if (fromCurrency == toCurrency)
            {
                return 1;
            }

            try
            {
                string url = "https://api.frankfurter.dev/v2/rate/" + fromCurrency + "/" + toCurrency;
                string json = await this.httpClient.GetStringAsync(url);

                JsonDocument document = JsonDocument.Parse(json);
                return document.RootElement.GetProperty("rate").GetDouble();
            }
            catch
            {
                return 1;
            }
        }

        public string GetSymbol(string currencyCode)
        {
            if (currencyCode == "ILS") return "₪";
            if (currencyCode == "USD") return "$";
            if (currencyCode == "EUR") return "€";
            if (currencyCode == "GBP") return "£";

            return currencyCode + " ";
        }
    }
}