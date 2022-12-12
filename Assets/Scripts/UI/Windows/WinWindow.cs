using Infrastructure.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
	public class WinWindow : WindowBase
	{
		[SerializeField] private Button playAgainButton; 
		private ISceneLoaderService _sceneLoaderService;
		
		public void Construct(ISceneLoaderService sceneLoaderService) => 
			_sceneLoaderService = sceneLoaderService;

		protected override void Subscribe()
		{
			base.Subscribe();
			
			playAgainButton.onClick.AddListener(RestartGame);
		}

		protected override void CleanUp()
		{
			base.CleanUp();
			
			playAgainButton.onClick.RemoveListener(RestartGame);
		}

		private void RestartGame() => 
			_sceneLoaderService.ReloadCurrent();
	}
}