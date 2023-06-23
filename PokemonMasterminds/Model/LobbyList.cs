using System.Collections.ObjectModel;

namespace PokemonMasterminds.Model;

public class LobbyList
{
	public ObservableCollection<Player> Players;
	public bool IsLeader { get; private set; }
	
	public LobbyList()
	{
		Players = new ObservableCollection<Player>();
	}
}