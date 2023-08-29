namespace PokemonMasterminds.Model.Questions;

//dit is basis class voor alle verschillende question classes
public abstract class Question
{
    public List<Answer> Answers { get; set; }
    public string Text { get; set;  }
    public bool HasBeenUsed { get; set; }

    public Question()
    {
        HasBeenUsed = false;
        Answers = new List<Answer>(); 
        CreateQuestion();
    }

    public abstract Task CreateQuestion();

    // deze methode bevat alle stappen welke moeten gebeuren om een question gereed te maken
    public void PrepareQuestion()
    {
        AssignCorrect();
    }
    
    //deze methode werdt gebruikt om antwoorden door elkaar te husselen, (echter is dit overbodig geworden en dus niet geimplementeerd)
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

    // Als er nog geen correct antwoord bestaat, wordt die met deze methode aangemaakt. Er wordt 1 correct antwoord gepakt uit de 4 opties.
    private void AssignCorrect()
    {
        if (getCorrectAnswer() == null)
        {
            var random = new Random();
            int n = random.Next(0, 4);

            Answers[n].CorrectAnswer = true;
            Answers[n].pokemon.sprites.SetImage();
        }
    }

    //deze methode geeft het correcte antwoord op de vraag
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