using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Inventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    public static _Inventory instance; 
   
    void Start()
    {
        instance = this;
    }


}
