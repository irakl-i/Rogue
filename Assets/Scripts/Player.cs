/*
 *	Created on 8/6/2017 7:31:58 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using System.Collections.Generic;
using System.Linq;
using Abilities;
using UnityEngine;
using Utilities;

public class Player : Entity
{
	private List<IAbility> abilities;

	public void Start()
	{
		var warp = gameObject.AddComponent<Warp>();
		warp.Init(this, 10, 3);

		abilities = new List<IAbility> {warp};
	}

	private void Update()
	{
		Move();
		Hit();
		abilities.First().Use();
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

	private void OnCollisionEnter2D(Collision2D collision)
	{
		TakeDamage(10);
	}
}