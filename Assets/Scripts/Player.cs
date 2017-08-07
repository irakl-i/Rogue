/*
 *	Created on 8/6/2017 7:31:58 PM
 *	Project Rogue by Irakli Chkuaseli
 */

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

	private Color original;
	private float lastWarp;

	private void Update()
	{
		Move();
		Warp();
		Hit();
	}

	private void Start()
	{
		original = renderer.color;
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
	///     Warps player in its facing direction.
	/// </summary>
	private void Warp()
	{
		var canWarp = lastWarp == 0 || Time.time - lastWarp >= delay;

		if (canWarp && renderer.color == original)
			renderer.color = warp;


		if (Input.GetButtonDown(Constants.Input.Jump) && canWarp)
		{
			RaycastHit2D hit = Physics2D.Raycast(body.position, direction, distance);
			if (hit.collider == null)
			{
				// Warp forward.
				body.position += direction * distance;

				// Play particle effect.
				particle.transform.up = -direction.normalized;
				particle.Play();

				lastWarp = Time.time;
				GetComponent<SpriteRenderer>().color = original;
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
				hit.transform.gameObject.GetComponent<Enemy>().TakeDamage(damage);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		TakeDamage(collisionDamage);
	}
}