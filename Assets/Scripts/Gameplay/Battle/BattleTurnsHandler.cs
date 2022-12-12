using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Board;
using Gameplay.CharactersSystem;

namespace Gameplay.Battle
{
	public class BattleTurnsHandler
	{
		private const Team DefaultFirstTurnTeam = Team.Ally;

		private readonly GameBoard _board;
		private readonly Dictionary<Team, List<Character>> _charactersOnBoard = new();

		private int _teamTurnCounter;
		private Team _currentTurnTeamType;

		public event Action<Team> OnTeamTurnStarted;
		public event Action OnPlayerTurnStarted;
		public event Action OnEnemyTurnStarted;
		
		public Character CurrentTurnCharacter { get; private set; }

		public BattleTurnsHandler(GameBoard board)
		{
			_board = board;
			
			Subscribe();
		}

		private void Subscribe() => 
			_board.OnCharactersUpdated += GetCharactersFromBoard;

		public void MoveToNextCharacterTurn()
		{
			if (CurrentTurnCharacter)
				CurrentTurnCharacter.View.DisableHighlight();

			if (IsTeamTurnEnded())
			{
				HandleTeamTurnEnded();
			}
			else
			{
				_teamTurnCounter++;
				TryStartTurn();
			}
		}

		public void StartBattle()
		{
			GetCharactersFromBoard();
			SetUpFirstTeamTurn(DefaultFirstTurnTeam);
		}

		private void HandleTeamTurnEnded() =>
			SetUpFirstTeamTurn(_currentTurnTeamType.GetOpposite());

		private void SetUpFirstTeamTurn(Team nextTeam)
		{
			OnTeamTurnStarted?.Invoke(nextTeam);
			_teamTurnCounter = 0;
			_currentTurnTeamType = nextTeam;
			TryStartTurn();
		}

		private void TryStartTurn()
		{
			CurrentTurnCharacter = GetCurrentTurnCharacter();

			if (CurrentTurnCharacter)
				NotifyTurnStarted();
		}

		private void NotifyTurnStarted()
		{
			CurrentTurnCharacter.View.EnableHighlight();

			if (_currentTurnTeamType == Team.Ally)
				OnPlayerTurnStarted?.Invoke();
			else
				OnEnemyTurnStarted?.Invoke();
		}

		private void GetCharactersFromBoard()
		{
			var enemyCharacters = _board.GetAllCharacters().Where(c => c.Team == Team.Enemy).ToList();
			var allyCharacters = _board.GetAllCharacters().Where(c => c.Team == Team.Ally).ToList();

			_charactersOnBoard.Clear();
			_charactersOnBoard.Add(Team.Enemy, enemyCharacters);
			_charactersOnBoard.Add(Team.Ally, allyCharacters);
		}
		
		private bool IsTeamTurnEnded() => 
			_teamTurnCounter + 1 >= GetTeamSize(_currentTurnTeamType);

		private int GetTeamSize(Team teamType) =>
			_charactersOnBoard[teamType].Count;

		private Character GetCurrentTurnCharacter() =>
			TryGetValueFromDictionary(_currentTurnTeamType, _teamTurnCounter);
		
		private Character TryGetValueFromDictionary(Team team, int index)
		{
			if (_charactersOnBoard.TryGetValue(team, out List<Character> characters))
			{
				var currentTeamCharacters = characters.Where(c => c.Team == team).ToList();
				if (currentTeamCharacters.Any())
					return currentTeamCharacters[index];
			}

			return null;
		}
	}
}