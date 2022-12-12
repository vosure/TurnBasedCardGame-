using System;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.InputSystem
{
	public interface IInputService : IService
	{
		Ray TouchRay { get; }
		Vector3 TouchPosition { get; }
		bool IsTouching { get; }
		LayerMask PlaneLayerMask { get; }

		event Action OnEndTurnPressed;
		event Action OnBattleReloadPressed;
	}
}