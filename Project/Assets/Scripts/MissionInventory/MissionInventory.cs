using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission Inventory", menuName = "Inventory System/MissionInventory ")]
public class MissionInventory : ScriptableObject
{

    public List<BagSlot> Container = new List<BagSlot>();



    public void AddItem(Item_SO _item, int _amount)
    {
        bool hasItem = false;
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            Container.Add(new BagSlot(_item, _amount));
        }
    }


    public void ResetBag()
    {
        Container.Clear();
    }

}

[System.Serializable]
public class BagSlot
{
    public Item_SO item;
    public int amount;
    public BagSlot(Item_SO _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}
