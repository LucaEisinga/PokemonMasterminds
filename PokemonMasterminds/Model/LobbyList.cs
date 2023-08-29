using System.Collections.ObjectModel;

namespace PokemonMasterminds.Model;

//lobbylist class, hierin wordt de speler bewaard. Mocht het spel nog multiplayer modes krijgen kan dit uitgebreid worden tot het behouden van meerdere players
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