using PokemonMasterminds.Model;
using PokemonMasterminds.Pages;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PokemonMasterminds.ViewModels
{
    public class InputLobbyViewModel : INotifyPropertyChanged
    {
        private string _lobbyCode;
        private string _playerName;
        private Command _createLobbyCommand;
        private Command _joinLobbyCommand;
        private string _fullLobbyText;

        private ObservableCollection<Player> _players = new ObservableCollection<Player>();
        private Dictionary<string, ObservableCollection<Player>> _lobbyPlayers = new Dictionary<string, ObservableCollection<Player>>();

        public string LobbyCode
        {
            get { return _lobbyCode; }
            set { SetProperty(ref _lobbyCode, value); }
        }

        public string PlayerName
        {
            get { return _playerName; }
            set { SetProperty(ref _playerName, value); }
        }

        public ObservableCollection<Player> Players
        {
            get { return _players; }
            set { SetProperty(ref _players, value); }
        }

        public string FullLobbyText
        {
            get { return _fullLobbyText; }
            set { SetProperty(ref _fullLobbyText, value); }
        }

        private readonly INavigation _navigation;
        private Game _game;

        public InputLobbyViewModel(INavigation navigation)
        {
            this._navigation = navigation;
            _game = Game.Instance;
        }

        public ICommand JoinLobbyCommand => _joinLobbyCommand ??= new Command<object>(JoinLobby);
        private async void JoinLobby(object parameter)
        {
            if (string.IsNullOrEmpty(PlayerName) || string.IsNullOrEmpty(LobbyCode))
            {
                return;
            }

            var filteredPlayers = _lobbyPlayers.ContainsKey(LobbyCode) ? _lobbyPlayers[LobbyCode] : new ObservableCollection<Player>();
            
            if (filteredPlayers.Count > 3)
            {
                FullLobbyText = "Deze lobby zit vol";
                return;
            }
            else
            {
                _game.Lobby.AddPlayer(new Player(PlayerName));
            }
            
            PlayerName = "-";

            if (Players.Count == 0)
            {
                return;
            }

            _game.Lobby.Players = new ObservableCollection<Player>(Players.ToList());

            if (!_lobbyPlayers.ContainsKey(LobbyCode))
            {
                _lobbyPlayers[LobbyCode] = new ObservableCollection<Player>();
            }
            _lobbyPlayers[LobbyCode].Clear();
            foreach (var player in Players)
            {
                _lobbyPlayers[LobbyCode].Add(player);
            }

            await CreateLobby(LobbyCode);
            await _navigation.PushAsync(new Lobby(_game));
        }

        public ICommand CreateLobbyCommand => _createLobbyCommand ??= new Command<string>(async (code) =>
        {
            string lobbyCode = code;

            await CreateLobby(lobbyCode);
        });


        private async Task CreateLobby(string lobbyCode)
        {
            try
            {
                // Create the game object with the players
                foreach (var player in Players)
                {
                    _game.Lobby.AddPlayer(player);
                }

                await _navigation.PushAsync(new Lobby(_game));
            }
            catch (Exception ex)
            {
                // Handle the exception or display an error message
                Console.WriteLine($"An error occurred while creating the lobby: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }

        private void SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}