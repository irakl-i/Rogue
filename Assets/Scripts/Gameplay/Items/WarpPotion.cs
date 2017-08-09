using Gamelogic.Extensions;
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

		private Warp warp;

		public override int ID => 0;
		public override string Name => "Warp";

		/// <summary>
		///     Use warp potion on colliding player, disable the effect after given time.
		/// </summary>
		/// <param name="collider">Colliding object</param>
		public override void Use(Collider2D collider)
		{
			if (collider.CompareTag(Constants.Tag.Player))
			{
				if (player == null) player = collider.gameObject.GetComponent<Player>();
				warp = collider.gameObject.AddComponent<Warp>();

				warp.Init(player, distance, delay, particle);
				player.AddAbility(warp);

				// Invoke delete action after length (s) amount of time.
				this.Invoke(Delete, length);
			}
		}

		/// <summary>
		///     Disable and delete potion effect.
		/// </summary>
		public override void Delete()
		{
			Debug.LogFormat("Deleting {0}", Name);
			Destroy(gameObject.GetComponent<Warp>());
			player.RemoveAbility(warp);

			Destroy(gameObject);
		}
	}
}