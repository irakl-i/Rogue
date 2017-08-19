/*
 *	Created on 8/19/2017 4:25:02 AM
 *	Project Rogue by Irakli Chkuaseli
 */

using UnityEngine;

namespace Gameplay.Items
{
	[RequireComponent(typeof(BoxCollider2D))]
	public class Droppable : MonoBehaviour
	{
		[SerializeField]
		private Item item;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			PickUp(collision.tag);
		}

		private void PickUp(string player)
		{
			Debug.Log(player + " took " + item.Name);
		}
	}
}