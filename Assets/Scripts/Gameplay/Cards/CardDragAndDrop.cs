using Gameplay.Battle;
using Gameplay.Board.Actions;
using Gameplay.CharactersSystem;
using Infrastructure.InputSystem;
using UnityEngine;
using Utility;

namespace Gameplay.Cards
{
	public class CardDragAndDrop : MonoBehaviour
	{
		private const string CardLayerMaskName = "Card";
		private const string CharacterLayerMaskName = "Character";
		private const float RaycastDistance = 999.0f;

		private Card _currentDraggingCard;
		private LayerMask _cardLayerMask;
		private LayerMask _characterLayerMask;

		private IInputService _inputService;
		private BattleTurnsHandler _battleTurnsHandler;
		private BoardActionsPerformer _boardActionsPerformer;

		private bool _updateDragInput;

		public void Construct(
			IInputService inputService,
			BattleTurnsHandler battleTurnsHandler,
			BoardActionsPerformer boardActionsPerformer)
		{
			_inputService = inputService;
			_battleTurnsHandler = battleTurnsHandler;
			_boardActionsPerformer = boardActionsPerformer;

			_updateDragInput = true;
		}

		private void Awake() => 
			SetUpLayerMasks();

		private void Update()
		{
			if (!_updateDragInput)
				return;
			if (!_currentDraggingCard)
			{
				if (_inputService.IsTouching)
					TryStartDrag(_inputService.TouchRay);
			}
			else
			{
				Ray ray = _inputService.TouchRay;
				if (_inputService.IsTouching)
					UpdateDrag(ray);
				else
					EndDrag(ray);
			}
		}

		private void TryStartDrag(Ray ray)
		{
			if (IsRayCollidedWithCard(ray, out Card card))
			{
				_currentDraggingCard = card;
				_currentDraggingCard.CardTransform.StartDrag();
			}
		}

		private void UpdateDrag(Ray ray)
		{
			if (IsRayCollidedWithGround(ray, out Vector3 position))
				_currentDraggingCard.CardTransform.UpdatePosition(position);
		}

		private void EndDrag(Ray ray)
		{
			if (IsRayCollidedWithCharacter(ray, out Character character))
				HandleCardDropOnCharacter(character);
			else
				ReturnCardToInitialState();

			ResetDraggingInput();
		}

		private void HandleCardDropOnCharacter(Character targetCharacter)
		{
			Character currentTurnCharacter = _battleTurnsHandler.CurrentTurnCharacter;
			GameAction action = _currentDraggingCard.ActionType;

			bool isActionAllowed =
				_boardActionsPerformer.IsActionAllowed(currentTurnCharacter, targetCharacter, action);

			if (isActionAllowed)
			{
				_boardActionsPerformer.PerformAction(targetCharacter, action);
				Destroy(_currentDraggingCard.gameObject);
			}
			else
				ReturnCardToInitialState();
		}

		private void ReturnCardToInitialState() =>
			_currentDraggingCard.CardTransform.ResetPosition();

		private void ResetDraggingInput() => 
			_currentDraggingCard = null;

		private bool IsRayCollidedWithCard(Ray ray, out Card card)
		{
			card = null;
			if (IsRayHitColliderWithLayer(ray, _cardLayerMask, out RaycastHit hit))
			{
				if (hit.collider.TryGetComponent(out Card hitCard))
				{
					card = hitCard;
					return true;
				}
			}

			return false;
		}

		private bool IsRayCollidedWithCharacter(Ray ray, out Character character)
		{
			character = null;
			if (IsRayHitColliderWithLayer(ray, _characterLayerMask, out RaycastHit hit))
			{
				if (hit.collider.TryGetComponent(out Character hitCharacter))
				{
					character = hitCharacter;
					return true;
				}
			}

			return false;
		}

		private bool IsRayCollidedWithGround(Ray ray, out Vector3 hitPosition)
		{
			hitPosition = Vector3.zero;
			if (IsRayHitColliderWithLayer(ray, _inputService.PlaneLayerMask, out RaycastHit hit))
			{
				hitPosition = hit.point;
				return true;
			}

			return false;
		}

		private bool IsRayHitColliderWithLayer(Ray ray, LayerMask mask, out RaycastHit raycastHit)
		{
			raycastHit = new RaycastHit();
			if (Physics.Raycast(ray, out RaycastHit hit, RaycastDistance, mask))
			{
				raycastHit = hit;
				return true;
			}

			return false;
		}

		private void SetUpLayerMasks()
		{
			_cardLayerMask = LayersAndTagsExtensions.NameToLayerMask(CardLayerMaskName);
			_characterLayerMask = LayersAndTagsExtensions.NameToLayerMask(CharacterLayerMaskName);
		}
	}
}