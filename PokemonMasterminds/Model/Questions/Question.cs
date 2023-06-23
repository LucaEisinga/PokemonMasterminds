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
    
    public void ShuffleAnswers()
    {
        var random = new Random();
        int n = Math.Min(Answers.Count, 4);
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Answer value = Answers[k];
            Answers[k] = Answers[n];
            Answers[n] = value;
        }
    }
    
}