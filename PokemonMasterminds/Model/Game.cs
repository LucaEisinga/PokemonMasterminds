namespace PokemonMasterminds.Model
{
    public class Game
    {
        public LobbyList Lobby { get; set; }
        public bool GameIsActive { get; set; }

        private static Game _instance;
        public static Game Instance => _instance ??= new Game();
        
        public Game()
        {
            Lobby = new LobbyList();
            GameIsActive = false;
        }
        
    }
}