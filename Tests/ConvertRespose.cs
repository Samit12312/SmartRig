using System.Text.Json.Serialization;

namespace Tests
{
    public class ConvertResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("result")]
        public decimal Result { get; set; }
    }
}