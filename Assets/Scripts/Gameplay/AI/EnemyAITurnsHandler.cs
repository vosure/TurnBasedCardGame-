using System.Collections;
using System.Linq;
using Gameplay.Battle;
using Gameplay.Board;
using Gameplay.Board.Actions;
using Gameplay.CharactersSystem;
using Infrastructure.Services;
using UnityEngine;
using Utility;

namespace Gameplay.AI
{
	public class EnemyAITurnsHandler
	{
		private const GameAction DefaultEnemyAction = GameAction.Attack;
		private const float DefaultAttackDelay = 1.0f;

		private readonly GameBoard _gameBoard;
		private readonly BattleTurnsHandler _battleTurnsHandler;
		private readonly BoardActionsPerformer _actionsPerformer;
		
		private readonly ICoroutineRunner _coroutineRunner;

		public EnemyAITurnsHandler(GameBoard gameBoard, BattleTurnsHandler battleTurnsHandler,
			BoardActionsPerformer actionsPerformer, ICoroutineRunner coroutineRunner)
		{
			_gameBoard = gameBoard;
			_battleTurnsHandler = battleTurnsHandler;
			_actionsPerformer = actionsPerformer;
			_coroutineRunner = coroutineRunner;

			Subscribe();
		}

		private void Subscribe() =>
			_battleTurnsHandler.OnEnemyTurnStarted += PerformTurn;

		private void PerformTurn()
		{
			Character currentEnemy = _battleTurnsHandler.CurrentTurnCharacter;
			Character randomTarget = _gameBoard.GetAllCharacters().Where(c => c.Team == Team.Ally).GetRandom();

			if (currentEnemy && randomTarget)
				if (_actionsPerformer.IsActionAllowed(currentEnemy, randomTarget, DefaultEnemyAction))
					_coroutineRunner.StartCoroutine(EnemyAttackCoroutine(randomTarget));
		}

		// HACK: To Have a little delay between enemy Attacks
		private IEnumerator EnemyAttackCoroutine(Character targetPlayer)
		{
			yield return new WaitForSeconds(DefaultAttackDelay);
			_actionsPerformer.PerformAction(targetPlayer, DefaultEnemyAction);
			_battleTurnsHandler.MoveToNextCharacterTurn();
		}
	}
}