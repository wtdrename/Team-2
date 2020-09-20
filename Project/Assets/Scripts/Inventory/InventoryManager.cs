using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    #region Singleton

    public static InventoryManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("[InventoryManager] There is more then one inventory instance");
            return;
        }
        instance = this;
    }

    #endregion

    //needs to be max slots -1 because of 0 in lists
    public int maxSlots = 11;
    public int emptySlots = 11;


    #region Events
    //whenever a change in the items occur, call this function
    public delegate void OnChangedItem();
    public OnChangedItem onChangedItemCall;
    #endregion

    public Transform inventorySlotsParent;
    InventorySlot[] inventorySlots;

    public GameObject inventoryUI;

    public List<Item_SO> items = new List<Item_SO>();
    void Start()
    {
        onChangedItemCall += UpdateInventorySlots;
        inventorySlots = inventorySlotsParent.GetComponentsInChildren<InventorySlot>();
        UpdateInventorySlots();
    }

    #region Item Handling

    public bool AddItem(Item_SO item)
    {
        if (emptySlots > 0)
        {
            foreach (InventorySlot slot in inventorySlots)
            {
                var tmp = slot.GetComponent<InventorySlot>();
                if (tmp.isEmptySlot)
                {
                    tmp.AddItemToSlot(item);
                    emptySlots--;
                    Debug.Log("[Inventory Manager] Item added to inventory");
                    break;
                }
            }
        }
        else
        {
            Debug.Log("[InventoryManager - AddItem] Inventory is full.");
            return false;
        }

        if (onChangedItemCall != null)
        {
            onChangedItemCall.Invoke();
        }
        item.stackSize++;
        return true;
    }
    public void RemoveItem(Item_SO item)
    {
        items.Remove(item);
        item.RemoveItem(item);

        if (onChangedItemCall != null)
        {
            onChangedItemCall.Invoke();
        }

    }

    #endregion

    #region Updating UI

    public void UpdateInventorySlots()
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            
            if(inventorySlots[i].GetItem() == null)
            {
                inventorySlots[i].ClearSlot();

                emptySlots++;
            }
            else
            {
                inventorySlots[i].AddItemToSlot(items[i]);
                emptySlots--;
            }
        }
        if (emptySlots > 12)
        {
            emptySlots = 12;
        }
        if (emptySlots < 0)
        {
            emptySlots = 0;
        }
    }

    public void ToggleInventory()
    {
        UpdateInventorySlots();
        if (inventoryUI.activeSelf == true)
        {
            inventoryUI.SetActive(false);
        }
        else
        {
            inventoryUI.SetActive(true);
        }
    }

    public bool AddItemToInventory(Item_SO item)
    {
        bool itemAdded = false;
        if (item.isStackable)
        {
            foreach (InventorySlot slot in inventorySlots)
            {
                var tmp = slot.GetComponent<InventorySlot>();
                var itemTmp = tmp.GetItem();
                if (itemTmp != null && itemTmp.isStackable && itemTmp.stackSize < item.maxStackSize)
                {
                    tmp.AddItemToSlot(item);
                    itemTmp.stackSize++;
                    Debug.Log("[Inventory Manager] Item added to the stack of " + item.itemName);
                    itemAdded = true;
                    break;
                }
            }
            if (itemAdded)
            {
                if (onChangedItemCall != null)
                {
                    onChangedItemCall.Invoke();
                }
                return true;
            }
        }
        if(emptySlots > 0)
        {
            return AddItem(item);
        }
        else
        {
            Debug.Log("[Inventory Manager] inventory is full");
            return false;
        }

    }

    public void RemoveItemFromInventory(Item_SO item)
    {
        if(item.stackSize == 0)
        {
            items.Remove(item);
        }
        else
        {
            Debug.Log("[Inventory Manager] There is a call to remove item with more than one stack size");
        }
        UpdateInventorySlots();

    }
    #endregion
}

/*
 * 
    public void AddItemToInventory(Item item)
    {
        if(list.Count < 9)
        {
            list.Add(item);
        }
        updatePanelSlots();
    }

    public void Remove(Item item)
    {
        list.Remove(item);
        updatePanelSlots();

    }

*/