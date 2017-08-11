using System;

namespace Gameplay.Items
{
	[Serializable]
	public class Sword : Weapon
	{
		public Sword(int id, string name, string description, string slug, int value, int damage, int range)
		{
			ID = id;
			Value = value;
			Name = name;
			Description = description;
			Slug = slug;
			Damage = damage;
			Range = range;
		}

		public Sword()
		{
		}

		public int Range { get; set; }

		/// <inheritdoc />
		public override string ToString()
		{
			return base.ToString() + $", {nameof(Range)}: {Range}";
		}
	}
}