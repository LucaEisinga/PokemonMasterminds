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
            Task.Run(async () => await LoadGameQuestions());
        }
        public string _buttonText { get; set; }

        public string ButtonText
        {
            get { return _buttonText; }
            set { _buttonText = value; OnPropertyChanged(); }
        }

        private async Task LoadGameQuestions() {
            await Game.Instance.FillQuestionList();
            ButtonText = "Start Game!";
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
