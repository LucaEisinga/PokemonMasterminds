using PokemonMasterminds.Model;
using PokemonMasterminds.Pages;
using PokemonMasterminds.ViewModels;
using Plugin.Maui.Audio;

namespace PokemonMasterminds
{
    partial class MainPage : ContentPage
    {
        public MainPage()
        {
            SoundPlayer.Instance.PlayBackgroundSound();
            InitializeComponent();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            SoundPlayer.Instance.PlayPlinkSound();
            var viewModel = new InputLobbyViewModel(Navigation);
            await Navigation.PushAsync(new InputLobby(viewModel));
        }
    }
}