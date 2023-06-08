using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PokemonMasterminds.Model;

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

        public QuestionViewModel(Game game)
        {

        }
    }
}
