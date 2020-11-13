using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCache : MonoBehaviour
{
    #region Singleton

    public static InventoryCache Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[InventoryCache] There is more than one Inventory Cache Instance");
            return;
        }
        Instance = this;
    }

    #endregion

    [SerializeField]
    private List<Item_SO> items = new List<Item_SO>();

    public bool AddItemToInventory(Item_SO item)
    {
        if (item.isStackable)
        {
            Item_SO ownedItem;
            for (int i = 0; i < items.Count; i++)
            {
                ownedItem = items[i];
                if (ownedItem != null && ownedItem.itemType == item.itemType)
                {
                    if (item.itemType == ItemType.WEAPON) break;

                    ownedItem.UseItem(item);
                    Debug.Log("[Inventory Cache] Item added to the stack of " + item.itemName);

                    return true;
                }
            }
        }

        return AddItem(item);
    }

    public bool AddItem(Item_SO item)
    {
        items.Add(item);
        Debug.Log("[Inventory Cache] Item added");

        return true;
    }
}
