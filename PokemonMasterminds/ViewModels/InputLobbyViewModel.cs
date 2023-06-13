using PokemonMasterminds.Model;
using PokemonMasterminds.Pages;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PokemonMasterminds.ViewModels
{
    public class InputLobbyViewModel : LobbyViewModel
    {
        private string lobbyCode;
        private string playerName;
        private ICommand joinLobbyCommand;
        private ICommand createLobbyCommand;
        private ObservableCollection<Player> players = new ObservableCollection<Player>();

        public string LobbyCode
        {
            get { return lobbyCode; }
            set { SetProperty(ref lobbyCode, value); }
        }

        public string PlayerName
        {
            get { return playerName; }
            set { SetProperty(ref playerName, value); }
        }

        public ICommand JoinLobbyCommand => joinLobbyCommand ?? (joinLobbyCommand = new Command(JoinLobby));

        public ObservableCollection<Player> Player
        {
            get { return players; }
            set { SetProperty(ref players, value); }
        }

        private readonly INavigation navigation;

        public InputLobbyViewModel(INavigation navigation, Game game)
            : base(game)
        {
            this.navigation = navigation;
        }

        private async void JoinLobby()
        {
            if (string.IsNullOrEmpty(PlayerName))
            {
                return;
            }

            Players.Add(new Player(PlayerName));
            PlayerName = "";

            if (Players.Count == 0)
            {
                return;
            }

            Game game = new Game
            {
                Players = Players
            };

            await navigation.PushAsync(new Lobby(game));
        }
        public ICommand CreateLobbyCommand => createLobbyCommand ?? (createLobbyCommand = new Command(CreateLobby));

        private void CreateLobby()
        {
            // Logic to create a lobby
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}