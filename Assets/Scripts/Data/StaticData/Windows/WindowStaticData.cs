using System.Collections.Generic;
using UnityEngine;

namespace Data.StaticData.Windows
{
	[CreateAssetMenu(fileName = "WindowsData", menuName = "StaticData/WindowsData", order = 0)]
	public class WindowStaticData : ScriptableObject
	{
		public List<WindowConfig> Configs;
	}
}