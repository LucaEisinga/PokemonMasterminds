using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PokemonMasterminds.Model
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public string LobbyCode { get; set; }

        public Player(string name)
        {
            Name = name;
            Score = 0;
        }
        public ObservableCollection<Player> Players { get; set; }
    }
}