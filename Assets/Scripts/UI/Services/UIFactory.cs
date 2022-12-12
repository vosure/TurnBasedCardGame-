using Data.StaticData.Windows;
using Gameplay.Player;
using Infrastructure.AssetManagement;
using Infrastructure.SceneManagement;
using Infrastructure.StaticData;
using UI.Windows;
using UnityEngine;

namespace UI.Services
{
	public class UIFactory : IUIFactory
	{
		private readonly IAssetsProvider _assetsProvider;
		private readonly IStaticDataService _staticDataService;
		private readonly ISceneLoaderService _sceneLoaderService;
		
		private Transform _uiRoot;
		
		public UIFactory(IAssetsProvider assetsProvider, IStaticDataService staticDataService, ISceneLoaderService sceneLoaderService)
		{
			_assetsProvider = assetsProvider;
			_staticDataService = staticDataService;
			_sceneLoaderService = sceneLoaderService;
		}

		public void CreateHud(PlayerTurnsHandler playerTurnsHandler)
		{
			_uiRoot = Object.Instantiate(_assetsProvider.GetUIRootObject()).transform;
			
			WindowConfig config = _staticDataService.GetWindowConfig(WindowId.Hud);
			HudWindow hud = Object.Instantiate(config.Prefab, _uiRoot) as HudWindow;
			hud.Construct(playerTurnsHandler);
		}

		public void CreateWinWindow()
		{
			WindowConfig config = _staticDataService.GetWindowConfig(WindowId.WinWindow);
			WinWindow winWindow = Object.Instantiate(config.Prefab, _uiRoot) as WinWindow;
			winWindow.Construct(_sceneLoaderService);
		}

		public void CreateLoseWindow()
		{
			WindowConfig config = _staticDataService.GetWindowConfig(WindowId.LoseWindow);
			LoseWindow loseWindow = Object.Instantiate(config.Prefab, _uiRoot) as LoseWindow;
			loseWindow.Construct(_sceneLoaderService);
		}
	}
}