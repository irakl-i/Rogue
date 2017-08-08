using UnityEngine;

namespace Items
{
	public abstract class Item : MonoBehaviour
	{
		protected int Id { get; set; }
		protected string Name { get; set; }
	}
}