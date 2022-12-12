using System.Collections.Generic;
using System.Linq;
using Data.StaticData;
using Data.StaticData.BoardSettings;
using Data.StaticData.CardEffects;
using Data.StaticData.Characters;
using Data.StaticData.Windows;
using Infrastructure.AssetManagement;
using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace Infrastructure.StaticData
{
	public class StaticDataService : IStaticDataService
	{
		private Dictionary<CharacterType, CharacterStaticData> _charactersData;
		private CardEffectsStaticData _cardEffectsData;
		private BoardSettingsStaticData _boardSettingsData;
		private Dictionary<WindowId, WindowConfig> _windowConfigs = new();

		public CharacterStaticData GetDataForCharacter(CharacterType type) =>
			_charactersData.TryGetValue(type, out CharacterStaticData staticData)
				? staticData
				: null;

		public WindowConfig GetWindowConfig(WindowId windowId) =>
			_windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig)
				? windowConfig
				: null;

		public CardEffectsStaticData GetCardEffectsData() =>
			_cardEffectsData;

		public BoardSettingsStaticData GetBoardSettings() =>
			_boardSettingsData;
		
		public void LoadAll()
		{
			LoadCharacterData();
			LoadCardEffectsData();
			LoadBoardSettingsData();
			LoadWindowsConfigs();
		}

		private void LoadWindowsConfigs() =>
			_windowConfigs = Resources
				.Load<WindowStaticData>(AssetPath.WindowsConfigsPath)
				.Configs
				.ToDictionary(x => x.WindowId, x => x);

		private void LoadCharacterData() =>
			_charactersData = Resources
				.LoadAll<CharacterStaticData>(AssetPath.CharactersStaticDataPath)
				.ToDictionary(c => c.CharacterType, c => c);

		private void LoadCardEffectsData() =>
			_cardEffectsData = Resources.Load<CardEffectsStaticData>(AssetPath.CardEffectsStaticDataPath);

		private void LoadBoardSettingsData() =>
			_boardSettingsData = Resources.Load<BoardSettingsStaticData>(AssetPath.BoardSettingsStaticDataPath);
	}
}