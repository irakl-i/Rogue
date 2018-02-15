/*
 *	Created on 8/11/2017 11:27:47 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using UnityEngine;

namespace Gameplay.Items.Inventory
{
	public class Database : Singleton<Database>
	{
		public List<Item> Items { get; set; }

		private void Start()
		{
			Items = new List<Item>();
			var items = Resources.LoadAll<Item>("Items");

			foreach (var item in items)
				Items.Add(item);
		}

		/// <summary>
		///     Gets the item with given ID from the database.
		/// </summary>
		/// <param name="slug">Item Slug</param>
		/// <returns>Item</returns>
		public Item GetItem(string slug)
		{
			return Items.FirstOrDefault(item => item.Slug == slug);
		}
	}
}