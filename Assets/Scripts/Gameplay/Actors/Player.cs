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
		private Animator animator;

		private bool attacking;

		[SerializeField]
		private float attackTime;

		private float attackTimeCounter;
		private GameObject weapon;

		public void Start()
		{
			weapon = GameObject.Find("Weapon");
			animator = weapon.GetComponent<Animator>();
			Debug.Log(weapon.transform.localPosition);
		}

		private void Update()
		{
			Move();
			Hit();
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

			body.velocity = movement.normalized * speed * Time.deltaTime * Constants.TimeMultiplier;
		}

		/// <summary>
		///     Initiates hit animation.
		/// </summary>
		private void Hit()
		{
			if (Input.GetButtonDown(Constants.Input.Shoot) && !attacking)
			{
				attackTimeCounter = attackTime;
				attacking = true;
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