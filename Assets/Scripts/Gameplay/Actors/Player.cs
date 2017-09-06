/*
 *	Created on 8/6/2017 7:31:58 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using UnityEngine;
using Utilities;

namespace Gameplay.Actors
{
	public class Player : Entity
	{
		[SerializeField, Range(0, 1)]
		private float attackTime;

		private bool attacking;
		private float attackTimeCounter;

		private Animator animator;
		private GameObject weapon;

		public void Start()
		{
			weapon = transform.Find("Weapon").gameObject;
			animator = weapon.GetComponent<Animator>();
		}

		private void Update()
		{
			Hit();
		}

		private void FixedUpdate()
		{
			Move();
		}

		private void LateUpdate()
		{
			SnapToGrid();
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			// TODO: Fix the sword touching damage issue, OnTriggerStay2D w/ cooldown.
			if (collision.CompareTag(Constants.Tag.Enemy))
			{
				var enemy = collision.gameObject.GetComponent<Enemy>();
				enemy.TakeDamage(1, facing);
			}
		}

		/// <summary>
		///     Moves player left, right, up and down.
		/// </summary>
		private void Move()
		{
			var horizontal = Input.GetAxisRaw(Constants.Input.Horizontal);
			var vertical = Input.GetAxisRaw(Constants.Input.Vertical);

			var movement = new Vector2(horizontal, vertical);
			facing = movement != Vector2.zero ? movement : facing;

			ChangeDirection();

			body.velocity = movement.normalized * speed * Time.fixedDeltaTime * Constants.TimeMultiplier;
		}

		/// <summary>
		///     Changes player's and weapon's direction.
		/// </summary>
		private void ChangeDirection()
		{
			if (facing == Vector2.left && !attacking)
			{
				renderer.flipX = true;
				weapon.GetComponentInChildren<SpriteRenderer>().flipX = true;
				animator.SetBool(Constants.Animation.Left, true);
			}
			else if (facing == Vector2.right && !attacking)
			{
				renderer.flipX = false;
				weapon.GetComponentInChildren<SpriteRenderer>().flipX = false;
				animator.SetBool(Constants.Animation.Left, false);
			}
		}

		/// <summary>
		///     Snaps the player to the grid for pixel perfect movement.
		/// </summary>
		private void SnapToGrid()
		{
			// if (!colliding)
			if (body.velocity == Vector2.zero)
				transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y),
					transform.position.z);
		}

		/// <summary>
		///     Initiates hit animation.
		/// </summary>
		private void Hit()
		{
			if (Input.GetButtonDown(Constants.Input.Shoot) && !attacking)
			{
				attacking = true;
				attackTimeCounter = attackTime;
				animator.SetBool(Constants.Animation.Attacking, true);
			}

			if (attackTimeCounter > 0)
				attackTimeCounter -= Time.deltaTime;

			if (attackTimeCounter <= 0 && attacking)
			{
				attacking = false;
				animator.SetBool(Constants.Animation.Attacking, false);
			}
		}
	}
}