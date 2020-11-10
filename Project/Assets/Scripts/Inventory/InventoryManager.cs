using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region Singleton

    public static InventoryManager Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("[InventoryManager] There is more then one inventory Instance");
            return;
        }
        Instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    public int maxSlots   = 12;
    public int emptySlots = 12;

    #region Events

    //whenever a change in the items occur, call this function
    public delegate void OnChangedItem();
    public OnChangedItem onChangedItemCall;

    #endregion

    [SerializeField] 
    private GameObject      inventoryUI;
    private InventorySlot[] inventorySlots;

    [SerializeField]
    private List<Item_SO> items = new List<Item_SO>();
    private void Start()
    {
        onChangedItemCall += UpdateInventorySlots;
    }

    public void LoadInventory(Transform invetoryTransform)
    {
        // Inventory should be first child object for "Menu Items" game object (you can find it in Scene via UI Canvas -> Top Menu -> Menu Items)
        //inventoryUI = invetoryTransform.GetChild(0).gameObject;
        inventorySlots = invetoryTransform.GetComponentsInChildren<InventorySlot>(true);
        onChangedItemCall?.Invoke();
    }

    #region Item Handling

    public bool AddItem(Item_SO item)
    {
        if (emptySlots > 0)
        {
            foreach (InventorySlot slot in inventorySlots)
            {
                if (slot.isEmptySlot)
                {
                    slot.AddItemToSlot(item);
                    items.Add(item);
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

        onChangedItemCall?.Invoke();
        item.stackSize++;
        return true;
    }

    public void RemoveItem(Item_SO item)
    {
        items.Remove(item);
        item.RemoveItem(item);

        onChangedItemCall?.Invoke();
    }

    #endregion

    #region Updating UI

    public void UpdateInventorySlots()
    {
        emptySlots = 0;
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if(inventorySlots[i].Item == null)
            {
                inventorySlots[i].ClearSlot();
                emptySlots++;
            }
        }
    }

    public void ToggleInventory()
    {
        //onChangedItemCall?.Invoke();
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
                InventorySlot tmpSlot = slot.GetComponent<InventorySlot>();
                var itemTmp = tmpSlot.Item;
                if (itemTmp != null && itemTmp.itemType == item.itemType && itemTmp.stackSize < item.maxStackSize)
                {
                    tmpSlot.AddItemToSlot(item);
                    itemTmp.stackSize++;
                    Debug.Log("[Inventory Manager] Item added to the stack of " + item.itemName);
                    itemAdded = true;
                    break;
                }
            }

            if (itemAdded)
            {
                onChangedItemCall?.Invoke();
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
        }

        return false;
    }

    public void RemoveItemFromInventory(Item_SO item)
    {
        if(item.stackSize == 0)
        {
            items.Remove(item);
        }
        else
        {
            Debug.LogWarning("[Inventory Manager] There is a call to remove item with more than one stack size");
        }

        onChangedItemCall?.Invoke();
    }

    #endregion
}