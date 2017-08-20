/*
 *	Created on 8/10/2017 5:43:45 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using System;
using UnityEngine;

namespace Gameplay.Items
{
	[Serializable]
	public abstract class Weapon : Item
	{
		[Header("Stats")]
		[SerializeField]
		private int damage;

		public int Damage
		{
			get { return damage; }
			set { damage = value; }
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return base.ToString() + $", {nameof(Damage)}: {Damage}";
		}
	}
}