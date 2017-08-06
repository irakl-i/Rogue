/*
 *	Created on 8/6/2017 7:31:58 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using UnityEngine;
using Utilities;

public class Player : MonoBehaviour
{
	[Header("Stats")]
	[SerializeField]
	private int health;

	[SerializeField]
	[Range(0, 10)]
	private int speed;

	[Header("Warping")]
	[SerializeField]
	[Range(0, 10)]
	private int distance;

	[SerializeField]
	private ParticleSystem particle;

	[SerializeField]
	private float delay;

	private Rigidbody2D body;
	private Vector2 direction;
	private float lastWarp;

	private void Start()
	{
		body = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		Move();
		Warp();
	}

	/// <summary>
	///     Moves player left, right, up and down.
	/// </summary>
	private void Move()
	{
		var horizontal = (int) Input.GetAxisRaw(Constants.Input.Horizontal);
		var vertical = (int) Input.GetAxisRaw(Constants.Input.Vertical);

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
			GetComponent<Renderer>().material.color = Color.cyan;

		if (Input.GetButtonDown(Constants.Input.Jump) && Time.time - lastWarp >= delay)
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
				GetComponent<Renderer>().material.color = Color.black;
			}
		}
	}
}