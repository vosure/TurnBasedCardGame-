using System;
using UnityEngine;

namespace Gameplay.CharactersSystem.HealthSystem
{
	public class CharacterHealth : MonoBehaviour, IHealth
	{
		private int _currentHealth;
		private int _temporalHealth;

		public int StartingHealth { get; set; }

		public int CurrentHealth
		{
			get => _currentHealth;
			set
			{
				_currentHealth = Mathf.Clamp(value, 0, StartingHealth);

				if (value != StartingHealth)
				{
					if (CurrentHealth <= 0 && !IsDead)
						Die();
				}
			}
		}

		public int TemporalHealth
		{
			get => _temporalHealth;
			set
			{
				_temporalHealth = Mathf.Clamp(value, 0, Int32.MaxValue);
				OnTemporalHealthChanged?.Invoke(_temporalHealth);
			}
		}

		public bool IsDead { get; set; }

		public event System.Action OnDeath;
		public event System.Action<int> OnHealthChanged;
		public event System.Action<int> OnTemporalHealthChanged;

		public void TakeDamage(int value)
		{
			if (TemporalHealth > 0)
			{
				if (TemporalHealth - value < 0)
				{
					CurrentHealth -= Mathf.Abs(TemporalHealth - value);
					OnHealthChanged?.Invoke(CurrentHealth);
				}
				TemporalHealth -= value;
			}
			else
			{
				CurrentHealth -= value;
				OnHealthChanged?.Invoke(CurrentHealth);
			}
		}

		public void Heal(int value)
		{
			CurrentHealth += value;
			OnHealthChanged?.Invoke(CurrentHealth);
		}

		public void Die()
		{
			IsDead = true;
			OnDeath?.Invoke();

			OnDie();
		}

		private void OnDie() =>
			gameObject.SetActive(false);
	}
}