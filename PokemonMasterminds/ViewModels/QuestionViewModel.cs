using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Dispatching;
using PokemonMasterminds.Model;
using PokemonMasterminds.Pages;
using Timer = System.Timers.Timer;

namespace PokemonMasterminds.ViewModels
{
    class QuestionViewModel: INotifyPropertyChanged
    {
        private string _answerOneText;
        private string _answerTwoText;
        private string _answerThreeText;
        private string _answerFourText;
        
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
        
        
        
        //GetQuestionCommand : ICommand
        public ICommand GetQuestionCommand { private set; get; }

        //OnAnswerSelectedCommand : ICommand

        public ICommand OnAnswerSelectedCommand { private set; get; }
       
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

<<<<<<< Updated upstream
        private string MyPlayerName;

        public QuestionViewModel(Game game)
        {
            INavigation navigation = App.Current.MainPage.Navigation;

            MyPlayerName = game.Players[0].Name;

            OnAnswerSelectedCommand = new Command((isCorrect) =>
            {
                if ((bool)isCorrect)
                {
                    game.Players[0].Score++;
                }
            });

            void NextQuestion()
            {
                navigation.PushAsync(new GamePage());
                return;
            }

        }
            

=======
        public QuestionViewModel() //did remove game param should just be one instance
        {
        Question question = new WhoIsThatPokemon();
        question.CreateQuestion();
        //Question question = new DumyQuestion(); 
        
        if (question.Answers.Count >= 4)
        {
            AnswerOneText = question.Answers[0].Value;
            AnswerTwoText = question.Answers[1].Value;
            AnswerThreeText = question.Answers[2].Value;
            AnswerFourText = question.Answers[3].Value;
        }
        else
        {
            // Handle the case when there are not enough answers available
            // You can assign default values or display an error message
          
            AnswerOneText = "Default answer 1";
            AnswerTwoText = "Default answer 2";
            AnswerThreeText = "Default answer 3";
            AnswerFourText = "Default answer 4";
        }
        
        
        /*AnswerOneText = "Hans";
        AnswerTwoText = "Hans";
        AnswerThreeText = "Hans";
        AnswerFourText = "Hans"; */
            
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
>>>>>>> Stashed changes
    }
}
