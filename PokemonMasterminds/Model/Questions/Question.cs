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

    public void PrepareQuestion()
    {
        //ShuffleAnswers(); //shuffle is niet meer nodig
        AssignCorrect();
    }
    private void ShuffleAnswers()
    {
        var random = new Random();
        int n = Math.Min(Answers.Count, 4);
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            (Answers[k], Answers[n]) = (Answers[n], Answers[k]);
        }
        
    }

    private void AssignCorrect()
    {
        if (getCorrectAnswer() == null)
        {
            var random = new Random();
            int n = random.Next(0, 4);

            Answers[n].CorrectAnswer = true;
        }
    }

    public Answer getCorrectAnswer()
    {
        foreach (var a in Answers)
        {
            if (a.CorrectAnswer)
            {
                return a;
            }
        }

        return null;
    }
    
}