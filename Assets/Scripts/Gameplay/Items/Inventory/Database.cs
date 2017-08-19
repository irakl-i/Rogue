/*
 *	Created on 8/11/2017 11:27:47 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using System.IO;
using System.Linq;
using Gamelogic.Extensions;
using LitJson;
using UnityEngine;
using Utilities;

namespace Gameplay.Items.Inventory
{
	public class Database : MonoBehaviour
	{
		private JsonData data;

		[SerializeField]
		private GameObject sword;

		[SerializeField]
		private ItemList items;

		public ItemList Items => items;

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
			JsonData swords = data[Constants.Database.Weapons][Constants.Database.Swords];

			for (var i = 0; i < swords.Count; i++)
			{
				var id = (int) swords[i][Constants.Database.ID];
				var name = swords[i][Constants.Database.Name].ToString();
				var description = swords[i][Constants.Database.Description].ToString();
				var slug = swords[i][Constants.Database.Slug].ToString();
				var value = (int) swords[i][Constants.Database.Value];
				var stackable = (bool) swords[i][Constants.Database.Stackable];
				var damage = (int) swords[i][Constants.Database.Stats][Constants.Database.Damage];
				var range = (int) swords[i][Constants.Database.Stats][Constants.Database.Range];

				GameObject swordObject = Instantiate(sword, Vector3.zero, Quaternion.identity);
				var swordScript = swordObject.GetComponent<Sword>();
				swordScript.Initialize(id, name, description, slug, value, stackable, damage, range);

				swordObject.name = swordScript.Name;
				swordObject.transform.SetParent(transform);

				items.Add(swordScript);
			}
		}

		/// <summary>
		///     Gets the item with given ID from the database.
		/// </summary>
		/// <param name="id">Item ID</param>
		/// <returns>Item</returns>
		public Item GetItem(int id)
		{
			return Items.FirstOrDefault(sword => sword.ID == id);
		}
	}
}