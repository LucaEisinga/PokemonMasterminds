namespace PokemonMasterminds.Pages;

public partial class InputLobby : ContentPage
{
	public InputLobby()
	{
		InitializeComponent();
	}

    async void CreateLobbyClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.Lobby());
    }

    async void JoinLobbyClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.Lobby());
    }
}