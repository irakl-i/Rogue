using Gameplay.Abilities;
using Gameplay.Actors;
using UnityEngine;
using Utilities;

namespace Gameplay.Items
{
	public class WarpPotion : Potion
	{
		[SerializeField]
		[Range(0, 100)]
		private int distance;
		
		[SerializeField]
		[Range(0, 10)]
		private float delay;

		[SerializeField]
		private ParticleSystem particle;

		public override void Use(Collider2D collider)
		{
			Debug.Log("Supper");
			if (collider.CompareTag(Constants.Tag.Player))
			{
				Debug.Log("Supper!!!");
				var player = collider.gameObject.GetComponent<Player>();
				var warp = collider.gameObject.AddComponent<Warp>();

				warp.Init(player, distance, delay, particle);
				player.AddAbility(warp);
			}
		}
	}
}