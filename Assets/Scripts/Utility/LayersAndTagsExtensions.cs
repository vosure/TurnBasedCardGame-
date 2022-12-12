using UnityEngine;

namespace Utility
{
	public static class LayersAndTagsExtensions
	{
		public static LayerMask NameToLayerMask(string name) => 
			1 << LayerMask.NameToLayer(name);
	}
}