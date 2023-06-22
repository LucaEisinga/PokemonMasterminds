using PokemonMasterminds.Model.ApiModels;
using System.Diagnostics;
using System.Text.Json;

namespace PokemonMasterminds.Model
{
    public class Api : IDisposable
    {
        private readonly HttpClient Client;
        JsonSerializerOptions SerializerOptions;
        private const string BaseUrl = "https://pokeapi.co/api/v2/";

        public List<PokemonResult> Pokemons {  get; private set; }

        private static Api _instance;

        public static Api Instance => _instance ??= new Api();
        public Api()
        {
            Client = new HttpClient();
            SerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<Pokemon> FetchPokemonData(int id)
        {
            string apiUrl = $"{BaseUrl}pokemon/{id}";
            Pokemon pokemon = new Pokemon();
            string json = await Client.GetStringAsync(apiUrl);

            try
            {
                HttpResponseMessage response = await Client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    pokemon = JsonSerializer.Deserialize<Pokemon>(content, SerializerOptions);
                }
                Console.WriteLine($"Response content: {json}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return pokemon;
        }

        public async Task SavePokemonsAsync(PokemonResult pokemon, bool isNewItem = false)
        {
            string apiUrl = $"{BaseUrl}pokemon/1";
            try
            {
                string json = JsonSerializer.Serialize<PokemonResult>(pokemon, SerializerOptions);
                StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                    response = await Client.PostAsync(apiUrl, content);
                else
                    response = await Client.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"\tPokemonResult succesfully saved");
            }
            catch(Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public void Dispose()
        {
            Debug.WriteLine("Dispose");
        }
    }
}