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
                List<Task<Pokemon>> fetchTasks = new List<Task<Pokemon>>();

                for (int i = 0; i < numberOfOptions; i++)
                {
                    Task<Pokemon> fetchTask = api.FetchPokemonData(random.Next(x, y + 1));
                    fetchTasks.Add(fetchTask);
                }

                // Await the completion of all fetch tasks
                Pokemon[] fetchedResults = await Task.WhenAll(fetchTasks);

                // Process the fetched results
                foreach (Pokemon pokemon in fetchedResults)
                {
                    Answers.Add(new Answer(pokemon.name, Answers.Count == 0));
                }
            }
        }

        private static async Task<Pokemon> FetchRandomPokemon(Api api, Random random, int x, int y, List<Pokemon> fetchedPokemon)
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
            return pokemon;
        }
    }
}