/*
 *	Created on 8/11/2017 11:27:47 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using System.Collections.Generic;
using System.IO;
using System.Linq;
using LitJson;
using UnityEngine;
using Utilities;

namespace Gameplay.Items.Inventory
{
	public class Database : MonoBehaviour
	{
		private JsonData data;

		public List<Sword> Swords { get; private set; }
		public List<Potion> Potions { get; private set; }

		private void Start()
		{
			var json = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, Constants.Database.Items));
			data = JsonMapper.ToObject(json);

			// Load all the database items.
			LoadItems();
		}

		/// <summary>
		///     Loads items from the database.
		/// </summary>
		private void LoadItems()
		{
			LoadSwords();
			LoadPotions();
			// ...
		}

		/// <summary>
		///     Loads potions from the database.
		/// </summary>
		private void LoadPotions()
		{
			// TODO
		}


		/// <summary>
		///     Loads swords from the database.
		/// </summary>
		private void LoadSwords()
		{
			Swords = new List<Sword>();
			JsonData swords = data[Constants.Database.Weapons][Constants.Database.Swords];

			for (var i = 0; i < swords.Count; i++)
			{
				var id = (int) swords[i][Constants.Database.ID];
				var name = swords[i][Constants.Database.Name].ToString();
				var description = swords[i][Constants.Database.Description].ToString();
				var slug = swords[i][Constants.Database.Slug].ToString();
				var value = (int) swords[i][Constants.Database.Value];
				var damage = (int) swords[i][Constants.Database.Stats][Constants.Database.Damage];
				var range = (int) swords[i][Constants.Database.Stats][Constants.Database.Range];

				Swords.Add(new Sword(id, name, description, slug, value, damage, range));
			}
		}

		/// <summary>
		///     Gets the item with given ID from the database.
		/// </summary>
		/// <param name="id">Item ID</param>
		/// <returns>Item</returns>
		public Item GetItem(int id)
		{
			return Swords.FirstOrDefault(sword => sword.ID == id);
		}
	}
}