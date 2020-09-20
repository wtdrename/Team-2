using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item_SO item;
    public Item_SO GetItem()
    {
        return item;
    }
    public Image icon;

    public Text itemAmount;

    public bool isEmptySlot;


    public void AddItemToSlot(Item_SO newItem)
    {
        if(item != null && item.isStackable)
        {
            UpdateStackSize();
            return;
        }
        else if(item != null && !item.isStackable)
        {
            return;
        }
        item = newItem;
        icon.sprite = item.itemSprite;
        icon.enabled = true;
        UpdateStackSize();
        isEmptySlot = false;
    }
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        if (itemAmount)
        {
            itemAmount.text = "0";
            itemAmount.enabled = false;
        }
        isEmptySlot = true;
    }
    public void UseItem()
    {
        if(item != null)
        {
            item.UseItem(item);
            UpdateStackSize();
        }
        if(item.stackSize == 0)
        {
            ClearSlot();
        }

    }

    public void UpdateStackSize()
    {
        if (item.isStackable == true && item.stackSize >= 1)
        {
            itemAmount.text = item.stackSize.ToString();
            itemAmount.enabled = true;
        }
    }
}