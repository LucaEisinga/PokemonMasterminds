namespace PokemonMasterminds.Model.Questions
{
    public class WhoIsThatPokemon : Question
    {
        public override async Task CreateQuestion()
        {
            using (Api api = Api.Instance)
            {
                int x = 1; // Minimum value
                int y = 1000; // Maximum value
                int numberOfOptions = 4;

                Random random = new Random();
                List<Pokemon> fetchedPokemon = new List<Pokemon>();

                for (int i = 0; i < numberOfOptions; i++)
                {
                    Pokemon pokemon = null;
                    bool isValidPokemon = false;

                    while (!isValidPokemon)
                    {
                        int randomNumber = random.Next(x, y + 1);
                        pokemon = await api.FetchPokemonData(randomNumber);
                        isValidPokemon = pokemon != null && !fetchedPokemon.Contains(pokemon);
                    }

                    fetchedPokemon.Add(pokemon);
                    
                    Answers.Add(new Answer(pokemon));

                    if (Answers.Count == 4)
                    {
                        return;
                    }
                }
            }
        }
    }
}