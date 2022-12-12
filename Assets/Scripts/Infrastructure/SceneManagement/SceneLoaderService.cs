using System;
using System.Collections;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneManagement
{
	public class SceneLoaderService : ISceneLoaderService
	{
		private readonly ICoroutineRunner _coroutineRunner;

		public SceneLoaderService(ICoroutineRunner coroutineRunner) =>
			_coroutineRunner = coroutineRunner;

		public void ReloadCurrent() => 
			Load(SceneManager.GetActiveScene().name);

		public void Load(string name, Action onLoaded = null) =>
			_coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

		private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
		{
			AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

			while (!waitNextScene.isDone)
				yield return null;

			onLoaded?.Invoke();
		}
	}
}