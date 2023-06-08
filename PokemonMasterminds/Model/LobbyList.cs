using System.Collections.ObjectModel;

namespace PokemonMasterminds.Model;

public class LobbyList
{
	public ObservableCollection<Player> Players;
	
	public LobbyList()
	{
		Players = new ObservableCollection<Player>();
	}
}