using UnityEngine;

namespace Infrastructure.InputSystem
{
	public class PCInputService : InputService
	{
		protected override void UpdateCursorInput()
		{
			TouchRay = GetTouchRay();

			if (Input.GetMouseButtonDown(0))
				StartTouch();
			else if (Input.GetMouseButton(0) && _startedPress)
			{
				if (Input.mousePosition != _startTouchPosition)
					GetTouchPosition();
				else
					ResetInput();
			}
			else if (Input.GetMouseButtonUp(0)) 
				_startedPress = false;
		}

		protected override void UpdateKeyboardInput()
		{
			if (Input.GetKeyDown(KeyCode.Space))
				InvokeKeyPressedEvent(KeyPressedType.EndTurn);
			if (Input.GetKeyDown(KeyCode.R))
				InvokeKeyPressedEvent(KeyPressedType.RestartGame);
		}

		private Ray GetTouchRay() => 
			_mainCamera.ScreenPointToRay(Input.mousePosition);

		private void GetTouchPosition()
		{
			if (Physics.Raycast(TouchRay, out RaycastHit hit, MaxRaycastDistance, _planeLayerMask,
				QueryTriggerInteraction.Ignore))
				TouchPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
		}

		private void StartTouch()
		{
			_startedPress = true;
			_startTouchPosition = Input.mousePosition;
		}
	}
}