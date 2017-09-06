/*
 *	Created on 8/6/2017 7:13:29 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using System.Collections;
using UnityEngine;

namespace Gameplay.Actors
{
	[RequireComponent(typeof(Rigidbody2D))]
	public abstract class Entity : MonoBehaviour
	{
		private const float FlashDelay = 0.1f;
		private const float KnockbackSpeed = 0.2f;

		[Header("Stats"), SerializeField]
		protected int health;

		[SerializeField, Range(0, 100)]
		protected int speed;

		[SerializeField]
		protected int damage;

		protected new SpriteRenderer renderer;
		protected Rigidbody2D body;
		protected Vector2 facing;
		protected Color original;

		public int Health => health;

		public Vector2 Facing => facing;

		private void Awake()
		{
			body = GetComponent<Rigidbody2D>();
			renderer = GetComponent<SpriteRenderer>();
			original = renderer.color;
		}

		public void TakeDamage(int damage, Vector2 direction)
		{
			health -= damage;
			Debug.LogFormat("Took {0} damage, {1} health remaining", damage, health);

			StartCoroutine(Flash());
			StartCoroutine(
				Knockback(transform, transform.position + Vector3.Scale(new Vector3(4, 4), direction), KnockbackSpeed));

			if (health <= 0)
				Destroy(gameObject);
		}

		private IEnumerator Knockback(Transform transform, Vector3 position, float time)
		{
			var count = 0f;
			Vector3 currentPosition = transform.position;

			while (count < 1)
			{
				count += Time.deltaTime / time;
				transform.position = Vector3.Lerp(currentPosition, position, count);
				yield return null;
			}
		}

		private IEnumerator Flash()
		{
			renderer.color = Color.red;
			yield return new WaitForSeconds(FlashDelay);

			renderer.color = original;
			yield return new WaitForSeconds(FlashDelay);
		}
	}
}