using PokemonMasterminds.Model.ApiModels;
using System.Diagnostics;
using System.Text.Json;

namespace PokemonMasterminds.Model
{
    
    // dit is de api class, welke de pokeapi gebruikt om de data voor de vragen op te halen.
    public class Api : IDisposable
    {
        
        //een http client voor het verbinden met de pokeapi
        private readonly HttpClient Client;
        JsonSerializerOptions SerializerOptions;
        private const string BaseUrl = "https://pokeapi.co/api/v2/";

        public List<PokemonResult> Pokemons {  get; private set; }

        private static Api _instance;

        public static Api Instance => _instance ??= new Api();
        public Api()
        {
            //aanmaken van http client
            Client = new HttpClient();
            SerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        //methode voor het verkrijgen van de pokemon data vanuit de pokeapi, deze methode returnt een pokemon
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

        public void Dispose()
        {
            Debug.WriteLine("Dispose");
        }
    }
}