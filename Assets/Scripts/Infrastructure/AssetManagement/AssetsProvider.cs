using System.Collections.Generic;
using System.Linq;
using Gameplay.Board.Actions;
using Gameplay.Cards;
using Gameplay.CharactersSystem;
using UnityEngine;
using Utility;

namespace Infrastructure.AssetManagement
{
	public class AssetsProvider : IAssetsProvider
	{
		private List<Character> _enemyCharacters;
		private List<Character> _playerCharacters;

		private Dictionary<GameAction, Card> _cards;

		public void LoadAll()
		{
			LoadCharacters();
			LoadCards();
		}

		public Character GetEnemy() =>
			GetCharacterByType(Team.Enemy);

		public Character GetAlly() =>
			GetCharacterByType(Team.Ally);

		public Card GetCard(GameAction actionType) => 
			_cards.TryGetValue(actionType, out Card card) ? card : null;

		private Character GetCharacterByType(Team teamType) =>
			teamType == Team.Ally ? _playerCharacters.GetRandom() : _enemyCharacters.GetRandom();

		public GameObject GetUIRootObject() => 
			Resources.Load<GameObject>(AssetPath.UiRootObject);
		
		private void LoadCharacters()
		{
			_enemyCharacters = Resources.LoadAll<Character>(AssetPath.EnemiesPrefabsPath).ToList();
			_playerCharacters = Resources.LoadAll<Character>(AssetPath.AlliesPrefabsPath).ToList();
		}

		private void LoadCards() => 
			_cards = Resources.LoadAll<Card>(AssetPath.CardsPrefabsPath).ToDictionary(c => c.ActionType, c => c);
	}
}