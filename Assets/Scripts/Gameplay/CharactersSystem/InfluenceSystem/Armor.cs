using Gameplay.CharactersSystem.HealthSystem;

namespace Gameplay.CharactersSystem.InfluenceSystem
{
	public class Armor : Influence
	{
		private readonly IHealth _health;

		public Armor(int value, int turnsToExpire, IHealth health) : base(value, turnsToExpire)
		{
			Type = InfluenceType.Armor;
			_health = health;
			_health.TemporalHealth = value;
			
			Subscribe();
		}

		private void Subscribe() => 
			_health.OnTemporalHealthChanged += (int temporalHealth) => Update(temporalHealth, TurnsToExpire);

		public override bool IsExpired()
		{
			bool expired = base.IsExpired();
			if (expired) 
				_health.TemporalHealth = 0;

			return expired;
		}
	}
}