using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopMenuController : MonoBehaviour
{
    public Button inventoryButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void inventoryToggle()
    {
        InventoryManager.instance.ToggleInventory();
    }
}
