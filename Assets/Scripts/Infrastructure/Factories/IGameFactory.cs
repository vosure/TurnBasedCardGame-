using Gameplay.Cards;
using Gameplay.CharactersSystem;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Factories
{
	public interface IGameFactory : IService
	{
		Character CreateAllyCharacter(Transform spawnPosition);
		Character CreateEnemyCharacter(Transform spawnPosition);
		Card CreateRandomCard(Transform spawnPosition);
	}
}