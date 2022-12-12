namespace Gameplay.CharactersSystem
{
	public static class TeamExtension
	{
		public static Team GetOpposite(this Team team) => 
			team == Team.Ally ? Team.Enemy : Team.Ally;
	}
}