using System.Collections.ObjectModel;

namespace PokemonMasterminds.Model
{
    public class Game
    {
        public ObservableCollection<Player> Players { get; set; }

        public Game()
        {
            Players = new ObservableCollection<Player>();
        }
    }
}