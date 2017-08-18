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
	public abstract class Item : IHTMLSerializable, IDroppable
	{
		public int ID { get; set; }
		public int Value { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Slug { get; set; }
		public bool Stackable { get; set; }
		public Sprite Sprite { get; set; }

		/// <inheritdoc />
		public abstract void Drop();

		/// <inheritdoc />
		public abstract void PickUp();

		/// <inheritdoc />
		public abstract string ToHTML(Color nameColor, Color descriptionColor, Color valueColor);


		/// <inheritdoc />
		public override string ToString()
		{
			return
				$"{nameof(ID)}: {ID}, {nameof(Name)}: {Name}, {nameof(Description)}: {Description}, {nameof(Slug)}: {Slug}, {nameof(Value)}: {Value}";
		}
	}
}