using PokemonMasterminds.Model.Questions;

namespace PokemonMasterminds.Model
{
    //dit is de game class, hierin wordt alle informatie bijgehouden om het spel te laten verlopen
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

        //methode om de huidige vraag weer uit de game class te halen
        public void SetToQuestionNull()
        {
            this.CurrentQuestion = null;
            this.SelectedAnswer = null;
        }

        //methode om de game weer te resetten zodat er een nieuw spel gespeeld kan worden
        public void ResetGame()
        {
            this.Count = 0;
            GameIsActive = false;
            SetToQuestionNull();
            Lobby.Player.Score = 0;
        }

    }
}