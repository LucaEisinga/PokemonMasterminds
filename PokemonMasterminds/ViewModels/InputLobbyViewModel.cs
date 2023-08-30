using PokemonMasterminds.Model;
using PokemonMasterminds.Pages;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PokemonMasterminds.ViewModels
{
    
    //viewmodel class voor de input lobby, hier wordt de naam van speler ingevoerd
    public class InputLobbyViewModel : INotifyPropertyChanged
    {
        private string _lobbyCode;
        private string _playerName;
        private Command _createLobbyCommand;
        private string _noNameText;

        private ObservableCollection<Player> _players = new ObservableCollection<Player>();

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

        public string NoNameText
        {
            get { return _noNameText; }
            set { SetProperty(ref _noNameText, value); }
        }

        private readonly INavigation _navigation;
        private Game _game;

        public InputLobbyViewModel(INavigation navigation)
        {
            this._navigation = navigation;
            _game = Game.Instance;
        }

        public ICommand CreateLobbyCommand => _createLobbyCommand ??= new Command<string>(async (code) =>
        {
            string lobbyCode = code;

            await CreateLobby(lobbyCode);
        });

        //Er wordt gecheckt of de speler zijn naam wel heeft ingevuld, zo niet komt er een melding.
        //Als zijn naam is ingevuld, wordt er een lobby aangemaakt met de speler en wordt de speler doorgestuurd naar zijn lobby.
        private async Task CreateLobby(string lobbyCode)
        {
            SoundPlayer.Instance.PlayPlinkSound();

            if (string.IsNullOrEmpty(PlayerName))
            {
                NoNameText = "Please fill in a name!";
                return;
            }

            NoNameText = string.Empty;

            try
            {
                // Create the game object with the players
                Player player = new Player(PlayerName);
                
                _game.Lobby.AddPlayer(player);
                
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