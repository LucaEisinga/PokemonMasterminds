using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PokemonMasterminds.Model;
using PokemonMasterminds.Pages;
using PokemonMasterminds.Model.Questions;

namespace PokemonMasterminds.ViewModels
{
    class QuestionViewModel: INotifyPropertyChanged
    {
        private string _answerOneText;
        private string _answerTwoText;
        private string _answerThreeText;
        private string _answerFourText;
        private string _pokeImage;
        public int Count { get; set; }
        
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

        //GetQuestionCommand : ICommand
        public ICommand GetQuestionCommand { private set; get; }

        //OnAnswerSelectedCommand : ICommand

        public ICommand OnAnswerSelectedCommand { private set; get; }
        public ICommand NextQuestionCommand { get; }
        public List<Player> PlayerList { get; set; }
        public LobbyViewModel LobbyViewModel { get; set; }

        public QuestionViewModel()
        {
            LoadQuestionAsync(); //.Wait();
        }


        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private string MyPlayerName;

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
        
        

        private async Task LoadQuestionAsync()
        {
            Question question = new WhoIsThatPokemon();
            await question.CreateQuestion();

            question.ShuffleAnswers();
            
            if (question.Answers.Count >= 4)
            {
                Game.Instance.CurrentQuestion = question;
                
                AnswerOneText = question.Answers[0].pokemon.name;
                AnswerTwoText = question.Answers[1].pokemon.name;
                AnswerThreeText = question.Answers[2].pokemon.name;
                AnswerFourText = question.Answers[3].pokemon.name;

                foreach (Answer a in question.Answers)
                {
                    if (a.CorrectAnswer)
                    {
                        PokeImage = a.pokemon.sprites.front_default;
                        break;
                    }
                }
            }
            else
            {
                AnswerOneText = "Default answer 1";
                AnswerTwoText = "Default answer 2";
                AnswerThreeText = "Default answer 3";
                AnswerFourText = "Default answer 4";
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
