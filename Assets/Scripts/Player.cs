/*
 *	Created on 8/6/2017 7:31:58 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using UnityEngine;
using Utilities;

public class Player : Entity
{
	[Header("Warping")]
	[SerializeField]
	[Range(0, 10)]
	private int distance;

	[SerializeField]
	private ParticleSystem particle;

	[SerializeField]
	private float delay;

	private float lastWarp;

	private void Update()
	{
		Move();
		Warp();
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
		direction = movement != Vector2.zero ? movement : direction;

		body.velocity = movement * speed * Time.deltaTime * Constants.TimeMultiplier;
	}

	/// <summary>
	///     Warps player in his facing direction.
	/// </summary>
	private void Warp()
	{
		if (Time.time - lastWarp >= delay)
			GetComponent<SpriteRenderer>().color = Color.cyan;

		if (Input.GetButtonDown(Constants.Input.Jump) && Time.time - lastWarp >= delay)
		{
			// TODO: Cast rays from two or three points.
			RaycastHit2D hit = Physics2D.Raycast(body.position, direction, distance);
			if (hit.collider == null)
			{
				// Warp forward.
				body.position += direction * distance;

				// Play particle effect.
				particle.transform.up = -direction.normalized;
				particle.Play();

				lastWarp = Time.time;
				GetComponent<SpriteRenderer>().color = Color.black;
			}
		}
	}

	private void Hit()
	{
		if (Input.GetButtonDown(Constants.Input.Shoot))
		{
			RaycastHit2D hit = Physics2D.Raycast(body.position, direction, reach);
			Debug.DrawRay(body.position, direction, Color.red);
			if (hit.collider != null && hit.collider.CompareTag(Constants.Tag.Enemy))
			{
				hit.transform.gameObject.GetComponent<Enemy>().TakeDamage(10);
			}
		}

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		health -= 10;
		if (health <= 0)
			Destroy(gameObject);
	}
}