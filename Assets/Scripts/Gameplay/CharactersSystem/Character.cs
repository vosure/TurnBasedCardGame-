using Gameplay.CharactersSystem.Presentation;
using UnityEngine;

namespace Gameplay.CharactersSystem
{
	public class Character : MonoBehaviour
	{
		[SerializeField] private Team team;
		
		private CharacterAction _action;
		private CharacterView _characterView;

		public Team Team => team;
		public CharacterAction Action => _action;
		public CharacterView View => _characterView;

		private void Awake()
		{
			_action = GetComponent<CharacterAction>();
			_characterView = GetComponent<CharacterView>();
		}
	}
}