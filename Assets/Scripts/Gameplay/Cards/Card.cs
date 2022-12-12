using Gameplay.Board.Actions;
using UnityEngine;

namespace Gameplay.Cards
{
	public class Card : MonoBehaviour
	{
		[SerializeField] private GameAction actionType;

		private CardTransform _cardTransform;

		public CardTransform CardTransform => _cardTransform;
		public GameAction ActionType => actionType;

		private void Awake() => 
			_cardTransform = GetComponent<CardTransform>();
	}
}