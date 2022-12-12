using UnityEngine;

namespace Gameplay.Board.Actions
{
	public static class GameActionsExtensions
	{
		public static bool IsPositive(this GameAction action) => 
			action == GameAction.Defence || action == GameAction.Heal;
		
		public static bool IsNegative(this GameAction action) => 
			action == GameAction.Attack || action == GameAction.Poison;

		public static GameAction GetRandomAction() => 
			(GameAction) Random.Range(0, 4);
	}
}