using Gameplay.Game;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using Infrastructure.InputSystem;
using Infrastructure.SceneManagement;
using Infrastructure.Services;
using Infrastructure.StaticData;
using UI.Services;
using UnityEngine;

namespace Infrastructure.Bootstrap
{
	public class Bootstrapper : MonoBehaviour, ICoroutineRunner
	{
		[SerializeField] private InputService inputService;
		[SerializeField] private GameInitializer gameInitializer;

		private void Awake() =>
			RegisterServices();

		private void Start() =>
			InitializeGame();

		private void RegisterServices()
		{
			ServiceLocator.CreateContainer();
			
			RegisterDataManagementServices();
			RegisterSceneManagementServices();
			RegisterFactories();
			RegisterInputService();
		}

		private static void RegisterDataManagementServices()
		{
			IAssetsProvider assetsProvider = new AssetsProvider();
			assetsProvider.LoadAll();
			ServiceLocator.Container.Register<IAssetsProvider>(assetsProvider);

			IStaticDataService staticDataService = new StaticDataService();
			staticDataService.LoadAll();
			ServiceLocator.Container.Register<IStaticDataService>(staticDataService);
		}

		private void RegisterSceneManagementServices() => 
			ServiceLocator.Container.Register<ISceneLoaderService>(new SceneLoaderService(this));

		private static void RegisterFactories()
		{
			var assetsProvider = ServiceLocator.Container.Get<IAssetsProvider>();
			var staticData = ServiceLocator.Container.Get<IStaticDataService>();

			ServiceLocator.Container.Register<IGameFactory>(new GameFactory(assetsProvider, staticData));
			ServiceLocator.Container.Register<IUIFactory>(new UIFactory(assetsProvider, staticData, ServiceLocator.Container.Get<ISceneLoaderService>()));
			ServiceLocator.Container.Register<IWindowService>(new WindowService(ServiceLocator.Container.Get<IUIFactory>()));
		}

		private void RegisterInputService() => 
			ServiceLocator.Container.Register<IInputService>(inputService);

		private void InitializeGame()
		{
			gameInitializer.Construct(
				ServiceLocator.Container.Get<IGameFactory>(),
				ServiceLocator.Container.Get<IStaticDataService>(),
				ServiceLocator.Container.Get<IInputService>(),
				ServiceLocator.Container.Get<IUIFactory>(),
				ServiceLocator.Container.Get<IWindowService>(),
				ServiceLocator.Container.Get<ISceneLoaderService>(),
				this);

			gameInitializer.Initialize();
		}
	}
}