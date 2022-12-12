using System;

namespace Gameplay.CharactersSystem.InfluenceSystem
{
	public interface IInfluence
	{
		InfluenceType Type { get; }
		int Value { get; }
		int TurnsToExpire { get; }
		event Action OnInfluenceLeft;
		event Action OnInfluenceUpdated;
		void Update(int value, int turnsToExpire);

		void DecrementExpireTurns();
		bool IsExpired();
	}
}