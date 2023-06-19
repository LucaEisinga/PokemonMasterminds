using PokemonMasterminds.ViewModels;
using PokemonMasterminds.Model;

namespace PokemonMasterminds.Pages
{
    public partial class Lobby : ContentPage
    {
        public Lobby(Game game)
        {
            InitializeComponent();

            LobbyViewModel viewModel = new LobbyViewModel(game);
            BindingContext = viewModel;
        }

        async void OnRedirectClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage());
        }
    }
}