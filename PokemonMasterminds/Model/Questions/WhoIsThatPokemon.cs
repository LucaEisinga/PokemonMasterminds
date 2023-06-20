namespace PokemonMasterminds.Model.Questions;

public class WhoIsThatPokemon : Question
{
    public override async Task CreateQuestion()
    {
        this.Text = "Who is that Pokémon!";

        Api api = Api.Instance;

        int x = 1; // Minimum value
        int y = 1000; // Maximum value

        Random random = new Random();

        List<int> used = new List<int>();

        int randomNumber = random.Next(x, y + 1);

        
        Pokemon correctPoke = await api.FetchPokemonData(randomNumber);
        
        used.Add(randomNumber);
        
        randomNumber = random.Next(x, y + 1);
        while (randomNumber == used[0])
        {
            randomNumber = random.Next(x, y + 1); 
        }
        Pokemon wrongPoke1 = await api.FetchPokemonData(randomNumber);

        randomNumber = random.Next(x, y + 1);
        while (randomNumber == used[0] || randomNumber == used[1])
        {
            randomNumber = random.Next(x, y + 1); 
        }
        Pokemon wrongPoke2 = await api.FetchPokemonData(randomNumber);
            
        randomNumber = random.Next(x, y + 1);
        while (randomNumber == used[0] || randomNumber == used[1] || randomNumber == used[2])
        {
            randomNumber = random.Next(x, y + 1); 
        }
        Pokemon wrongPoke3 = await api.FetchPokemonData(randomNumber);
   
        Answers.Add(new Answer(correctPoke.name, true));
        Answers.Add(new Answer(wrongPoke1.name, false));
        Answers.Add(new Answer(wrongPoke2.name, false));
        Answers.Add(new Answer(wrongPoke3.name, false));

    }
}
