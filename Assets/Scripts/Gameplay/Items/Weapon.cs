/*
 *	Created on 8/10/2017 5:43:45 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using System;

namespace Gameplay.Items
{
	[Serializable]
	public abstract class Weapon : Item
	{
		public int Damage { get; set; }

		/// <inheritdoc />
		public override string ToString()
		{
			return base.ToString() + $", {nameof(Damage)}: {Damage}";
		}
	}
}