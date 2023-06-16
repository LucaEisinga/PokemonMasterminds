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
        //GetQuestionCommand : ICommand
        public ICommand GetQuestionCommand { private set; get; }

        //OnAnswerSelectedCommand : ICommand

        public ICommand OnAnswerSelectedCommand { private set; get; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            

    }
}
