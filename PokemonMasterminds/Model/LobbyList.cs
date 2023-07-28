using System.Collections.ObjectModel;

namespace PokemonMasterminds.Model;

public class LobbyList
{
	//public ObservableCollection<Player> Players { get; set; }
	public Player Player { get; set; }
	public bool IsLeader { get; private set; }

	public LobbyList()
	{
		//Players = new ObservableCollection<Player>();
		
	}

	public void AddPlayer(Player player)
	{
		//Players.Add(player);
		this.Player = player;
	}
}