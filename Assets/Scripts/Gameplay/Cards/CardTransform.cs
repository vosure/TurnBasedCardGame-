using UnityEngine;

namespace Gameplay.Cards
{
	public class CardTransform : MonoBehaviour
	{
		private const float FlyHeight = 1.5f;
		
		private Vector3 _initialScale;
		private Vector3 _initialPosition;
		private Vector3 _dragScale;
		public void Awake()
		{
			var thisTransform = transform;
			_initialPosition = thisTransform.position;
			_initialScale = thisTransform.localScale;
			
			_dragScale = _initialScale * 1.15f;
		}

		public void StartDrag() => 
			transform.localScale = _dragScale;

		public void ResetPosition()
		{
			transform.position = _initialPosition;
			transform.localScale = _initialScale;
		}

		public void UpdatePosition(Vector3 newPosition)
		{
			Vector3 targetPosition = newPosition;
			targetPosition.y = FlyHeight;

			transform.position = targetPosition;
		}
	}
}