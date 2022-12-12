using System;
using Infrastructure.Services;

namespace Infrastructure.SceneManagement
{
	public interface ISceneLoaderService : IService
	{
		void Load(string name, Action onLoaded = null);
		void ReloadCurrent();
	}
}