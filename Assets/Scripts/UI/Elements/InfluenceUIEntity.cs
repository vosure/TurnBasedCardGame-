using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
	public class InfluenceUIEntity : MonoBehaviour
	{
		[SerializeField] private Image iconImage;
		[SerializeField] private TMP_Text valueText;
		[SerializeField] private TMP_Text turnsToExpireText;

		private int _value;
		private int _turnsToExpire;

		public void Initialize(Sprite iconSprite, int value, int turnsToExpire)
		{
			iconImage.sprite = iconSprite;
			
			UpdateValue(value, turnsToExpire);
			UpdateUIValue();
			
		}

		private void UpdateValue(int value, int turnsToExpire)
		{
			_value = value;
			_turnsToExpire = turnsToExpire;
		}

		private void UpdateUIValue()
		{
			valueText.text = _value.ToString();
			turnsToExpireText.text = _turnsToExpire.ToString();
		}
	}
}