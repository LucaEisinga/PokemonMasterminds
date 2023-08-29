using System.Collections.ObjectModel;

namespace PokemonMasterminds.Model
{
    //player class hierin wordt alle informatie van een player bijgehouden
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }

        public Player(string name)
        {
            Name = name;
            Score = 0;
        }
    }
}