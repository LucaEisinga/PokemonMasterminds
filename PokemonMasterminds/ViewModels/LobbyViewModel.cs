using PokemonMasterminds.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;


namespace PokemonMasterminds.ViewModels
{
    public class LobbyViewModel: INotifyPropertyChanged
    {
        public List<Player> PlayerList { get; set; }

        //StartGameCommand : ICommand
        public ICommand StartGameCommand { private set; get; }

        //LeaveLobbyCommand : ICommand
        public ICommand LeaveLobbyCommand { private set; get; }

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

        public LobbyViewModel(Game game)
        {
            INavigation navigation = App.Current.MainPage.Navigation;
            
            PlayerList = game.Players.ToList();
        }

        public ObservableCollection<Player> Players { get; private set; }

    }
}
