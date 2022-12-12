using Gameplay.CharactersSystem.HealthSystem;
using Gameplay.CharactersSystem.InfluenceSystem;
using UnityEngine;

namespace Gameplay.CharactersSystem
{
	public class CharacterAction : MonoBehaviour
	{
		private IHealth _health;

		private CharacterInfluencesContainer _influencesContainer;

		private void Awake()
		{
			_health = GetComponent<IHealth>();
			_influencesContainer = GetComponent<CharacterInfluencesContainer>();
		}

		public void TakeDamage(int amount) => 
			_health.TakeDamage(amount);

		public void Heal(int amount)
		{
			_health.Heal(amount);
			
			_influencesContainer.TryRemoveInfluence(InfluenceType.Poison);
		}

		public void AddArmor(int amount, int turns) => 
			_influencesContainer.AddNewInfluence(InfluenceType.Armor, amount, turns);

		public void GetPoisoned(int initialDamage, int damagePerTurn, int turns)
		{
			_health.TakeDamage(initialDamage);
			
			_influencesContainer.AddNewInfluence(InfluenceType.Poison, damagePerTurn, turns);
		}

		public void DecrementAndPlayInfluences()
		{
			_influencesContainer.PlayActiveInfluences();
			_influencesContainer.DecrementExpireTurns();
		}
	}
}