/*
 *	Created on 8/9/2017 3:21:54 AM
 *	Project Rogue by Irakli Chkuaseli
 */

using System;
using Gameplay.Actors;
using UnityEngine;
using Utilities;

namespace Gameplay.Items
{
	[Serializable]
	public class Potion : Item
	{
		[SerializeField]
		private float duration;
		[SerializeField]
		private int health;
		[SerializeField]
		private int speed;
		[SerializeField]
		private float jumpForce;
		[SerializeField]
		private int damage;

		public float Duration
		{
			get { return duration; }
			set { duration = value; }
		}

		public int Health
		{
			get { return health; }
			set { health = value; }
		}

		public int Speed
		{
			get { return speed; }
			set { speed = value; }
		}

		public float JumpForce
		{
			get { return jumpForce; }
			set { jumpForce = value; }
		}

		public int Damage
		{
			get { return damage; }
			set { damage = value; }
		}

		public void Initialize(string name, string description, string slug, int value, bool stackable, float duration,
			int health, int speed, float jump, int damage)
		{
			Value = value;
			Name = name;
			Description = description;
			Slug = slug;
			Stackable = stackable;
			Duration = duration;
			Health = health;
			Speed = speed;
			JumpForce = jump;
			Damage = damage;
			
			Sprite = Resources.Load<Sprite>($"Sprites/Items/{Slug}");
		}

		public void Delete()
		{
		}
		
		public override string ToHTML(Color nameColor, Color descriptionColor, Color valueColor)
		{
			return $"<color=#{nameColor.ToHex()}>{Name}</color>\n\n"
			       + $"<color=#{descriptionColor.ToHex()}>{Description}</color>\n"
			       + $"<color=#{valueColor.ToHex()}>Value: {Value}</color>";
		}

		public override void Drop()
		{
			throw new NotImplementedException();
		}

		public override void PickUp()
		{
			throw new NotImplementedException();
		}

		public override void Use(GameObject target)
		{
			target.GetComponent<Player>().UpdateStats(health, speed, jumpForce, damage);
		}
	}
}