using System;
using DG.Tweening;
using Gameplay.CharactersSystem;
using Gameplay.CharactersSystem.HealthSystem;
using Gameplay.CharactersSystem.InfluenceSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI.Elements
{
	public class CharacterUI : MonoBehaviour
	{
		private const float DefaultFillTime = 1.0f;
		
		[SerializeField] private Image healthBarImage;
		[SerializeField] private TMP_Text healthText;
		
		[SerializeField] private Transform influencesHolder;

		[SerializeField] private InfluenceUIEntity influenceEntityPrefabs;
		[SerializeField] private Sprite poisonIcon; // HACK
		[SerializeField] private Sprite armorIcon; // HACK
		
		private IHealth _health;
		private CharacterInfluencesContainer _influencesContainer;

		private int _maxHealth;

		public void Construct(IHealth health, CharacterInfluencesContainer influencesContainer)
		{
			_health = health;
			_influencesContainer = influencesContainer;
			
			InitializeHealthBar(health);

			Subscribe();
		}

		private void InitializeHealthBar(IHealth health)
		{
			_maxHealth = _health.StartingHealth;
			SetHealthText();
		}

		private void OnDestroy() => 
			Unsubscribe();

		private void Subscribe()
		{
			_health.OnHealthChanged += UpdateHealthBar;
			_influencesContainer.OnInfluencesUpdated += UpdateInfluences;
		}

		private void Unsubscribe()
		{
			_health.OnHealthChanged -= UpdateHealthBar;
			_influencesContainer.OnInfluencesUpdated -= UpdateInfluences;
		}

		private void UpdateInfluences()
		{
			influencesHolder.Clear();

			foreach (var influences in _influencesContainer.Influences.Values)
			{
				InfluenceUIEntity influenceUIEntity = Instantiate(influenceEntityPrefabs, influencesHolder);
				influenceUIEntity.Initialize(GetInfluenceSprite(influences.Type), influences.Value, influences.TurnsToExpire);
			}
		}

		private Sprite GetInfluenceSprite(InfluenceType type) => // HACK
			type == InfluenceType.Armor ? armorIcon : poisonIcon;

		private void UpdateHealthBar(int value)
		{
			float remainingHealthPercent = ((float) value / _maxHealth);
			healthBarImage.DOFillAmount(remainingHealthPercent, DefaultFillTime);
			
			SetHealthText();
		}

		private void SetHealthText() => 
			healthText.text = _health.CurrentHealth + " / " + _maxHealth;
	}
}