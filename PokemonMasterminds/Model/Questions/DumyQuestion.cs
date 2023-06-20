namespace PokemonMasterminds.Model;

public class DumyQuestion : Question
{
    public override Task CreateQuestion()
    {
        this.Text = "Who is that Pokémon!";

        Answers = new List<Answer>();

        Answers.Add(new Answer("Hans", true));
        Answers.Add(new Answer("Hans", false));
        Answers.Add(new Answer("Hans", false));
        Answers.Add(new Answer("Hans", false));


        return null;
    }
}