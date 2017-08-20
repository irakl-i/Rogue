/*
 *	Created on 8/13/2017 5:28:45 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using System.Collections.Generic;
using Gamelogic.Extensions;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Items.Inventory
{
	public class Inventory : MonoBehaviour
	{
		public Inventory()
		{
			Slots = new List<GameObject>();
		}

		private void Start()
		{
			// Find the inventory panel.
			inventoryPanel = GameObject.Find("Inventory Panel");

			// Find the slot panel.
			slotPanel = inventoryPanel.transform.Find("Slot Panel").gameObject;

			// Find the sibling database script.
			database = Database.Instance;

			// Instantiate the empty slots.
			for (var i = 0; i < size; i++)
			{
				// Add empty items to the list.
				Items.Add(null);

				// Instantiate Slots.
				Slots.Add(Instantiate(slot));
				Slots[i].transform.SetParent(slotPanel.transform, false);
				Slots[i].name = "Slot " + i;
				Slots[i].GetComponent<Slot>().Index = i;
			}

			for (var i = 0; i < 3; i++)
			{
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
			if (toAdd.Stackable && Items.Contains(toAdd))
			{
				// Get its associated data.
				var data = Slots[Items.IndexOf(toAdd)].transform.GetComponentInChildren<ItemData>();

				// Increase the amount.
				data.Amount++;
				Items[Items.IndexOf(toAdd)].name = toAdd.Name + $" ({data.Amount})";

				// Update the amount text.
				data.transform.GetComponentInChildren<Text>().text = data.Amount.ToString();
				return;
			}

			for (var i = 0; i < Items.Count; i++)
				if (Items[i] == null)
				{
					// Get the item from the database and save it in the inventory.
					Items[i] = toAdd;

					// Instantiate the item in inventory panel.
					GameObject item = Instantiate(this.item);
					var data = item.GetComponent<ItemData>();

					// Set correct values.
					item.transform.SetParent(Slots[i].transform, false);
					item.GetComponent<Image>().sprite = toAdd.Sprite;

					// Set item name.
					item.name = toAdd.Name;

					// Update the data values.
					data.Amount = 1;
					data.Item = toAdd;
					data.Index = i;

					break;
				}
		}

		// Collections

		#region Public properties

		public ItemList Items => items;

		public List<GameObject> Slots { get; set; }

		#endregion

		#region Instance variables

		private GameObject inventoryPanel;
		private GameObject slotPanel;
		private Database database;

		#endregion

		#region Inspector variables

		[SerializeField]
		private int size;

		[SerializeField]
		private ItemList items;

		[SerializeField]
		private GameObject slot;

		[SerializeField]
		private GameObject item;

		#endregion
	}
}