/*
 *	Created on 8/15/2017 2:27:56 AM
 *	Project Rogue by Irakli Chkuaseli
 */

using Gameplay.Items.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
	public class Slot : MonoBehaviour, IDropHandler
	{
		private Inventory inventory;

		public int Index { get; set; }

		/// <inheritdoc />
		public void OnDrop(PointerEventData eventData)
		{
			var data = eventData.pointerDrag.GetComponent<ItemData>();

			// Check if the item exists in this slot.
			if (inventory.Items[Index] == null)
			{
				// If the slot is empty just assign the new item.
				data.transform.SetParent(transform);
				data.transform.position = transform.position;
			}
			else if (data.Index != Index)
			{
				// If it's not empty move current item to the dragged item's slot.
				Transform item = transform.GetChild(0);
				item.GetComponent<ItemData>().Index = data.Index;
				item.transform.SetParent(inventory.Slots[data.Index].transform);
				item.transform.position = inventory.Slots[data.Index].transform.position;
			}

			// Assign dragged item to this slot.
			inventory.Items[data.Index] = inventory.Items[Index];
			inventory.Items[Index] = data.Item;
			data.Index = Index;
		}

		private void Start()
		{
			inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
		}
	}
}