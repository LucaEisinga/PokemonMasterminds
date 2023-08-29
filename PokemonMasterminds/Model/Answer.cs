namespace PokemonMasterminds.Model;

//dit is de answer class, elk mogelijke antwoord heeft verscheidene data nodig welk opgeslagen moet worden
public class Answer
{
	public Pokemon pokemon { get; private set; }
	public bool CorrectAnswer { get; set; }

	public Answer(Pokemon pokemon)
	{
		this.pokemon = pokemon;
	}
}