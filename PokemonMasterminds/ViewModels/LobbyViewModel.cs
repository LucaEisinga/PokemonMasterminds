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
        }

       
        private async void StartGame()
        {
            SoundPlayer.Instance.PlayPlinkSound();
            
            INavigation navigation = App.Current.MainPage.Navigation;

            Game game = Game.Instance;
            game.GameIsActive = true;

            await navigation.PushAsync(new GamePage());
        }

    }
}
