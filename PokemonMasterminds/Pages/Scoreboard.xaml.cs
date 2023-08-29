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

    //verwijs speler terug naar startscherm vanaf scoreboard scherm
    async void OnStartPageClicked(object sender, EventArgs e)
    {
	    SoundPlayer.Instance.PlayPlinkSound();
	    
	    Game game = Game.Instance;
	    game.GameIsActive = false;
        await Navigation.PushAsync(new Pages.Lobby(Game.Instance));
    }
}