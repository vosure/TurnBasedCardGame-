using Gameplay.Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
	public class HudWindow : WindowBase
	{
		[SerializeField] private Button endTurnButton;
		
		private PlayerTurnsHandler _playerTurnsHandler;

		public void Construct(PlayerTurnsHandler playerTurnsHandler) => 
			_playerTurnsHandler = playerTurnsHandler;

		protected override void Subscribe()
		{
			base.Subscribe();

			_playerTurnsHandler.OnTurnAvailable += EnableButton;
			_playerTurnsHandler.OnTurnUnavailable += DisableButton;
			
			endTurnButton.onClick.AddListener(TryEndTurn);
		}

		protected override void CleanUp()
		{
			base.CleanUp();
			
			_playerTurnsHandler.OnTurnAvailable -= EnableButton;
			_playerTurnsHandler.OnTurnUnavailable -= DisableButton;
			
			endTurnButton.onClick.RemoveListener(TryEndTurn);
		}

		private void TryEndTurn() => 
			_playerTurnsHandler.TryEndTurn();

		private void EnableButton() => 
			SetButtonActiveState(true);

		private void DisableButton() => 
			SetButtonActiveState(false);

		private void SetButtonActiveState(bool state) => 
			endTurnButton.gameObject.SetActive(state);
	}
}