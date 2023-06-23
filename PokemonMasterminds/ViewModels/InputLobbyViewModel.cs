using PokemonMasterminds.Model;
using PokemonMasterminds.Pages;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PokemonMasterminds.ViewModels
{
    public class InputLobbyViewModel : INotifyPropertyChanged
    {
        private string lobbyCode;
        private string playerName;
        private Command createLobbyCommand;
        private Command joinLobbyCommand;
        private string fullLobbyText;

        private ObservableCollection<Player> players = new ObservableCollection<Player>();
        private Dictionary<string, ObservableCollection<Player>> lobbyPlayers = new Dictionary<string, ObservableCollection<Player>>();

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

        public ObservableCollection<Player> Players
        {
            get { return players; }
            set { SetProperty(ref players, value); }
        }

        public string FullLobbyText
        {
            get { return fullLobbyText; }
            set { SetProperty(ref fullLobbyText, value); }
        }

        private readonly INavigation navigation;
        public readonly Game game;
        private readonly LobbyWebSocketClient webSocketClient;

        public InputLobbyViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            game = new Game();
            webSocketClient = new LobbyWebSocketClient();
        }

        public ICommand JoinLobbyCommand => joinLobbyCommand ?? (joinLobbyCommand = new Command<object>(JoinLobby));
        private async void JoinLobby(object parameter)
        {
            if (string.IsNullOrEmpty(PlayerName) || string.IsNullOrEmpty(LobbyCode))
            {
                return;
            }

            var filteredPlayers = lobbyPlayers.ContainsKey(LobbyCode) ? lobbyPlayers[LobbyCode] : new ObservableCollection<Player>();
            Players = new ObservableCollection<Player>(filteredPlayers);

            if (filteredPlayers.Count > 3)
            {
                FullLobbyText = "Deze lobby zit vol";
                return;
            }
            else
            {
                Players.Add(new Player(PlayerName));
            }
            
            PlayerName = "";

            if (Players.Count == 0)
            {
                return;
            }

            game.Lobby.Players = new ObservableCollection<Player>(Players.ToList());

            if (!lobbyPlayers.ContainsKey(LobbyCode))
            {
                lobbyPlayers[LobbyCode] = new ObservableCollection<Player>();
            }
            lobbyPlayers[LobbyCode].Clear();
            foreach (var player in Players)
            {
                lobbyPlayers[LobbyCode].Add(player);
            }

            await CreateLobby(LobbyCode);
            await navigation.PushAsync(new Lobby(game));
        }

        public ICommand CreateLobbyCommand => createLobbyCommand ?? (createLobbyCommand = new Command<string>(async (code) =>
        {
            string lobbyCode = code;

            await CreateLobby(lobbyCode);
        }));


        private async Task CreateLobby(string lobbyCode)
        {
            try
            {
                // Connect to the WebSocket server
                await webSocketClient.ConnectAsync(new Uri("ws://192.168.56.1:8000"), System.Threading.CancellationToken.None);
                // Send a message to create the lobby with the lobby code
                await webSocketClient.SendAsync($"create_lobby {lobbyCode}", System.Threading.CancellationToken.None);

                // Send additional messages or perform other operations as needed
                var message = "Your additional message";
                await webSocketClient.SendAsync(message, System.Threading.CancellationToken.None);

                // Wait for the server response or handle the response in the ReceiveLoopAsync method

                // Disconnect from the WebSocket server
                await webSocketClient.DisconnectAsync();

                // Create the game object with the players
                Game game = new Game();
                foreach (var player in Players)
                {
                    game.Lobby.Players.Add(player);
                }

                await navigation.PushAsync(new Lobby(game));
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