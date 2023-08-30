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
        public int Count = 0; //count wordt gebruikt om bij te houden hoeveelste vraag het spel zich bevindt

        public Question CurrentQuestion = null;
        public Answer SelectedAnswer = null;

        private List<Question> Questions = null;

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

        //methode voor het vullen van list met questions
        public async Task FillQuestionList()
        {
            Questions = new List<Question>();
            
            for (int i = 0; i < 15; i++)
            {
                Question Q = new WhoIsThatPokemon();
                await Q.CreateQuestion();
                Q.PrepareQuestion();
                
                Questions.Add(Q);
            }
        }
        
        //methode voor het verkrijgen van de volgende question uit de list
        public Question GetQuestionFromList()
        {
            Question Q = Questions[Count];
            
            return Q;
        }

        // methode voor het controleren of the list gevuld is, zodat het spel kan beginnen
        public bool IsQuestionsListFilled()
        {
            if (Questions.Count == 15)
            {
                return true;
            }

            return false;
        }
        

    }
}