using PokemonMasterminds.ViewModels;
using System.Windows.Input;

namespace PokemonMasterminds.Pages
{
    public partial class InputLobby : ContentPage
    {
        public InputLobby(InputLobbyViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = new InputLobbyViewModel(Navigation);
        }

        public InputLobbyViewModel ViewModel => (InputLobbyViewModel)BindingContext;
    }
}