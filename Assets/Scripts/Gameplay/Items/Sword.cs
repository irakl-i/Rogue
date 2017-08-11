using System;

namespace Gameplay.Items
{
	[Serializable]
	public class Sword : Weapon
	{
		public Sword(int id, int value, string name, string description, int damage, int range)
		{
			ID = id;
			Value = value;
			Name = name;
			Description = description;
			Damage = damage;
			Range = range;
		}

		public Sword()
		{
		}

		public int Range { get; set; }

		public override string ToString()
		{
			return "What up piiiiiiiiiiiiiiiiiiiiimps";
		}
	}
}