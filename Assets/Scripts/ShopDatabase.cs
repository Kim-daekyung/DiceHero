using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDatabase : MonoBehaviour
{
    public static ShopDatabase instance;
    public List<Item> items = new List<Item>();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        instance = this;
        Add("체리맛 회복제", 1, 0, "5차례에 걸쳐 5의 체력을 채웁니다. (맛이 굉장히 없습니다)", ItemType.Consumption);
        Add("속도 증진기", 1, 0, "3차례 동안 선택한 유닛의 속도를 일시적으로 10 추가합니다.", ItemType.Consumption);
        Add("초강력 접착제", 1, 0, "3차례 동안 선택한 유닛의 속도를 일시적으로 10 감소합니다.", ItemType.Consumption);
        Add("해골물", 1, 0, "3차례 동안 선택한 아군이 군중제어기에 면역이 됩니다. 다음 해당 유닛의 차례가 올 때 마신 물의 정체를 알게 되어 2차례 동안 공포상태가 됩니다.", ItemType.Consumption);
        Add("가득찬 페인트통", 1, 0, "선택한 유닛이 은신상태가 불가능합니다.", ItemType.Consumption);
        Add("단짠단짠 사탕", 1, 0, "5차례 동안 선택한 유닛의 공격력 또는 주문력을 무작위로 1 추가합니다", ItemType.Consumption);
        Add("짱짱 쎄지는 쓥퐈 도핑", 1, 0, "이번 차례에 선택한 유닛의 공격력을 2 추가합니다. 그 후 3차례에 걸쳐 공격력이 1씩 감소하고, 4차례에 다시 기존수치가 됩니다.", ItemType.Consumption);
    }
    public void Add(string name, int value, int price, string description, ItemType itemType)
    {
        items.Add(new Item(name, value, price, description, itemType, Resources.Load<Sprite>("ConsumptionItemImages/" + name)));
    }
    public void Remove(string name, int value, int price, string description, ItemType itemType)
    {
        items.Remove(new Item(name, value, price, description, itemType, Resources.Load<Sprite>("ConsumptionItemImages/" + name)));
    }
    public void Clear()
    {
        items.Clear();
    }
}
