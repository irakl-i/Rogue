using System;

namespace Gameplay.Items
{
	[Serializable]
	public abstract class Item
	{
		protected int ID { get; set; }

		protected string Name { get; set; }

		protected string Description { get; set; }

		protected int Value { get; set; }
	}
}