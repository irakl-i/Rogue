/*
 *	Created on 8/14/2017 4:47:21 AM
 *	Project Rogue by Irakli Chkuaseli
 */

using Gameplay.Items;
using Gameplay.Items.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
	public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler,
		IPointerExitHandler
	{
		private Vector2 offset;
		private Inventory inventory;
		private Tooltip tooltip;


		public int Amount { get; set; }
		public Item Item { get; set; }
		public int Index { get; set; }

		public void OnBeginDrag(PointerEventData eventData)
		{
			if (Item != null)
			{
				offset = eventData.position - (Vector2) transform.position;

				transform.SetParent(transform.parent.parent);
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
			transform.SetParent(inventory.Slots[Index].transform);
			transform.position = inventory.Slots[Index].transform.position;

			GetComponent<CanvasGroup>().blocksRaycasts = true;
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
			inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
			tooltip = inventory.GetComponent<Tooltip>();
		}
	}
}