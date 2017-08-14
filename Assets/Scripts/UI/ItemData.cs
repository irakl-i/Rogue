/*
 *	Created on 8/14/2017 4:47:21 AM
 *	Project Rogue by Irakli Chkuaseli
 */

using Gameplay.Items;
using UnityEngine;

namespace UI
{
	public class ItemData : MonoBehaviour
	{
		[SerializeField]
		private Item item;

		public int Amount;
	}
}