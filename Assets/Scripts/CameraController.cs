/*
 *	Created on 8/6/2017 7:57:38 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField]
	private Transform target;

	[SerializeField]
	[Range(0, 3)]
	private float time;

	private Vector3 offset;
	private Vector3 velocity;

	private void Start()
	{
		offset = transform.position;
	}

	private void FixedUpdate()
	{
		Vector3 position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity,
			time * Time.smoothDeltaTime);
		transform.position = position;
	}
}