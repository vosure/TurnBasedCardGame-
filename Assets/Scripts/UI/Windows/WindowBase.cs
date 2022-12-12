using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
	public abstract class WindowBase : MonoBehaviour
	{
		[SerializeField] private Button closeButton;

		private void Awake() =>
			OnAwake();

		private void Start()
		{
			Initialize();
			Subscribe();
		}

		private void OnDestroy() =>
			CleanUp();

		protected virtual void OnAwake()
		{
			if (closeButton)
				closeButton.onClick.AddListener(() => Destroy(gameObject));
		}

		protected virtual void CleanUp()
		{
			if (closeButton)
				closeButton.onClick.RemoveAllListeners();
		}

		protected virtual void Initialize()
		{
		}

		protected virtual void Subscribe()
		{
		}
	}
}