using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    public List<Item> items = new List<Item>();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }
    public void Add(string name, int value, int price, string description, ItemType itemType)
    {
        if (SceneManager.GetActiveScene().name == "MercenaryGuild")
            items.Add(new Item(name, value, price, description, itemType, Resources.Load<Sprite>("MercenaryImages/" + name)));
        else if (SceneManager.GetActiveScene().name == "Shop")
            items.Add(new Item(name, value, price, description, itemType, Resources.Load<Sprite>("ConsumptionItemImages/" + name)));
    }
    public void Remove(string name, int value, int price, string description, ItemType itemType)
    {
        if (SceneManager.GetActiveScene().name == "MercenaryGuild")
            items.Remove(new Item(name, value, price, description, itemType, Resources.Load<Sprite>("MercenaryImages/" + name)));
        else if (SceneManager.GetActiveScene().name == "Shop")
            items.Remove(new Item(name, value, price, description, itemType, Resources.Load<Sprite>("ConsumptionItemImages/" + name)));
    }
    public void Clear()
    {
        items.Clear();
    }
}