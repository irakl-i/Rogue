/*
 *	Created on 8/10/2017 5:31:46 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using System;
using UI;
using UnityEngine;

namespace Gameplay.Items
{
	[Serializable]
	public abstract class Item : MonoBehaviour, IHTMLSerializable
	{
		[SerializeField]
		[InspectorName("ID")]
		private int id;

		[SerializeField]
		private int value;

		[SerializeField]
		private new string name;

		[SerializeField]
		private string description;

		[SerializeField]
		private string slug;

		[SerializeField]
		private bool stackable;

		[SerializeField]
		private Sprite sprite;

		public int ID
		{
			get { return id; }
			set { id = value; }
		}

		public int Value
		{
			get { return value; }
			set { this.value = value; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		public string Slug
		{
			get { return slug; }
			set { slug = value; }
		}

		public bool Stackable
		{
			get { return stackable; }
			set { stackable = value; }
		}

		public Sprite Sprite
		{
			get { return sprite; }
			set { sprite = value; }
		}

		/// <inheritdoc />
		public abstract string ToHTML(Color nameColor, Color descriptionColor, Color valueColor);

		/// <inheritdoc />
		public abstract void Drop();

		/// <inheritdoc />
		public abstract void PickUp();

		/// <inheritdoc />
		public override string ToString()
		{
			return
				$"{nameof(ID)}: {ID}, {nameof(Name)}: {Name}, {nameof(Description)}: {Description}, {nameof(Slug)}: {Slug}, {nameof(Value)}: {Value}";
		}
	}
}