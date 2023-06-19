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
        private LobbyWebSocketClient webSocketClient;
        private string lobbyCode;
        private string playerName;
        private Command createLobbyCommand;
        private Command joinLobbyCommand;

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

        public ObservableCollection<Player> Players
        {
            get { return players; }
            set { SetProperty(ref players, value); }
        }

        private readonly INavigation navigation;
        private readonly Game game;

        public InputLobbyViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            game = new Game();
            webSocketClient = new LobbyWebSocketClient();
        }

        public ICommand JoinLobbyCommand => joinLobbyCommand ?? (joinLobbyCommand = new Command<object>(JoinLobby));

        private async void JoinLobby(object parameter)
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

            game.Players = new ObservableCollection<Player>(Players.ToList());

            await CreateLobby();
            await navigation.PushAsync(new Lobby(game));
        }

        public ICommand CreateLobbyCommand => createLobbyCommand ?? (createLobbyCommand = new Command(async () =>
        {
            await CreateLobby();

            game.Players = new ObservableCollection<Player>(Players.ToList());

            await navigation.PushAsync(new Lobby(game));
        }));

        private async Task CreateLobby()
        {
            try
            {
                // Disconnect from the WebSocket server if already connected
                if (webSocketClient.State == WebSocketState.Open)
                {
                    await webSocketClient.DisconnectAsync();
                }

                // Connect to the WebSocket server
                await webSocketClient.ConnectAsync(new Uri("ws://localhost"), System.Threading.CancellationToken.None);
                // Send a message to create the lobby
                await webSocketClient.SendAsync("create_lobby", System.Threading.CancellationToken.None);

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
                    game.Players.Add(player);
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