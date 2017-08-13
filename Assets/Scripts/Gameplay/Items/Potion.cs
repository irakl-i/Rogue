/*
 *	Created on 8/9/2017 3:21:54 AM
 *	Project Rogue by Irakli Chkuaseli
 */

using System;
using UnityEngine;

namespace Gameplay.Items
{
	[Serializable]
	public abstract class Potion : Item
	{
		private float duration;

		public float Duration
		{
			get { return duration; }
			set { duration = value; }
		}

		public abstract void Use(Collider2D collision);
		public abstract void Delete();
	}
}