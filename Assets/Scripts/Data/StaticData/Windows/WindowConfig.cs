using System;
using UI.Windows;

namespace Data.StaticData.Windows
{
	[Serializable]
	public class WindowConfig
	{
		public string Name; // NOTE: For editor
		public WindowId WindowId;
		public WindowBase Prefab;
	}
}