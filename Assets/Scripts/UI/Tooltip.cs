/*
 *	Created on 8/15/2017 6:59:08 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using Gameplay.Items;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
	public class Tooltip : MonoBehaviour
	{
		private Item item;
		private GameObject tooltip;

		[SerializeField]
		private Color nameColor;

		private string data;

		private void Start()
		{
			tooltip = GameObject.Find("Tooltip");
			tooltip.SetActive(false);
		}

		private void Update()
		{
			if (tooltip.activeSelf)
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
			// TODO: Make use of ITooltipSerializable, use StringBuilder.
			data = $"<color=#{nameColor.ToHex()}><b>" + item.Name + "</b></color>\n\n" + item.Description + "\nValue: " + item.Value;
			tooltip.GetComponentInChildren<Text>().text = data;
		}
	}
}