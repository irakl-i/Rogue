using UnityEngine;

namespace Items
{
	public abstract class Item : MonoBehaviour
	{
		public int ID { get; set; }
		public string Name { get; set; }
	}
}