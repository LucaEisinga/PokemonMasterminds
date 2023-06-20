using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonMasterminds.Model
{
    public class Api
    {
        private const string BaseUrl = "https://pokeapi.co/api/v2/";
        private static readonly HttpClient Client = new();
        private HttpResponseMessage responex;

        //private readonly Dictionary<int, Pokemon> _cachedPokemon = new();

        private static Api _instance;

        public static Api Instance => _instance ??= new Api();

        public async Task<Pokemon> FetchPokemonData(int id)
        {
            var apiUrl = $"{BaseUrl}pokemon/{id}";

            try
            {
                HttpResponseMessage response = await Client.GetAsync(apiUrl);

                this.responex = response;
                
                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    var pokemon = await JsonSerializer.DeserializeAsync<Pokemon>(responseStream);

                    return pokemon;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return null;
        }
    }
}