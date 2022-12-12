using Data.StaticData.Windows;
using Gameplay.Board;
using UI.Services;

namespace Gameplay.Game
{
	public class GameStatesObserver
	{
		private readonly BoardSituationChecker _boardSituationChecker;
		private readonly IWindowService _windowService;

		public GameStatesObserver(BoardSituationChecker boardSituationChecker, IWindowService windowService)
		{
			_boardSituationChecker = boardSituationChecker;
			_windowService = windowService;
			
			Subscribe();
		}

		private void Subscribe()
		{
			_boardSituationChecker.OnPlayerWin += () => _windowService.Open(WindowId.WinWindow);
			_boardSituationChecker.OnPlayerLose += () => _windowService.Open(WindowId.LoseWindow);
		}
		
	}
}