using Gameplay.Actors;
using UnityEngine;
using Utilities;

namespace Gameplay
{
	public class Bullet : MonoBehaviour
	{
		[SerializeField] 
		private int damage;
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag(Constants.Tag.Player))
			{
				other.GetComponent<Player>().TakeDamage(damage, Vector2.zero);
				Destroy(gameObject);
			}
		}
	}
}
