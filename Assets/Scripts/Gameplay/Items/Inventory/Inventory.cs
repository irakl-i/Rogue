/*
 *	Created on 8/13/2017 5:28:45 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using System.Collections.Generic;
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
			}

			AddItem(0);
			AddItem(2);
			AddItem(1);
		}

		/// <summary>
		/// Add the item to the player inventory.
		/// </summary>
		/// <param name="id">ID of the item in the database</param>
		public void AddItem(int id)
		{
			for (var i = 0; i < items.Count; i++)
			{
				if (items[i] == null)
				{
					// Get the item from the database and save it in the inventory.
					Item toAdd = database.GetItem(id);
					items[i] = toAdd;

					// Instantiate the item in inventory panel.
					GameObject item = Instantiate(this.item);

					// Set correct values.
					item.transform.SetParent(slots[i].transform);
					item.GetComponent<Image>().sprite = toAdd.Sprite;

					// Set item and slot names.
					slots[i].name = "Slot: " + toAdd.Name;
					item.name = toAdd.Name;

					break;
				}
			}
		}
	}
}