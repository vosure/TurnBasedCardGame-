using Data.StaticData;
using Data.StaticData.BoardSettings;
using Data.StaticData.CardEffects;
using Data.StaticData.Characters;
using Data.StaticData.Windows;
using Infrastructure.Services;

namespace Infrastructure.StaticData
{
	public interface IStaticDataService : IService
	{
		CharacterStaticData GetDataForCharacter(CharacterType type);
		CardEffectsStaticData GetCardEffectsData();
		BoardSettingsStaticData GetBoardSettings();
		WindowConfig GetWindowConfig(WindowId windowId);
		void LoadAll();
	}
}