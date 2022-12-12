using System;
using System.Collections.Generic;
using Gameplay.CharactersSystem;
using Gameplay.CharactersSystem.HealthSystem;
using Infrastructure.Factories;
using UnityEngine;

namespace Gameplay.Board
{
	public class GameBoard
	{
		private readonly IGameFactory _gameFactory;
		private readonly List<Character> _characters = new();

		public event Action OnCharactersUpdated;

		public GameBoard(IGameFactory gameFactory) =>
			_gameFactory = gameFactory;

		public List<Character> GetAllCharacters() =>
			_characters;

		public void InitializeBoard(int allyTeamSize, int enemyTeamSize, Transform[] allyTeamPosition,
			Transform[] enemyTeamPositions)
		{
			CreateTeam(allyTeamSize, Team.Ally, allyTeamPosition);
			CreateTeam(enemyTeamSize, Team.Enemy, enemyTeamPositions);

			SubscribeToDeath();
		}

		private void CreateTeam(int teamSize, Team teamType, Transform[] positions)
		{
			for (int i = 0; i < teamSize; i++)
			{
				var character = teamType == Team.Ally
					? _gameFactory.CreateAllyCharacter(positions[i])
					: _gameFactory.CreateEnemyCharacter(positions[i]);

				_characters.Add(character);
			}
		}

		private void SubscribeToDeath()
		{
			foreach (Character character in _characters)
				character.GetComponent<IHealth>().OnDeath += () => OnCharacterDead(character);
		}

		private void OnCharacterDead(Character character)
		{
			_characters.Remove(character);
			OnCharactersUpdated?.Invoke();
		}
	}
}