using PokemonMasterminds.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;


namespace PokemonMasterminds.ViewModels
{
    class LobbyViewModel : INotifyPropertyChanged
    {
        public InputLobbyViewModel InputViewModel { get; }
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
            StartGameCommand = new Command(StartGame);
            PlayerList = game.Players.ToList();
        }

       
        private void StartGame()
        {
            // Start the game for all players in the same lobby

            // Here, you can access the lobby code or any other identifier for the lobby
            // and use it to determine the players in the same lobby

            // Example: Starting the game for all players in the PlayerList
            foreach (Player player in PlayerList)
            {
                // Start the game for the player
                StartPlayerGame(player);
            }
        }

        private void StartPlayerGame(Player player)
        {
            // Implement the logic to start the game for a specific player
        }



        public ObservableCollection<Player> Players { get; private set; }

    }
}
