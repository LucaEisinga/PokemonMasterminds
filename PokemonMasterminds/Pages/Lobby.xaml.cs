namespace PokemonMasterminds.Pages;

public partial class Lobby : ContentPage
{
	public Lobby()
	{
		InitializeComponent();
	}

    async void OnRedirectClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.GamePage());
    }
}