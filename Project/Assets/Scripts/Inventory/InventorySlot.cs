using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item_SO Item { private set; get; }

    public Image icon;

    public Text itemAmount;

    public bool isEmptySlot;

    public void AddItemToSlot(Item_SO newItem)
    {
        if(Item != null && Item.isStackable)
        {
            UpdateStackSize();
            return;
        }
        else if(Item != null && !Item.isStackable)
        {
            return;
        }
        Item = newItem;
        icon.sprite = Item.itemSprite;
        icon.enabled = true;
        UpdateStackSize();
        isEmptySlot = false;
    }

    public void ClearSlot()
    {
        Item = null;
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
        if(Item != null)
        {
            Item.UseItem(Item);
            UpdateStackSize();
        }

        if(Item.stackSize == 0)
        {
            ClearSlot();
        }
    }

    public void UpdateStackSize()
    {
        if (Item.isStackable == true && Item.stackSize >= 1)
        {
            itemAmount.text = Item.stackSize.ToString();
            itemAmount.enabled = true;
        }
    }
}