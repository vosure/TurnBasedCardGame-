using Gameplay.Board.Actions;
using Gameplay.Cards;
using Gameplay.CharactersSystem;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.AssetManagement
{
	public interface IAssetsProvider : IService
	{
		void LoadAll();
		Character GetEnemy();
		Character GetAlly();
		Card GetCard(GameAction actionType);
		GameObject GetUIRootObject();
	}
}