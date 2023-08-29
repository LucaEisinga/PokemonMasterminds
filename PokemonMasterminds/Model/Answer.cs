namespace PokemonMasterminds.Model;

public class Answer
{
	public Pokemon pokemon { get; private set; }
	public bool CorrectAnswer { get; set; }

	public Answer(Pokemon pokemon)
	{
		this.pokemon = pokemon;
	}
}