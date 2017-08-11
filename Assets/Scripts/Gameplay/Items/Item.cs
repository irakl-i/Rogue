using System;

namespace Gameplay.Items
{
	[Serializable]
	public abstract class Item
	{
		protected int ID { get; set; }
		protected string Name { get; set; }
		protected string Description { get; set; }
		protected string Slug { get; set; }
		protected int Value { get; set; }

		/// <inheritdoc />
		public override string ToString()
		{
			return $"{nameof(ID)}: {ID}, {nameof(Name)}: {Name}, {nameof(Description)}: {Description}, {nameof(Slug)}: {Slug}, {nameof(Value)}: {Value}";
		}
	}
}