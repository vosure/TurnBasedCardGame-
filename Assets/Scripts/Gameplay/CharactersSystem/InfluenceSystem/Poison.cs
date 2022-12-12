using Gameplay.CharactersSystem.HealthSystem;

namespace Gameplay.CharactersSystem.InfluenceSystem
{
	public class Poison : Influence, IActivateEffectInfluence
	{
		private readonly IHealth _health;

		public Poison(int value, int turnsToExpire, IHealth health) : base(value, turnsToExpire)
		{
			Type = InfluenceType.Poison;
			_health = health;
		}

		public void Activate() =>
			_health.TakeDamage(Value);
	}
}