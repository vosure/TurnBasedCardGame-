using Data.StaticData.Windows;
using Infrastructure.Services;

namespace UI.Services
{
	public interface IWindowService : IService
	{
		void Open(WindowId windowId);
	}
}