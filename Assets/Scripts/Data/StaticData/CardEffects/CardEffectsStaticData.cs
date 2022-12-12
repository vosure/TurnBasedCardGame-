using UnityEngine;

namespace Data.StaticData.CardEffects
{
	[CreateAssetMenu(fileName = "CardEffectsData", menuName = "StaticData/CardEffectsData", order = 0)]
	public class CardEffectsStaticData : ScriptableObject
	{
		public int AttackDamage;

		public int ArmorValue;
		public int ArmorActiveTurnsNumber;

		public int HealValue;

		public int PoisonInitialDamage;
		public int PoisonDamagePerTurn;
		public int PoisonActiveTurnsNumber;
	}
}