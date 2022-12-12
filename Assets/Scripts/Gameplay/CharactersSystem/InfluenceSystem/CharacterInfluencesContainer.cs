using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.CharactersSystem.HealthSystem;
using UnityEngine;

namespace Gameplay.CharactersSystem.InfluenceSystem
{
	public class CharacterInfluencesContainer : MonoBehaviour
	{
		private readonly Dictionary<InfluenceType, IInfluence> _influences = new();
		private readonly List<IActivateEffectInfluence> _activeInfluences = new();

		private IHealth _health;

		public Dictionary<InfluenceType, IInfluence> Influences => _influences;
		public event Action OnInfluencesUpdated;

		private void Awake() =>
			_health = GetComponent<IHealth>();

		public void TryRemoveInfluence(InfluenceType type)
		{
			if (_influences.ContainsKey(type))
				RemoveInfluenceFromList(type);
		}

		public void PlayActiveInfluences()
		{
			foreach (IActivateEffectInfluence activeInfluence in _activeInfluences)
				activeInfluence.Activate();
		}

		public void DecrementExpireTurns()
		{
			foreach (IInfluence influence in _influences.Values.ToList()) 
				influence.DecrementExpireTurns();
		}

		public void AddNewInfluence(InfluenceType type, int value, int turnsToExpire)
		{
			if (_influences.ContainsKey(type))
				UpdateInfluence(type, value, turnsToExpire);
			else
				CreateNewInfluence(type, value, turnsToExpire);
		}

		private void UpdateInfluence(InfluenceType type, int value, int turnsToExpire) => 
			_influences[type].Update(value, turnsToExpire);

		private void CreateNewInfluence(InfluenceType type, int value, int turnsToExpire)
		{
			IInfluence newInfluence = null;
			switch (type)
			{
				case InfluenceType.Armor:
					newInfluence = new Armor(value, turnsToExpire, _health);
					break;
				case InfluenceType.Poison:
					newInfluence = new Poison(value, turnsToExpire, _health);
					break;
			}

			AddInfluenceToList(newInfluence);
		}

		private void AddInfluenceToList(IInfluence newInfluence)
		{
			if (newInfluence is IActivateEffectInfluence activeInfluence)
				_activeInfluences.Add(activeInfluence);

			newInfluence.OnInfluenceLeft += () => TryRemoveInfluence(newInfluence.Type);
			newInfluence.OnInfluenceUpdated += () => OnInfluencesUpdated?.Invoke();
			
			_influences.Add(newInfluence.Type, newInfluence);

			OnInfluencesUpdated?.Invoke();
		}

		
		
		private void RemoveInfluenceFromList(InfluenceType type)
		{
			if (_influences[type] is IActivateEffectInfluence activeInfluence)
				_activeInfluences.Remove(activeInfluence);

			_influences.Remove(type);

			OnInfluencesUpdated?.Invoke();
		}
	}
}