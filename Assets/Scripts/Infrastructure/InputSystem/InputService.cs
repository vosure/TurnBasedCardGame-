using System;
using UnityEngine;
using Utility;

namespace Infrastructure.InputSystem
{
	public abstract class InputService : MonoBehaviour, IInputService
	{
		protected const float MaxRaycastDistance = 999.0f;
		private const string GroundInputLayerMask = "Ground";

		public Ray TouchRay { get; protected set; }
		public Vector3 TouchPosition { get; protected set; }
		public bool IsTouching => _startedPress;
		public LayerMask PlaneLayerMask => _planeLayerMask;
		public event Action OnEndTurnPressed;
		public event Action OnBattleReloadPressed;

		protected bool _startedPress;
		protected Vector3 _startTouchPosition;

		protected LayerMask _planeLayerMask;
		protected Camera _mainCamera;

		private void Awake()
		{
			_mainCamera = Camera.main;
			_planeLayerMask = LayersAndTagsExtensions.NameToLayerMask(GroundInputLayerMask);
		}

		private void Update()
		{
			UpdateCursorInput();
			UpdateKeyboardInput();
		}

		protected void ResetInput() => 
			TouchPosition = Vector3.zero;

		protected void InvokeKeyPressedEvent(KeyPressedType keyPressedType)
		{
			switch (keyPressedType)
			{
				case KeyPressedType.EndTurn:
					OnEndTurnPressed?.Invoke();
					break;
				case KeyPressedType.RestartGame:
					OnBattleReloadPressed?.Invoke();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(keyPressedType), keyPressedType, null);
			}
		}


		protected abstract void UpdateCursorInput();
		protected abstract void UpdateKeyboardInput();
	}

	public enum KeyPressedType
	{
		None = -1,
		EndTurn = 0,
		RestartGame = 1
	}
}