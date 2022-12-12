using Data.StaticData.BoardSettings;
using Gameplay.AI;
using Gameplay.Battle;
using Gameplay.Board;
using Gameplay.Board.Actions;
using Gameplay.Cards;
using Gameplay.Player;
using Infrastructure.Factories;
using Infrastructure.InputSystem;
using Infrastructure.SceneManagement;
using Infrastructure.Services;
using Infrastructure.StaticData;
using UI.Services;
using UnityEngine;

namespace Gameplay.Game
{
	public class GameInitializer : MonoBehaviour
	{
		[SerializeField] private CardDragAndDrop dragAndDrop;

		[SerializeField] private Transform cardSpawnPosition;
		
		[SerializeField] Transform[] allyTeamPositions;
		[SerializeField] Transform[] enemyTeamPositions;
		
		private BattleTurnsHandler _battleTurnsHandler;
		private CardSpawner _cardSpawner;

		private GameBoard _gameBoard;
		private BoardActionsPerformer _boardActionsPerformer;
		private BoardSituationChecker _boardSituationChecker;
		private InfluencesActivator _influencesActivator;
		private EnemyAITurnsHandler _enemyAI;
		private PlayerTurnsHandler _playerTurnsHandler;
		private GameStatesObserver _gameStatesObserver;
		private BattleReloader _battleReloader;
		
		private IGameFactory _gameFactory;
		private IStaticDataService _staticDataService;
		private IInputService _inputService;
		private ICoroutineRunner _coroutineRunner;
		private IUIFactory _uiFactory;
		private IWindowService _windowService;
		private ISceneLoaderService _sceneLoaderService;

		public void Construct(
			IGameFactory gameFactory, 
			IStaticDataService staticDataService, 
			IInputService inputService,
			IUIFactory uiFactory,
			IWindowService windowService,
			ISceneLoaderService sceneLoaderService,
			ICoroutineRunner coroutineRunner)
		{
			_windowService = windowService;
			_uiFactory = uiFactory;
			_inputService = inputService;
			_gameFactory = gameFactory;
			_staticDataService = staticDataService;
			_sceneLoaderService = sceneLoaderService;
			_coroutineRunner = coroutineRunner;
		}

		public void Initialize()
		{
			SetUpGame();
			StartGame();
		}

		private void SetUpGame()
		{
			SetUpGameBoard();
			SetUpBattle();
			SetUpTurnHandlers();
			SetUpCards();
			SetUpGameUI();
		}

		private void StartGame()
		{
			InitializeBoard();
			_battleTurnsHandler.StartBattle();
		}

		private void SetUpGameBoard()
		{
			_gameBoard = new GameBoard(_gameFactory);
			_boardSituationChecker = new BoardSituationChecker(_gameBoard);
		}

		private void SetUpBattle()
		{
			_battleTurnsHandler = new BattleTurnsHandler(_gameBoard);
			_influencesActivator = new InfluencesActivator(_gameBoard, _battleTurnsHandler);
			_boardActionsPerformer = new BoardActionsPerformer(_staticDataService);
			
			_gameStatesObserver = new GameStatesObserver(_boardSituationChecker, _windowService);
			_battleReloader = new BattleReloader(_inputService, _sceneLoaderService);
		}

		private void SetUpTurnHandlers()
		{
			_enemyAI = new EnemyAITurnsHandler(_gameBoard, _battleTurnsHandler, _boardActionsPerformer, _coroutineRunner);
			_playerTurnsHandler = new PlayerTurnsHandler(_battleTurnsHandler, _inputService);
		}

		private void SetUpCards()
		{
			_cardSpawner = new CardSpawner(_gameFactory, _battleTurnsHandler, cardSpawnPosition);
			dragAndDrop.Construct(_inputService, _battleTurnsHandler, _boardActionsPerformer);
		}

		private void SetUpGameUI() => 
			_uiFactory.CreateHud(_playerTurnsHandler);

		private void InitializeBoard()
		{
			BoardSettingsStaticData boardSettings = _staticDataService.GetBoardSettings();
			_gameBoard.InitializeBoard(boardSettings.AllyTeamSize, boardSettings.EnemyTeamSize, allyTeamPositions,
				enemyTeamPositions);
		}
	}
}