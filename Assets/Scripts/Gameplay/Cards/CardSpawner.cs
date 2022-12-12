using Gameplay.Battle;
using Gameplay.CharactersSystem;
using Infrastructure.Factories;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay.Cards
{
	public class CardSpawner
	{
		private readonly IGameFactory _gameFactory;
		private readonly BattleTurnsHandler _battleTurnsHandler;
		private readonly Transform _cardSpawnPosition;

		private Card _cardOnBoard;

		public CardSpawner(IGameFactory gameFactory, BattleTurnsHandler battleTurnsHandler, Transform cardSpawnPosition)
		{
			_gameFactory = gameFactory;
			_battleTurnsHandler = battleTurnsHandler;
			_cardSpawnPosition = cardSpawnPosition;

			Subscribe();
		}

		private void Subscribe()
		{
			_battleTurnsHandler.OnPlayerTurnStarted += CreateCard;
			_battleTurnsHandler.OnTeamTurnStarted += DestroyCardIfExistOnEnemyTurn;
		}

		private void CreateCard()
		{
			DestroyCardIfExists();
			_cardOnBoard = _gameFactory.CreateRandomCard(_cardSpawnPosition);
		}

		private void DestroyCardIfExists()
		{
			if (_cardOnBoard)
				Object.Destroy(_cardOnBoard.gameObject);
		}

		private void DestroyCardIfExistOnEnemyTurn(Team team)
		{
			if (team == Team.Enemy)
				DestroyCardIfExists();
		}
	}
}