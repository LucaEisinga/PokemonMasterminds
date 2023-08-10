using PokemonMasterminds.Model.Questions;

namespace PokemonMasterminds.Model
{
    public class Game
    {
        public LobbyList Lobby { get; set; }
        public bool GameIsActive { get; set; }

        private static Game _instance;
        public static Game Instance => _instance ??= new Game();
        public int Count = 0;
        
        public Question CurrentQuestion = null;
        public Answer SelectedAnswer = null;
        
        public Game()
        {
            Lobby = new LobbyList();
            GameIsActive = false;
        }

        public void SetToQuestionNull()
        {
            this.CurrentQuestion = null;
            this.SelectedAnswer = null;
        }

        public void ResetGame()
        {
            this.Count = 0;
            GameIsActive = false;
            SetToQuestionNull();
            Lobby.Player.Score = 0;
        }

    }
}