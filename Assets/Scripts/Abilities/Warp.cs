using System;
using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

namespace Abilities
{
	[Serializable]
	public class Warp : MonoBehaviour, IAbility
	{
		[SerializeField]
		[Range(0, 10)]
		private int distance;

		[SerializeField]
		private ParticleSystem particle;

		[SerializeField]
		private float delay;

		private float lastWarp;
		private Entity entity;
		private Rigidbody2D body;

		public int ID => 0;

		public string Name => "Warp";


		public void Use()
		{
			var canWarp = lastWarp == 0 || Time.time - lastWarp >= delay;

			if (Input.GetButtonDown(Constants.Input.Jump) && canWarp)
			{
				RaycastHit2D hit = Physics2D.Raycast(body.position, entity.Facing, distance);
				if (hit.collider == null)
				{
					// Warp forward.
					body.position += entity.Facing * distance;

					// Play particle effect.
					particle.transform.up = -entity.Facing.normalized;
					particle.Play();

					lastWarp = Time.time;
				}
			}
		}

		private void Start()
		{
			body = entity.GetComponent<Rigidbody2D>();
			particle = entity.transform.Find(Name).GetComponent<ParticleSystem>();

			Assert.IsNotNull(particle);
		}

		public void Init(Entity entity, int distance, float delay)
		{
			this.entity = entity;
			this.distance = distance;
			this.delay = delay;
		}
	}
}