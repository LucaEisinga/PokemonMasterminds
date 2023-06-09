namespace PokemonMasterminds.Model;

public class Question
{
	public string Questions { get; private set; }
	public List<Answer> Answers { get; private set; }

	public Question(string Question, List<Answer> Answers)
	{
		Questions = Question;
		this.Answers = Answers;
	}
}