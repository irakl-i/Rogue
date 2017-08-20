using System.Linq;
using Gameplay.Items;
using UnityEditor;
using UnityEngine;

public class InventoryFactory
{
	[MenuItem("GameObject/Inventory/Item/Swords", false, 0)]
	private static void InitializeSwords()
	{
		var items = Resources.LoadAll<Item>("Items/Swords");

		var window = EditorWindow.GetWindow<InventoryWindow>(true, "Create a new sword", true);
		window.ShowPopup();

		window.Items = items;
	}

	[MenuItem("GameObject/Inventory/Item/Potions", false, 1)]
	private static void InitializePotions()
	{
		var items = Resources.LoadAll<Item>("Items/Potions");

		var window = EditorWindow.GetWindow<InventoryWindow>(true, "Create a new potion", true);
		window.ShowPopup();

		window.Items = items;
	}
}

public class InventoryWindow : EditorWindow
{
	private int index;
	private string[] names;

	private Item[] items;

	public Item[] Items
	{
		get { return items; }
		set
		{
			items = value;
			names = items.Select(t => t.Name).ToArray();
		}
	}

	public void OnGUI()
	{
		index = EditorGUILayout.Popup(index, names);

		if (GUILayout.Button("Create"))
		{
			var item = new GameObject(items[index].Name);
			item.transform.position = new Vector3(0, 0, 0);

			item.AddComponent<SpriteRenderer>().sprite = items[index].Sprite;
			item.AddComponent<BoxCollider2D>().isTrigger = true;
			item.AddComponent<Droppable>().Data = items[index];

			Close();
		}
	}
}