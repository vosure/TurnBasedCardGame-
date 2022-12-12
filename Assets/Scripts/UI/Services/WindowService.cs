using System;
using Data.StaticData.Windows;

namespace UI.Services
{
	public class WindowService : IWindowService
	{
		private readonly IUIFactory _uiFactory;

		public WindowService (IUIFactory uiFactory) => 
			_uiFactory = uiFactory;

		public void Open(WindowId windowId)
		{
			switch (windowId)
			{
				case WindowId.WinWindow:
					_uiFactory.CreateWinWindow();
					break;
				case WindowId.LoseWindow:
					_uiFactory.CreateLoseWindow();
					break;
			}
		}
	}
}