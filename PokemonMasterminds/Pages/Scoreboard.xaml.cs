using PokemonMasterminds.Model;
using PokemonMasterminds.ViewModels;

namespace PokemonMasterminds.Pages;

public partial class Scoreboard : ContentPage
{
	InputLobbyViewModel viewModel;

    public Scoreboard(Game game)
	{
		BindingContext = new ScoreboardViewModel(game);
		InitializeComponent();
	}

    async void OnStartPageClicked(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new Pages.InputLobby(viewModel));
    }
}