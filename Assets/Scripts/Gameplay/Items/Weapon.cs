using UnityEngine;

namespace Gameplay.Items
{
	public abstract class Weapon : Item
	{
		[SerializeField]
		protected int damage;

		public int Damage => damage;
	}
}