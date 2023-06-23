using System.Collections.ObjectModel;

namespace PokemonMasterminds.Model
{
    public class Game
    {
      //  public ObservableCollection<Player> Players { get; set; }
        public LobbyList Lobby { get; set; }

        private static Game _instance;
        public static Game Instance => _instance ??= new Game();
        
        public Game()
        {
           // Players = new ObservableCollection<Player>();
            Lobby = new LobbyList();
        }
        
    }
}