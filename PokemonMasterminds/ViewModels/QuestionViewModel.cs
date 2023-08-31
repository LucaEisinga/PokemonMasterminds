using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PokemonMasterminds.Model;
using PokemonMasterminds.Pages;
using PokemonMasterminds.Model.Questions;

namespace PokemonMasterminds.ViewModels
{
    //viewmodel voor de questions, hier wordt geregeld dat de vragen en antwoorden juist worden weergegeven
    class QuestionViewModel: INotifyPropertyChanged
    {
        private string _answerOneText;
        private string _answerTwoText;
        private string _answerThreeText;
        private string _answerFourText;
        private string _pokeImage;
        
        public string AnswerOneText
        {
            get { return _answerOneText; }
            set { _answerOneText = value; OnPropertyChanged(); }
        }

        public string AnswerTwoText
        {
            get { return _answerTwoText; }
            set { _answerTwoText = value; OnPropertyChanged(); }
        }

        public string AnswerThreeText
        {
            get { return _answerThreeText; }
            set { _answerThreeText = value; OnPropertyChanged(); }
        }

        public string AnswerFourText
        {
            get { return _answerFourText; }
            set { _answerFourText = value; OnPropertyChanged(); }
        }
        
        public string PokeImage
        {
            get { return _pokeImage; }
            set { _pokeImage = value; OnPropertyChanged(); }
        }

        public ICommand NextQuestionCommand { get; }
        public string _ScorePoints { get; set; }

        public string ScorePoints
        {
            get { return _ScorePoints; }
            set { _ScorePoints = value; OnPropertyChanged(); }
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public QuestionViewModel()
        {
            UpdateScore();
            LoadQuestionAsync();
        }
        
        public QuestionViewModel(Game game)
        {
            LoadQuestionAsync();

            INavigation navigation = App.Current.MainPage.Navigation;

            void NextQuestion()
            {
                navigation.PushAsync(new GamePage());
                return;
            }

            NextQuestionCommand = new Command(() =>
            {
                NextQuestion();
            });
        }

        //methode voor het inladen van de vraag op het scherm
        private async Task<Task> LoadQuestion()
        {
            Game.Instance.CurrentQuestion = null;
            Game.Instance.CurrentQuestion = Game.Instance.GetQuestionFromList();
            
            Question currentQuestion = Game.Instance.CurrentQuestion;

            if (Game.Instance.CurrentQuestion.Answers.Count >= 4)
            {
                Game.Instance.CurrentQuestion = currentQuestion;
                
                AnswerOneText = Game.Instance.CurrentQuestion.Answers[0].pokemon.name;
                AnswerTwoText = Game.Instance.CurrentQuestion.Answers[1].pokemon.name;
                AnswerThreeText = Game.Instance.CurrentQuestion.Answers[2].pokemon.name;
                AnswerFourText = Game.Instance.CurrentQuestion.Answers[3].pokemon.name;
                
                PokeImage = Game.Instance.CurrentQuestion.getCorrectAnswer().pokemon.sprites.pokemonSprite;
               
            }
            else
            {
                AnswerOneText = "Default answer 1";
                AnswerTwoText = "Default answer 2";
                AnswerThreeText = "Default answer 3";
                AnswerFourText = "Default answer 4";
            }
            
            return Task.CompletedTask;
        }
        
        //methode om de huidige score te updaten
        private async Task UpdateScore()
        {
            ScorePoints = Game.Instance.Lobby.Player.Score.ToString();
        }

        private async Task LoadQuestionAsync()
        {
            await LoadQuestion();
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
