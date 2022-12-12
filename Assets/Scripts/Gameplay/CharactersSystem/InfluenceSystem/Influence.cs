using System;

namespace Gameplay.CharactersSystem.InfluenceSystem
{
	public abstract class Influence : IInfluence
	{
		public InfluenceType Type { get; protected set; }
		public int Value { get; private set; }
		public int TurnsToExpire { get; private set; }

		public event Action OnInfluenceLeft;
		public event Action OnInfluenceUpdated;

		public void DecrementExpireTurns() => 
			Update(Value, TurnsToExpire - 1);

		public virtual bool IsExpired() =>
			TurnsToExpire <= 0;
		
		private bool IsLeft() =>
			Value <= 0;

		protected Influence(int value, int turnsToExpire)
		{
			Value = value;
			TurnsToExpire = turnsToExpire;
		}

		public void Update(int value, int turnsToExpire)
		{
			Value = value;
			TurnsToExpire = turnsToExpire;
			
			if (IsLeft() || IsExpired())
				OnInfluenceLeft?.Invoke();
			else
				OnInfluenceUpdated?.Invoke();
		}
	}
}