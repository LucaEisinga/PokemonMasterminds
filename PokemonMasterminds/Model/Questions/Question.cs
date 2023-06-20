namespace PokemonMasterminds.Model.Questions;

public abstract class Question
{
    public List<Answer> Answers { get; set; }
    public string Text { get; set;  }

    public Question()
    {
        Answers = new List<Answer>(); 
        CreateQuestion();
        //createRandomQuestion();
        //this.Answers = Answers;
        //this.Text = question;
    }

    public abstract Task CreateQuestion();
}