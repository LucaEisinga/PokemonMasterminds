using PokemonMasterminds.Model;
using PokemonMasterminds.Pages;
using PokemonMasterminds.ViewModels;

namespace PokemonMasterminds
{
    partial class MainPage : ContentPage
    {
         MainPage()
        {
            InitializeComponent();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            var game = new Game(); // Create a new Game object
            var viewModel = new InputLobbyViewModel(Navigation, game); // Pass the Game object to the constructor
            await Navigation.PushAsync(new InputLobby(viewModel));
        }
    }
}