using UnityEngine;

namespace Data.StaticData.BoardSettings
{
	[CreateAssetMenu(fileName = "BoardSettingsData", menuName = "StaticData/BoardSettingsData", order = 0)]
	public class BoardSettingsStaticData : ScriptableObject
	{
		[Range(1, 5)] public int AllyTeamSize = 3;
		[Range(1, 5)] public int EnemyTeamSize = 3;
	}
}