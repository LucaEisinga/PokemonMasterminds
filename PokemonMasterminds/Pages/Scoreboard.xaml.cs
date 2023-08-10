using PokemonMasterminds.Model;
using PokemonMasterminds.ViewModels;

namespace PokemonMasterminds.Pages;

public partial class Scoreboard : ContentPage
{
	InputLobbyViewModel viewModel;

    public Scoreboard()
	{
		BindingContext = new ScoreboardViewModel();
		InitializeComponent();
	}

    async void OnStartPageClicked(object sender, EventArgs e)
    {
	    Game game = Game.Instance;
	    game.GameIsActive = false;
        await Navigation.PushAsync(new Pages.Lobby(Game.Instance));
    }
}