/*
 *	Created on 8/8/2017 2:17:46 AM
 *	Project Rogue by Irakli Chkuaseli
 */

using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
	public void TakeDamage(int damage)
	{
		health -= damage;
		Debug.LogFormat("Took {0} damage, {1} health remaining", damage, health);

		StartCoroutine("Flash");
		if (health <= 0)
			Destroy(gameObject);
	}

	private IEnumerator Flash()
	{
		var renderer = GetComponent<SpriteRenderer>();
		Color color = renderer.color;

		renderer.color = Color.white;
		yield return new WaitForSeconds(0.1f);
		renderer.color = color;
		yield return new WaitForSeconds(0.1f);
	}
}