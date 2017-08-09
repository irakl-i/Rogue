/*
 *	Created on 8/6/2017 7:31:58 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using System.Collections.Generic;
using Gameplay.Abilities;
using UnityEngine;
using Utilities;

namespace Gameplay.Actors
{
	public class Player : Entity
	{
		private List<IAbility> abilities;

		public void Start()
		{
			abilities = new List<IAbility>();
		}

		private void Update()
		{
			Move();
			Hit();
			foreach (IAbility ability in abilities)
				ability.Use();
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

			body.velocity = movement * speed * Time.deltaTime * Constants.TimeMultiplier;
		}

		/// <summary>
		///     Warps player in its facing direction.
		/// </summary>
		private void Hit()
		{
			if (Input.GetButtonDown(Constants.Input.Shoot))
			{
				RaycastHit2D hit = Physics2D.Raycast(body.position, facing, reach);

				Debug.DrawRay(body.position, facing, Color.red);

				if (hit.collider != null && hit.collider.CompareTag(Constants.Tag.Enemy))
					hit.transform.gameObject.GetComponent<Enemy>().TakeDamage(damage);
			}
		}

		public void AddAbility(IAbility ability)
		{
			abilities.Add(ability);
		}

		public void RemoveAbility(IAbility ability)
		{
			abilities.Remove(ability);
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			TakeDamage(10);
		}
	}
}