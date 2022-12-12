using Data.StaticData.CardEffects;
using Gameplay.CharactersSystem;
using Infrastructure.StaticData;

namespace Gameplay.Board.Actions
{
	public class BoardActionsPerformer
	{
		private readonly CardEffectsStaticData _cardEffectsStaticData;

		public BoardActionsPerformer(IStaticDataService staticDataService) => 
			_cardEffectsStaticData = staticDataService.GetCardEffectsData();

		public void PerformAction(Character targetCharacter, GameAction actionType) => 
			ApplyActionOnCharacter(targetCharacter.Action, actionType);

		public bool IsActionAllowed(Character character1, Character character2, GameAction actionType)
		{
			bool isSameTeam = character1.Team == character2.Team;

			if (actionType.IsPositive() && isSameTeam)
				return true;
			if (actionType.IsNegative() && !isSameTeam)
				return true;

			return false;
		}

		private void ApplyActionOnCharacter(CharacterAction character, GameAction action)
		{
			switch (action)
			{
				case GameAction.Attack:
					character.TakeDamage(_cardEffectsStaticData.AttackDamage);
					break;
				case GameAction.Defence:
					character.AddArmor(_cardEffectsStaticData.ArmorValue,
						_cardEffectsStaticData.ArmorActiveTurnsNumber);
					break;
				case GameAction.Heal:
					character.Heal(_cardEffectsStaticData.HealValue);
					break;
				case GameAction.Poison:
					character.GetPoisoned(_cardEffectsStaticData.PoisonInitialDamage,
						_cardEffectsStaticData.PoisonDamagePerTurn, _cardEffectsStaticData.PoisonActiveTurnsNumber);
					break;
			}
		}
	}
}