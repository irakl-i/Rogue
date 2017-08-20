/*
 *	Created on 8/21/2017 12:47:54 AM
 *	Project Rogue by Irakli Chkuaseli
 */

using UnityEngine;

namespace Gameplay.Items
{
	public class Droppable : MonoBehaviour
	{
		[SerializeField]
		private Item data;

		public Item Data
		{
			get { return data; }
			set { data = value; }
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			PickUp();
			Destroy(gameObject);
		}

		private void PickUp()
		{
			Inventory.Inventory.Instance.AddItem(data);
		}
	}
}