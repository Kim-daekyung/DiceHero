using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Mercenary,      //용병
    Equipment,      //장비
    Consumption,    //소모품
    Misc            //기타
}

[System.Serializable]
public class Item
{
    public string name;
    public int value;
    public int price;
    public string description;
    public ItemType itemType;
    public Sprite image;

    public Item(string _name, int _value, int _price, string _description, ItemType _itemType, Sprite _image)
    {
        name = _name;
        value = _value;
        price = _price;
        description = _description;
        itemType = _itemType;
        image = _image;
    }
}