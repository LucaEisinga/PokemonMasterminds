using Microsoft.Maui.Controls;
using PokemonMasterminds.ViewModels;

namespace PokemonMasterminds.Pages
{
    public partial class Lobby : ContentPage
    {
        public Lobby()
        {
            InitializeComponent();
            BindingContext = new LobbyViewModel();
        }

        // Handle button click event or any other logic here
        private void OnRedirectClicked(object sender, EventArgs e)
        {
            // Access player names through the view model
            var viewModel = (LobbyViewModel)BindingContext;
            string player1Name = viewModel.Player1Name;
            string player2Name = viewModel.Player2Name;
            string player3Name = viewModel.Player3Name;
            string player4Name = viewModel.Player4Name;


        }
    }
}
