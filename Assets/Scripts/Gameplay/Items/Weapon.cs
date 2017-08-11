using System;

namespace Gameplay.Items
{
	[Serializable]
	public abstract class Weapon : Item
	{
		protected int Damage { get; set; }

		/// <inheritdoc />
		public override string ToString()
		{
			return base.ToString() + $", {nameof(Damage)}: {Damage}";
		}
	}
}