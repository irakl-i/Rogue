using UnityEngine;

namespace Gameplay.Items
{
	public abstract class Potion : Item
	{
		[SerializeField]
		protected float length;

		private void OnTriggerEnter2D(Collider2D collider)
		{
			Use(collider);
			Destroy(gameObject);
		}

		public abstract void Use(Collider2D collider);
	}
}