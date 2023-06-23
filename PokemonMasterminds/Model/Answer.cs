namespace PokemonMasterminds.Model;

public class Answer
{
	public Pokemon pokemon { get; private set; }
	public bool CorrectAnswer { get; private set; }

	public Answer(Pokemon pokemon, bool correctAnswer)
	{
		this.pokemon = pokemon;
		this.CorrectAnswer = correctAnswer;
	}
}