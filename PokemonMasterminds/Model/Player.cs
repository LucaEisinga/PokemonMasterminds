using System.Security.Cryptography.X509Certificates;

namespace PokemonMasterminds.Model;

public class Player
{
	public string Name { get; set; }
	public int Score { get; set; }
	public Player(String name)
	{
		Name = name;
		Score = 0;
	}
}