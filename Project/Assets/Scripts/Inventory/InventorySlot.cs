using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item_SO item;
    public Image icon;

    public Text itemAmount;


    public void AddItemToSlot(Item_SO newItem)
    {
        item = newItem;
        icon.sprite = item.itemSprite;
        icon.enabled = true;
        if(item.isStackable == true)
        {
            itemAmount.text = "1";
            itemAmount.enabled = true;
        }

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
    }
    public void UseItem()
    {
        if(item != null)
        {
            item.UseItem(item);
        }
    }
}
/*public Button UI_InventoryBag;
    


    void Start()
    {
        
        Button btn = UI_InventoryBag.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        GameObject isOpen = _Inventory.instance.inventoryPanel;
        isOpen.SetActive(false);
    }

    public void TaskOnClick()
    {
        GameObject isOpen = _Inventory.instance.inventoryPanel;

        if (!isOpen.activeSelf)
        {
            isOpen.SetActive(true);
        }
        else
        {
            isOpen.SetActive(false);
        }
    }
       
*/