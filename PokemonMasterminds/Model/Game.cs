using System.Runtime.CompilerServices;
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

        private List<Question> Questions = null;

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

        public async Task ResetGame()
        {
            this.Count = 0;
            GameIsActive = false;
            SetToQuestionNull();
            Lobby.Player.Score = 0;

            //await FillQuestionList();
        }

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
    

        public void DisableQuestionFromList(Question question)
        {
            question.HasBeenUsed = true;
            // Questions.Remove(question);
        }

        public Question GetQuestionFromList()
        {
            Question Q = Questions[Count];

           // DisableQuestionFromList(Q);
            
            return Q;
        }

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