namespace PokemonMasterminds;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    async void OnButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.InputLobby());
    }
}