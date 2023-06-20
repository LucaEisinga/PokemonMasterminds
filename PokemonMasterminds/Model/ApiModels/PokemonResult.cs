namespace PokemonMasterminds.Model.ApiModels;
using System.Collections.Generic;

public class PokemonResult
{
        // Result from pokeapi https://pokeapi.co/api/v2/pokemon/1
        public int id { get; set; }
        public string name { get; set; }
        public Sprite sprite { get; set; }

        public class Sprite
        {
            public string front_default { get; set; }
        }
        
        public class Info
        {
            public string name { get; set; }
            public string url { get; set; }
        }
    
}