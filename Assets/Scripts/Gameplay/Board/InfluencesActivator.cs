using System.Collections.Generic;
using System.Linq;
using Gameplay.Battle;
using Gameplay.CharactersSystem;

namespace Gameplay.Board
{
	public class InfluencesActivator
	{
		private readonly GameBoard _board;
		private readonly BattleTurnsHandler _battleTurnsHandler;

		public InfluencesActivator(GameBoard board, BattleTurnsHandler battleTurnsHandler)
		{
			_board = board;
			_battleTurnsHandler = battleTurnsHandler;
			
			Subscribe();
		}

		private void Subscribe() => 
			_battleTurnsHandler.OnTeamTurnStarted += PlayActiveInfluencesInTeam;

		private void PlayActiveInfluencesInTeam(Team team)
		{
			List<Character> characters = _board.GetAllCharacters().Where(c => c.Team == team).ToList();

			foreach (Character character in characters)
				character.Action.DecrementAndPlayInfluences();
		}
	}
}