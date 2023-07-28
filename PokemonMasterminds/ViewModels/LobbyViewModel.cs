using PokemonMasterminds.Model;
using PokemonMasterminds.Pages;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace PokemonMasterminds.ViewModels
{
    class LobbyViewModel : INotifyPropertyChanged
    {
        //public InputLobbyViewModel InputViewModel { get; }
       // public List<Player> PlayerList { get; set; }

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
            StartGameCommand = new Command(StartGame);
            //PlayerList = game.Lobby.Player.ToList();
        }

       
        private async void StartGame()
        {
            INavigation navigation = App.Current.MainPage.Navigation;

            Game game = Game.Instance;
            game.GameIsActive = true;

            await navigation.PushAsync(new GamePage());
        }

    }
}
