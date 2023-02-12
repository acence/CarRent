using CarRent.WebApi.Converters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarRent.IntegrationTests.Helpers
{
    public static class SerializationHelper
    {
        public static JsonSerializerOptions Options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters =
                {
                    new JsonStringEnumConverter(),
                    new DateOnlyJsonConverter()
                }
        };

        public static async Task<T> GetDeserializedValue<T>(HttpResponseMessage responseMessage)
        {
            var body = await responseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(body, Options)!;
        }
    }
}
