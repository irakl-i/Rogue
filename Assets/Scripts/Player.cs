/*
 *	Created on 8/6/2017 7:31:58 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using UnityEngine;
using Utilities;

public class Player : MonoBehaviour
{
	[SerializeField]
	[Range(0, 10)]
	private int speed;

	[SerializeField]
	[Range(0, 10)]
	private int warp;

	private Rigidbody2D body;
	private Vector2 direction;

	private void Start()
	{
		body = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		Move();
		Warp();
	}

	private void Move()
	{
		var horizontal = (int) Input.GetAxisRaw(Constants.Input.Horizontal);
		var vertical = (int) Input.GetAxisRaw(Constants.Input.Vertical);

		var movement = new Vector2(horizontal, vertical);
		direction = movement != Vector2.zero ? movement : direction;

		body.velocity = movement * speed * Time.deltaTime * Constants.TimeMultiplier;
	}

	private void Warp()
	{
		if (Input.GetButtonDown(Constants.Input.Jump))
		{
			RaycastHit2D hit = Physics2D.Raycast(body.position, direction, warp);
			if (hit.collider == null)
				body.position += direction * warp;
		}
	}
}