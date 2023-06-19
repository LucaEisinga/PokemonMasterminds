using PokemonMasterminds.Model;
using PokemonMasterminds.Pages;
using PokemonMasterminds.ViewModels;

namespace PokemonMasterminds
{
    partial class MainPage : ContentPage
    {
       public MainPage()
        {
            InitializeComponent();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            var viewModel = new InputLobbyViewModel(Navigation);
            await Navigation.PushAsync(new InputLobby(viewModel));
        }
    }
}