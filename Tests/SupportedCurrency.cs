using System.Text.Json.Serialization;

namespace Tests
{
    public class SupportedCurrency
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}