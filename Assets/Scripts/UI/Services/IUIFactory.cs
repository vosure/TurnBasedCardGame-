using Gameplay.Player;
using Infrastructure.Services;

namespace UI.Services
{
	public interface IUIFactory : IService
	{
		void CreateHud(PlayerTurnsHandler playerTurnsHandler);
		void CreateWinWindow();
		void CreateLoseWindow();
	}
}