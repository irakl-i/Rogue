using UnityEngine;

namespace Gameplay.Items
{
	public abstract class Item : MonoBehaviour
	{
		public abstract int ID { get; }
		public abstract string Name { get; }
	}
}