using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PokemonMasterminds.ViewModels
{
    public class LobbyViewModel : INotifyPropertyChanged
    {
        private string _player1Name;
        private string _player2Name;

        public string Player1Name
        {
            get => _player1Name;
            set
            {
                if (_player1Name != value)
                {
                    _player1Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Player2Name
        {
            get => _player2Name;
            set
            {
                if (_player2Name != value)
                {
                    _player2Name = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}