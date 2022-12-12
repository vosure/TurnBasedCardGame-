using Data.StaticData.Characters;
using Gameplay.Board.Actions;
using Gameplay.Cards;
using Gameplay.CharactersSystem;
using Gameplay.CharactersSystem.HealthSystem;
using Gameplay.CharactersSystem.InfluenceSystem;
using Infrastructure.AssetManagement;
using Infrastructure.StaticData;
using UI.Elements;
using UnityEngine;

namespace Infrastructure.Factories
{
	public class GameFactory : IGameFactory
	{
		private readonly IAssetsProvider _assetsProvider;
		private readonly IStaticDataService _staticDataService;

		public GameFactory(IAssetsProvider assetsProvider, IStaticDataService staticDataService)
		{
			_assetsProvider = assetsProvider;
			_staticDataService = staticDataService;
		}

		public Character CreateAllyCharacter(Transform spawnPosition) =>
			SetUpCharacter(_assetsProvider.GetAlly(), spawnPosition);

		public Character CreateEnemyCharacter(Transform spawnPosition) =>
			SetUpCharacter(_assetsProvider.GetEnemy(), spawnPosition);

		public Card CreateRandomCard(Transform spawnPosition)
		{
			GameAction randomAction = GameActionsExtensions.GetRandomAction();
			return Object.Instantiate(_assetsProvider.GetCard(randomAction), spawnPosition.position, spawnPosition.rotation);
		}

		private Character SetUpCharacter(Character characterPrefab, Transform spawnPosition)
		{
			Character newCharacter = Object.Instantiate(characterPrefab, spawnPosition.position, spawnPosition.rotation,
				spawnPosition);

			CharacterType characterType =
				newCharacter.Team == Team.Ally ? CharacterType.DefaultAlly : CharacterType.DefaultEnemy;
			CharacterStaticData characterStaticData = _staticDataService.GetDataForCharacter(characterType);

			IHealth health = newCharacter.GetComponent<IHealth>();
			health.StartingHealth = characterStaticData.StartingHealth;
			health.CurrentHealth = characterStaticData.StartingHealth;

			CharacterInfluencesContainer influencesContainer =
				newCharacter.GetComponent<CharacterInfluencesContainer>();
			
			newCharacter.GetComponentInChildren<CharacterUI>().Construct(health, influencesContainer);
			
			return newCharacter;
		}
	}
}