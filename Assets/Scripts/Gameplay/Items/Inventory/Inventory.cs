/*
 *	Created on 8/13/2017 5:28:45 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using System;
using System.Collections.Generic;
using Gamelogic.Extensions;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Gameplay.Items.Inventory
{
	public class Inventory : Singleton<Inventory>
	{
		public enum Equipments
		{
			Weapon,
			Helmet,
			Breastplate,
			Leggings,
			Boots
		}

		// TODO: Fix items getting stuck in-between the slots.

		private void Start()
		{
			InventorySlots = new List<GameObject>();
			EquipmentSlots = new List<GameObject>(Enum.GetValues(typeof(Equipments)).Length);
		}

		private void Update()
		{
			Toggle();
		}

		private void Toggle()
		{
			if (Input.GetButtonDown(Constants.Input.Tab))
				if (inventoryPanel.activeSelf)
				{
					// Unpause the game.
					Extensions.Unpause();

					// Disable inventory and equipment panels.
					inventoryPanel.SetActive(false);
					equipmentPanel.SetActive(false);

					IsActive = false;
				}
				else
				{
					// Pause the game.
					Extensions.Pause();

					// Enable inventory and equipment, disable tooltip.
					inventoryPanel.SetActive(true);
					equipmentPanel.SetActive(true);
					tooltip.SetActive(false);

					IsActive = true;

					// If the inventory has not been initialized before, initialize it.
					if (!isSetup) Initialize();
				}
		}

		private void Initialize()
		{
			// Find the slot panel.
			slotPanel = inventoryPanel.transform.Find("Slot Panel").gameObject;

			// Find the sibling database script.
			database = Database.Instance;

			// Instantiate the empty slots.
			for (var i = 0; i < size; i++)
			{
				// Add empty items to the list.
				Items.Add(null);

				// Instantiate InventorySlots.
				InventorySlots.Add(Instantiate(slot));
				InventorySlots[i].transform.SetParent(slotPanel.transform, false);
				InventorySlots[i].name = "Slot " + i;
				InventorySlots[i].GetComponent<Slot>().Index = i;
			}

			for (var i = 0; i < Enum.GetValues(typeof(Equipments)).Length; i++)
				EquipmentSlots.Add(null);

			for (var i = 0; i < 3; i++)
			{
				AddItem(2);
				AddItem(2);
				AddItem(2);
				AddItem(1);
			}

			isSetup = true;
		}

		/// <summary>
		///     Add the item to the player inventory.
		/// </summary>
		/// <param name="id">ID of the item in the database</param>
		public void AddItem(int id)
		{
			// Get the item from the database.
			AddItem(database.GetItem(id));
		}

		/// <summary>
		///     Add the item to the player inventory.
		/// </summary>
		/// <param name="item">Item to add</param>
		public void AddItem(Item item)
		{
			// Check if the item is stackable and present in the inventory.
			if (item.Stackable && Items.Contains(item))
			{
				// Get its associated data.
				var data = InventorySlots[Items.IndexOf(item)].transform.GetComponentInChildren<ItemData>();

				// Increase the amount.
				data.Amount++;

				// Update the amount text.
				data.transform.GetComponentInChildren<Text>().text = data.Amount.ToString();
				return;
			}

			for (var i = 0; i < Items.Count; i++)
				if (Items[i] == null)
				{
					// Get the item from the database and save it in the inventory.
					Items[i] = item;

					// Instantiate the item in inventory panel.
					GameObject itemObject = Instantiate(this.item);
					var data = itemObject.GetComponent<ItemData>();

					// Set correct values.
					itemObject.transform.SetParent(InventorySlots[i].transform, false);
					itemObject.GetComponent<Image>().sprite = item.Sprite;

					// Set item name.
					itemObject.name = item.Name;

					// Update the data values.
					data.Amount = 1;
					data.Item = item;
					data.Index = i;

					break;
				}
		}

		#region Public properties

		public ItemList Items => items;
		public List<GameObject> InventorySlots { get; set; }
		public List<GameObject> EquipmentSlots { get; set; }
		public bool IsActive { get; set; }

		#endregion

		#region Instance variables

		private GameObject slotPanel;
		private Database database;
		private bool isSetup;

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

		[SerializeField]
		private GameObject inventoryPanel;

		[SerializeField]
		private GameObject equipmentPanel;

		[SerializeField]
		private GameObject tooltip;

		#endregion
	}
}