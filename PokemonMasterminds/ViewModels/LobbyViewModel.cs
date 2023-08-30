using PokemonMasterminds.Model;
using PokemonMasterminds.Pages;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace PokemonMasterminds.ViewModels
{
    //viewmodel voor de lobby, vanaf hieruit wordt de game geladen en kan er op start worden gedrukt
    class LobbyViewModel : INotifyPropertyChanged
    {
        public ICommand StartGameCommand { private set; get; }

        public ICommand LeaveLobbyCommand { private set; get; }
        
        private string[] loadingGifs = { "waitingcharmander.gif", "ghastlyevocircle.gif", "delivierypikachu.gif", "circlingmew.gif" };

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

            ButtonText = "Questions are loading...";
            
            //weergeef 1 random gif uit de lijst als loading icon
            Random random = new Random();
            int randomIndex = random.Next(loadingGifs.Length);
            LoadingGif = loadingGifs[randomIndex];
            
            Task.Run(async () => await LoadGameQuestions());
        }
        public string _buttonText { get; set; }

        public string ButtonText
        {
            get { return _buttonText; }
            set { _buttonText = value; OnPropertyChanged(); }
        }

        public string _loadingGif { get; set; }

        public string LoadingGif
        {
            get { return _loadingGif; }
            set { _loadingGif = value; OnPropertyChanged(); }
        }

        private async Task LoadGameQuestions() {
            await Game.Instance.FillQuestionList();
            ButtonText = "Start Game!";
            LoadingGif = "-";
        }

        //methode voor starten van de Game,
        //hierbij wordt gekeken of de questions al zijn ingeladen, zo ja begint spel na drukken van de start game knop
        private async void StartGame()
        {
            SoundPlayer.Instance.PlayPlinkSound();

            if (Game.Instance.IsQuestionsListFilled())
            {
                INavigation navigation = App.Current.MainPage.Navigation;

                Game game = Game.Instance;
                game.GameIsActive = true;

                await navigation.PushAsync(new GamePage());
            }
            
        }

    }
}
