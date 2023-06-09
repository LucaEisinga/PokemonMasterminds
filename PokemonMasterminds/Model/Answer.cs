namespace PokemonMasterminds.Model;

public class Answer
{
	public string Value { get; private set; }
	public bool CorrectAnswer { get; private set; }

	public Answer(string Answer, bool CorrectAnswer)
	{
		Value = Answer;
		this.CorrectAnswer = CorrectAnswer;
	}
}