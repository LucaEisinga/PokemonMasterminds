using System.Text.Json;

namespace PokemonMasterminds.Model
{
    public class Api : IDisposable
    {
        private const string BaseUrl = "https://pokeapi.co/api/v2/";
        private static readonly HttpClient Client = new HttpClient();

        private static Api _instance;

        public static Api Instance => _instance ??= new Api();

        public async Task<Pokemon> FetchPokemonData(int id)
        {
            string apiUrl = $"{BaseUrl}pokemon/{id}";

            try
            {
                HttpResponseMessage response = await Client.GetAsync(apiUrl);

                Console.WriteLine($"Response Status Code: {response.StatusCode}");

                // Add a delay of 0,1 second before fetching the response content
                await Task.Delay(100);

                string json = await Client.GetStringAsync(apiUrl);

                Console.WriteLine($"Response Content: {json}");

                Console.WriteLine(json); // Print the received JSON to verify its structure
                // Deserialize JSON into Pokemon object
                Pokemon pokemon = JsonSerializer.Deserialize<Pokemon>(json);

                return pokemon;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
                throw;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON Deserialization Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}