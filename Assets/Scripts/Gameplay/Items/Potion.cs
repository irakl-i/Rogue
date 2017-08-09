using Gameplay.Actors;
using UnityEngine;

namespace Gameplay.Items
{
	public abstract class Potion : Item
	{
		[SerializeField]
		protected float length;

		protected Player player;

		private void OnTriggerEnter2D(Collider2D collider)
		{
			Use(collider);

			// Disable renderer and collider for this object.
			GetComponent<SpriteRenderer>().enabled = false;
			GetComponent<BoxCollider2D>().enabled = false;
		}

		public abstract void Use(Collider2D collider);
		public abstract void Delete();
	}
}