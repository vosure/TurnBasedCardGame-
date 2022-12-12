using System;
using Gameplay.Battle;
using Gameplay.CharactersSystem;
using Infrastructure.InputSystem;

namespace Gameplay.Player
{
	public class PlayerTurnsHandler
	{
		private readonly BattleTurnsHandler _battleTurnsHandler;
		private readonly IInputService _inputService;

		public event Action OnTurnAvailable;
		public event Action OnTurnUnavailable;

		private bool _canEndTurn;

		public PlayerTurnsHandler(BattleTurnsHandler battleTurnsHandler, IInputService inputService)
		{
			_battleTurnsHandler = battleTurnsHandler;
			_inputService = inputService;
			
			Subscribe();
		}

		public void TryEndTurn()
		{
			if (_canEndTurn)
				_battleTurnsHandler.MoveToNextCharacterTurn();
		}

		private void Subscribe()
		{
			_battleTurnsHandler.OnTeamTurnStarted += SetPlayerTurnState;
			_inputService.OnEndTurnPressed += TryEndTurn;
		}

		private void SetPlayerTurnState(Team currentTeam)
		{
			_canEndTurn = currentTeam == Team.Ally;
			
			if (_canEndTurn)
				OnTurnAvailable?.Invoke();
			else 
				OnTurnUnavailable?.Invoke();
		}
	}
}