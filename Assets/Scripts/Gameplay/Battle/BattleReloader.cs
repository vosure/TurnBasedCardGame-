using Infrastructure.InputSystem;
using Infrastructure.SceneManagement;

namespace Gameplay.Battle
{
	public class BattleReloader
	{
		private readonly IInputService _inputService;
		private readonly ISceneLoaderService _sceneLoaderService;

		public BattleReloader(IInputService inputService, ISceneLoaderService sceneLoaderService)
		{
			_inputService = inputService;
			_sceneLoaderService = sceneLoaderService;

			Subscribe();
		}

		private void Subscribe() => 
			_inputService.OnBattleReloadPressed += RestartBattle;

		private void RestartBattle() => 
			_sceneLoaderService.ReloadCurrent();
	}
}