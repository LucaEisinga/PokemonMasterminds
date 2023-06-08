using PokemonMasterminds.ViewModels;
using PokemonMasterminds.Model;

namespace PokemonMasterminds.Pages;

public partial class Lobby
{
	public Lobby(Game game)
	{
        LobbyViewModel viewmodel = new(game);
        BindingContext = viewmodel;
        InitializeComponent();
    }

    async void OnRedirectClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.GamePage());
    }
}