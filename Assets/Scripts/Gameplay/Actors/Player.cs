/*
 *	Created on 8/6/2017 7:31:58 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Utilities;

namespace Gameplay.Actors
{
	public class Player : Entity
	{
		// Attack variables
		[SerializeField, Range(0, 1)]
		private float attackTime;
		private bool attacking;
		private float attackTimeCounter;
		
		// Movement variables
		private float horizontal;
		private bool jump;

		[SerializeField]
		private LayerMask groundLayer;
		
		public void Start()
		{
			body = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			Hit();
			GetInput();
			SetState();
		}

		private void FixedUpdate()
		{
			Move();
		}

		private void LateUpdate()
		{
			SnapToGrid();
		}
		
		/// <summary>
		/// 	Updates player state.
		/// </summary>
		private void SetState()
		{
//			grounded = IsGrounded();
		}
		
		/// <summary>
		/// 	Reads and stores player input.
		/// </summary>
		private void GetInput()
		{
			horizontal = Input.GetAxisRaw(Constants.Input.Horizontal);
			jump = Input.GetButtonDown(Constants.Input.Jump);
		}
		
		/// <summary>
		///     Moves player left, right and up.
		/// </summary>
		private void Move()
		{
			if (jump && IsGrounded())
				body.AddForce(new Vector2(0, jumpForce));

			var movement = new Vector2(horizontal * speed * Time.fixedDeltaTime * Constants.TimeMultiplier, body.velocity.y);

			if (horizontal < 0f && !facing)
				ChangeDirection();
			else if (horizontal > 0f && facing)
				ChangeDirection();
			
			body.velocity = movement;
			animator.SetBool(Constants.Animation.Running, Math.Abs(horizontal) > 0);
		}

		private bool IsGrounded()
		{
			var position = transform.position;
			var direction = Vector2.down;
			const float distance = 10.0f;
    
			var hit = Physics2D.Raycast(position, direction, distance, groundLayer);
			Debug.Log(hit.transform.name);
			Debug.DrawLine(position, position + new Vector3(0, -distance), Color.red); 
			
			return hit.collider != null;
		}

		/// <summary>
		///     Changes player's and weapon's direction.
		/// </summary>
		private void ChangeDirection()
		{
			facing = !facing;
			transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}

		/// <summary>
		///     Snaps the player to the grid for pixel perfect movement.
		/// </summary>
		private void SnapToGrid()
		{
			if (body.velocity == Vector2.zero && IsGrounded())
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
			}

			if (attackTimeCounter > 0)
				attackTimeCounter -= Time.deltaTime;

			if (attackTimeCounter <= 0 && attacking)
			{
				attacking = false;
			}
		}
	}
}