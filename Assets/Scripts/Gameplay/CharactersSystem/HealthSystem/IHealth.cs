namespace Gameplay.CharactersSystem.HealthSystem
{
	public interface IHealth
	{
		int StartingHealth { get; set; }
		int CurrentHealth { get; set; }

		int TemporalHealth { get; set; }
		bool IsDead { get; set; }

		event System.Action OnDeath;
		event System.Action<int> OnHealthChanged;
		event System.Action<int> OnTemporalHealthChanged;

		void Die();
		void TakeDamage(int value);
		void Heal(int value);
	}
}