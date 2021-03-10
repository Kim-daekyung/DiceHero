using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildDatabase : MonoBehaviour
{
    public static GuildDatabase instance;
    public List<Item> items = new List<Item>();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        instance = this;
        Add("KnightKim", 1, 0, "그는 용맹한 이름을 가진 용병입니다.", ItemType.Mercenary);
        Add("Teropin", 1, 0, "아이들의 듬직한 친구입니다.", ItemType.Mercenary);
    }
    public void Add(string name, int value, int price, string description, ItemType itemType)
    {
        items.Add(new Item(name, value, price, description, itemType, Resources.Load<Sprite>("MercenaryImages/" + name)));
    }
    public void Remove(string name, int value, int price, string description, ItemType itemType)
    {
        items.Remove(new Item(name, value, price, description, itemType, Resources.Load<Sprite>("MercenaryImages/" + name)));
    }
    public void Clear()
    {
        items.Clear();
    }
}
