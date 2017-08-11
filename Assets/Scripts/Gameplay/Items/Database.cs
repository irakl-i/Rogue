using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;
using Utilities;

namespace Gameplay.Items
{
	public class Database : MonoBehaviour
	{
		private JsonData data;

		public List<Sword> Swords { get; private set; }
		public List<Potion> Potions { get; private set; }

		private void Start()
		{
			var json = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "Items.json"));
			data = JsonMapper.ToObject(json);

//			Debug.Log(data[Constants.Database.Weapons][Constants.Database.Swords][2]["id"]);

			AddSwords();

			foreach (Sword sword in Swords)
				Debug.Log(sword.ToString());

			AddPotions();
		}

		private void AddPotions()
		{
			// TODO
		}

		private void AddSwords()
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
	}
}