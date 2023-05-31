namespace PokemonMasterminds.Pages;

public partial class GamePage : ContentPage
{
	public GamePage()
	{
		InitializeComponent();
	}

    async void OnAnswerOneClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.Scoreboard());
    }

    async void OnAnswerTwoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.Scoreboard());
    }

    async void OnAnswerThreeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.Scoreboard());
    }

    async void OnAnswerFourClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.Scoreboard());
    }
}