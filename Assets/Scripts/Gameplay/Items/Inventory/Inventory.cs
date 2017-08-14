/*
 *	Created on 8/13/2017 5:28:45 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Items.Inventory
{
	public class Inventory : MonoBehaviour
	{
		[SerializeField]
		private int size;

		private GameObject inventoryPanel;
		private GameObject slotPanel;
		private Database database;

		[SerializeField]
		private GameObject slot;

		[SerializeField]
		private GameObject item;


		// Collections
		[SerializeField]
		private List<Item> items;

		[SerializeField]
		private List<GameObject> slots;

		public Inventory()
		{
			items = new List<Item>();
			slots = new List<GameObject>();
		}

		private void Start()
		{
			database = GetComponent<Database>();

			inventoryPanel = GameObject.Find("Inventory Panel");
			slotPanel = inventoryPanel.transform.Find("Slot Panel").gameObject;

			for (var i = 0; i < size; i++)
			{
				// Add empty items to the list.
				items.Add(null);

				// Instantiate slots.
				slots.Add(Instantiate(slot));
				slots[i].transform.SetParent(slotPanel.transform);
				slots[i].name = "Empty";
			}

			AddItem(0);
			for (int i = 0; i < 5; i++)
			{
				AddItem(2);
				AddItem(2);
				AddItem(2);
				AddItem(2);
				AddItem(1);
			}

		}

		/// <summary>
		///     Add the item to the player inventory.
		/// </summary>
		/// <param name="id">ID of the item in the database</param>
		public void AddItem(int id)
		{
			// Get the item from the database.
			Item toAdd = database.GetItem(id);

			// Check if the item is stackable and present in the inventory.
			if (toAdd.Stackable && items.Contains(toAdd))
			{
				// Get its associated data.
				var data = slots[items.IndexOf(toAdd)].transform.GetComponentInChildren<ItemData>();

				// Increase the amount.
				data.Amount++;
				
				// Update the amount text.
				data.transform.GetComponentInChildren<Text>().text = data.Amount.ToString();
				return;
			}

			for (var i = 0; i < items.Count; i++)
				if (items[i] == null)
				{
					// Get the item from the database and save it in the inventory.
					items[i] = toAdd;

					// Instantiate the item in inventory panel.
					GameObject item = Instantiate(this.item);

					// Set correct values.
					item.transform.SetParent(slots[i].transform);
					item.GetComponent<Image>().sprite = toAdd.Sprite;

					// Set item and slot names.
					slots[i].name = "Slot " + i;
					item.name = toAdd.Name;

					// Update the amount.
					slots[i].transform.GetComponentInChildren<ItemData>().Amount = 1;

					break;
				}
		}
	}
}