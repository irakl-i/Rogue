/*
 *	Created on 8/14/2017 4:47:21 AM
 *	Project Rogue by Irakli Chkuaseli
 */

using Gamelogic.Extensions;
using Gameplay.Items;
using Gameplay.Items.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
	public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler,
		IPointerExitHandler, IPointerClickHandler
	{
		private Vector2 offset;
		private Inventory inventory;
		private Tooltip tooltip;

		public int Amount { get; set; }
		public int Index { get; set; }
		public bool IsEquipped { get; set; }
		public Item Item { get; set; }

		public void OnBeginDrag(PointerEventData eventData)
		{
			if (Item != null)
			{
				offset = eventData.position - (Vector2) transform.position;

				transform.hasChanged = true;
				// (Root = UI) -> Canvas -> Dragged Items
				transform.SetParent(transform.root.GetChild(0).Find("Dragged Items").transform);
				transform.position = eventData.position;

				GetComponent<CanvasGroup>().blocksRaycasts = false;
			}
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (Item != null)
				transform.position = eventData.position - offset;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			transform.SetParent(inventory.InventorySlots[Index].transform);
			transform.position = inventory.InventorySlots[Index].transform.position;

			GetComponent<CanvasGroup>().blocksRaycasts = true;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Right)
				if (!IsEquipped && inventory.EquipmentSlots[(int) Inventory.Equipments.Weapon] == null)
				{
					IsEquipped = true;
					inventory.EquipmentSlots[(int) Inventory.Equipments.Weapon] = gameObject;

					transform.SetParent(transform.root.GetChild(0).Find("Equipment Panel").GetChild(0).GetChild(0).transform);
					transform.ResetLocal();
					GetComponent<CanvasGroup>().blocksRaycasts = true;
				}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			tooltip.Activate(Item);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			tooltip.Deactivate();
		}

		public void Start()
		{
			inventory = Inventory.Instance;
			tooltip = inventory.GetComponent<Tooltip>();
			IsEquipped = false;
		}
	}
}