using UnityEngine;

namespace Gameplay.CharactersSystem.Presentation
{
	public class CharacterView : MonoBehaviour
	{
		[SerializeField] private GameObject highlightCircle;

		public void EnableHighlight() => 
			highlightCircle.SetActive(true);

		public void DisableHighlight() => 
			highlightCircle.SetActive(false);
	}
}
