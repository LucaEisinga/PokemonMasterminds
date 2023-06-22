namespace PokemonMasterminds.Model;

public class Pokemon
{
    public int id { get; set; }

    public string name { get; set; }
    
    public Sprite sprites { get; set; }

    public class Sprite
    {
        public string front_default { get; set; }
    }
   
}

