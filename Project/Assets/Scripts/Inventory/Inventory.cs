using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[Inventory] There is more then one Inventory Instance");
            return;
        }
        Instance = this;
    }

    #endregion

    private void Start()
    {
        InventoryManager.Instance.LoadInventory(this.transform);
    }
}
