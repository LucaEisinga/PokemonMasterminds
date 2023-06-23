using PokemonMasterminds.Model;
using PokemonMasterminds.ViewModels;

namespace PokemonMasterminds.Pages;

public partial class Scoreboard : ContentPage
{
	public Scoreboard(Game game)
	{
		BindingContext = new ScoreboardViewModel(game);
		InitializeComponent();
	}
}