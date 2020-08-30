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

    public int maxSlots = 12;



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
    }

    #region Item Handling

    public bool AddItem(Item_SO item)
    {
        items.Add(item);
        if(items.Count >= maxSlots)
        {
            Debug.Log("[InventoryManager - AddItem] Inventory is full.");
            return false;
        }
        item.AddItem(item);

        if(onChangedItemCall != null)
        {
            onChangedItemCall.Invoke();
        }
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
            if(i < items.Count)
            {
                inventorySlots[i].AddItemToSlot(items[i]);
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }
            /* option 2
            if(items[i] == null)
            {
                inventorySlots[i].ClearSlot();
            }
            else
            {
                inventorySlots[i].AddItemToSlot(items[i]);
            }
            */
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
    #endregion
}

/*
 * 
    public void Add(Item item)
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

    void Start()
    {
        instance = this;
        updatePanelSlots();
       

    }
    public static void Menu()
    {    

        if (Input.GetKeyDown(KeyCode.Tab))
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
    }
*/