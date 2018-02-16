/*
 *	Created on 8/6/2017 7:13:29 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
		protected float jumpForce;

		[SerializeField]
		protected int damage;

		[SerializeField]
		protected Text text;
		
		protected new SpriteRenderer renderer;
		protected Animator animator;
		protected Rigidbody2D body;
		protected bool facing;
		protected Color original;

		public int Health => health;

		public bool Facing => facing;

		private void Awake()
		{
			body = GetComponent<Rigidbody2D>();
			renderer = GetComponent<SpriteRenderer>();
			animator = GetComponent<Animator>();
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

		public void UpdateStats(int health, int speed, float jumpForce, int damage, float duration, string slug)
		{
			this.health += health;
			this.speed += speed;
			this.jumpForce += jumpForce;
			this.damage += damage;

			if (duration > 0) 
				StartCoroutine(Unuse(health, speed, jumpForce, damage, duration));
		}

		private IEnumerator Unuse(int health, int speed, float jumpForce, int damage, float duration)
		{
			var currentTime = duration;
			while (currentTime >= 0)
			{
				text.text = currentTime.ToString();
				yield return new WaitForSeconds(1.0f);
				currentTime--;
			}
			
			this.health -= health;
	        this.speed -= speed;
	        this.jumpForce -= jumpForce;
	        this.damage -= damage;
		}
	}
}