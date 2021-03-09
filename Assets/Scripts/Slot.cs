using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Slot : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IEndDragHandler
{
    public int number;
    public Item item;

    public void OnDrag(PointerEventData data)
    {
        if (this.transform.childCount > 0)
            this.transform.GetChild(0).SetParent(Inventory.instance.draggingItem);
        Inventory.instance.draggingItem.GetChild(0).position = data.position;
    }
    public void OnPointerEnter(PointerEventData data)
    {
        Inventory.instance.enteredSlot = this;
    }

    public void OnPointerExit(PointerEventData data)
    {
        Inventory.instance.enteredSlot = null;
    }

    public void OnEndDrag(PointerEventData data)
    {
        Inventory.instance.draggingItem.GetChild(0).SetParent(this.transform);
        this.transform.GetChild(0).localPosition = Vector3.zero;

        if (Inventory.instance.enteredSlot != null)
        {
            Item tempItem = item;
            item = Inventory.instance.enteredSlot.item;
            Inventory.instance.enteredSlot.item = tempItem;
            Inventory.instance.ItemImageChange(this);
            Inventory.instance.ItemImageChange(Inventory.instance.enteredSlot);
        }
        Inventory.instance.AddToDatabase();
        if (SceneManager.GetActiveScene().name == "MercenaryGuild")
            GuildList.instance.AddToDatabase();
        if (SceneManager.GetActiveScene().name == "Shop")
            ShopList.instance.AddToDatabase();
    }
}