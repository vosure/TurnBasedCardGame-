using UnityEngine;

namespace Data.StaticData.Characters
{
	[CreateAssetMenu(fileName = "CharacterData", menuName = "StaticData/CharacterData", order = 0)]
	public class CharacterStaticData : ScriptableObject
	{
		public CharacterType CharacterType;
		public int StartingHealth;
	}
}