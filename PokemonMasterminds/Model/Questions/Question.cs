namespace PokemonMasterminds.Model.Questions;

public abstract class Question
{
    public List<Answer> Answers { get; set; }
    public string Text { get; set;  }

    public Question()
    {
        Answers = new List<Answer>(); 
        CreateQuestion();
    }

    public abstract Task CreateQuestion();
}