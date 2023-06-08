using PokemonMasterminds.Model;
using PokemonMasterminds.ViewModels;

namespace PokemonMasterminds.Pages;

public partial class InputLobby : ContentPage
{
	public InputLobby(InputLobbyViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}

}