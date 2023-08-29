using System.Collections.ObjectModel;

namespace PokemonMasterminds.Model;

public class LobbyList
{
	public Player Player { get; set; }
	public bool IsLeader { get; private set; }

	public LobbyList()
	{
		
	}

	public void AddPlayer(Player player)
	{
		this.Player = player;
	}
}