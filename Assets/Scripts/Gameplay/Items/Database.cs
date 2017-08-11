using System.IO;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Gameplay.Items
{
	public class Database
	{
		private void Load()
		{
			var items = Resources.Load<TextAsset>("Items");
			var input = new StringReader(items.text);
			Debug.Log(input);

			var builder = new DeserializerBuilder();
			builder.WithNamingConvention(new CamelCaseNamingConvention());
			Deserializer deserializer = builder.Build();


		}
	}
}