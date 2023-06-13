using PokemonMasterminds.Model;
using Microsoft.Maui.Controls;
using PokemonMasterminds.ViewModels;

namespace PokemonMasterminds.Pages
{
    public partial class InputLobby : ContentPage
    {
        public InputLobby(InputLobbyViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}