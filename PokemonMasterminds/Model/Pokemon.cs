namespace PokemonMasterminds.Model;

public class Pokemon
{
    public int id { get; set; }
    //public string imageUrl { get; set; }
    //private List<Move> moves;     could perhaps be implemented for generating other questions
    public string name { get; set; }
    //private List<Type> types;     could perhaps be implemented for generating other questions

    public Pokemon(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}
