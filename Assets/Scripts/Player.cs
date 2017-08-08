/*
 *	Created on 8/6/2017 7:31:58 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using System.Collections.Generic;
using Items;
using UnityEngine;
using Utilities;

public class Player : Entity
{
	[SerializeField]
	private int collisionDamage;

	[Header("Warping")]
	[SerializeField]
	[Range(0, 10)]
	private int distance;

	[SerializeField]
	private ParticleSystem particle;

	[SerializeField]
	private float delay;

	[SerializeField]
	private Color warp;

	private float lastWarp;
	private SpriteRenderer hatRenderer;
	private List<Item> items;

	private void Update()
	{
		Move();
		Warp();
		Hit();
	}

	private void Start()
	{
		hatRenderer = transform.Find("Hat").GetComponent<SpriteRenderer>();
		original = hatRenderer.color;
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
	private void Warp()
	{
		var canWarp = lastWarp == 0 || Time.time - lastWarp >= delay;

		if (canWarp && hatRenderer.color == original)
			hatRenderer.color = warp;


		if (Input.GetButtonDown(Constants.Input.Jump) && canWarp)
		{
			RaycastHit2D hit = Physics2D.Raycast(body.position, facing, distance);
			if (hit.collider == null)
			{
				// Warp forward.
				body.position += facing * distance;

				// Play particle effect.
				particle.transform.up = -facing.normalized;
				particle.Play();

				lastWarp = Time.time;
				hatRenderer.color = original;
			}
		}
	}

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
		TakeDamage(collisionDamage);
	}
}