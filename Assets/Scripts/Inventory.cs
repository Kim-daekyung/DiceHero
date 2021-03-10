using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public Transform slot;
    public List<Slot> slotScripts = new List<Slot>();
    public Transform draggingItem;
    public Slot enteredSlot;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Transform newSlot = Instantiate(slot);
                newSlot.name = "Slot" + (i + 1) + "." + (j + 1);
                //newSlot.parent = this.transform;
                newSlot.SetParent(this.transform);
                RectTransform slotRect = newSlot.GetComponent<RectTransform>();
                slotRect.anchorMin = new Vector2(0.2f * j + 0.05f, 1 - (0.2f * (i + 1) - 0.05f));
                slotRect.anchorMax = new Vector2(0.2f * (j + 1) - 0.05f, 1 - (0.2f * i + 0.05f));
                slotRect.offsetMin = Vector2.zero;
                slotRect.offsetMax = Vector2.zero;

                slotScripts.Add(newSlot.GetComponent<Slot>());
                newSlot.GetComponent<Slot>().number = i * 4 + j;

                ItemImageChange(slotScripts[i * 4 + j]); // 모든 슬롯의 이미지 오브젝트 비활성화 (아이템이 슬롯에 있다면 반복문 아래의 AddItem을 통해 활성화)
            }
        }

        for (int i = 0; i < ItemDatabase.instance.items.Count; i++)
        {
            AddItem(i);
        }
    }

    void AddItem(int number)
    {
        if (slotScripts[number].item.value == 0)
        {
            slotScripts[number].item = ItemDatabase.instance.items[number];
            ItemImageChange(slotScripts[number]);
        }
    }
    public void ItemImageChange(Slot _slot)
    {
        if (_slot.item.value == 0) // 아이템이 없을 시 아이템 이미지 오브젝트 비활성화
        {
            _slot.transform.GetChild(0).gameObject.SetActive(false);
        }
        else // 이미지 오브젝트 활성화 및 이미지를 슬롯에 해당하는 아이템의 이미지로 변경
        {
            _slot.transform.GetChild(0).gameObject.SetActive(true);
            _slot.transform.GetChild(0).GetComponent<Image>().sprite = _slot.item.image;
        }
    }
    public void AddToDatabase()
    {
        ItemDatabase.instance.Clear();
        for (int i = 0; i < 4; i++)
        {
            if (slotScripts[i].item.name != "")
            {
                Item slotItem;
                slotItem = slotScripts[i].item;
                ItemDatabase.instance.Add(slotItem.name, slotItem.value, slotItem.price, slotItem.description, slotItem.itemType);
            }
        }
    }
}