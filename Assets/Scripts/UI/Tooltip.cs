/*
 *	Created on 8/15/2017 6:59:08 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using Gameplay.Items;
using Gameplay.Items.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class Tooltip : MonoBehaviour
	{
		private Item item;

		[SerializeField]
		private GameObject tooltip;

		[SerializeField]
		private Color nameColor;

		[SerializeField]
		private Color descriptionColor;

		[SerializeField]
		private Color valueColor;

		private string data;

		private void Start()
		{
			if (Inventory.Instance.IsActive)
				tooltip.SetActive(false);
		}

		private void Update()
		{
			if (Inventory.Instance.IsActive && tooltip.activeSelf)
				tooltip.transform.position = Input.mousePosition;
		}

		public void Activate(Item item)
		{
			this.item = item;
			ConstructString();
			tooltip.SetActive(true);
		}

		public void Deactivate()
		{
			tooltip.SetActive(false);
		}

		public void ConstructString()
		{
			tooltip.GetComponentInChildren<Text>().text = item.ToHTML(nameColor, descriptionColor, valueColor);
		}
	}
}