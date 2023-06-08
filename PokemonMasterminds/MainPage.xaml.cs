using PokemonMasterminds.Pages;
namespace PokemonMasterminds
{

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        

        async void OnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InputLobby(new ViewModels.InputLobbyViewModel()));
        }
    }

    
}