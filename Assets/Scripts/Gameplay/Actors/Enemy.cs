/*
 *	Created on 8/8/2017 2:17:46 AM
 *	Project Rogue by Irakli Chkuaseli
 */

using UnityEngine;
using UnityEngine.Assertions.Comparers;

namespace Gameplay.Actors
{
	public class Enemy : Entity
	{
		[SerializeField] 
		private GameObject bullet;

		[SerializeField] 
		private float fireRate = 3f;
		
		private float nextFire;
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.transform.position.x < transform.position.x)
				transform.localScale = new Vector3(-1, 1, 1);
			else
				transform.localEulerAngles = Vector3.one;
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.transform.position.x < transform.position.x)
				transform.localScale = new Vector3(-1, 1, 1);
			else
				transform.localEulerAngles = Vector3.one;
		}

		private void OnTriggerStay2D(Collider2D other)
		{
			if (Time.time > nextFire)
			{
				nextFire = Time.time + fireRate;
				var bullet = Instantiate(this.bullet, transform.position, Quaternion.identity);
				bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(5000 * transform.localScale.x, 0));
			}
		
		}
	}
}