/*
 *	Created on 8/10/2017 6:12:34 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using System;
using UnityEngine;

namespace Gameplay.Items
{
	[Serializable]
	public class Sword : Weapon
	{
		public Sword(int id, string name, string description, string slug, int value, bool stackable, int damage, int range)
		{
			ID = id;
			Value = value;
			Name = name;
			Description = description;
			Slug = slug;
			Damage = damage;
			Range = range;
			Stackable = stackable;

			Sprite = Resources.Load<Sprite>("Sprites/Items/" + Slug);
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

		/// <inheritdoc />
		public override string ToTooltip()
		{
			throw new NotImplementedException();
		}
	}
}