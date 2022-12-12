using System;
using System.Linq;
using Gameplay.CharactersSystem;

namespace Gameplay.Board
{
	public class BoardSituationChecker
	{
		private readonly GameBoard _board;

		public event Action OnPlayerWin;
		public event Action OnPlayerLose;

		public BoardSituationChecker(GameBoard board)
		{
			_board = board;

			Subscribe();
		}

		private void Subscribe() => 
			_board.OnCharactersUpdated += IsAnyTeamLeft;

		private void IsAnyTeamLeft()
		{
			if (IsAllEnemiesDead()) 
				OnPlayerWin?.Invoke();

			if (IsAllAlliesDead()) 
				OnPlayerLose?.Invoke();
		}

		private bool IsAllEnemiesDead() =>
			_board.GetAllCharacters().All(c => c.Team != Team.Enemy);

		private bool IsAllAlliesDead() =>
			_board.GetAllCharacters().All(c => c.Team != Team.Ally);
	}
}