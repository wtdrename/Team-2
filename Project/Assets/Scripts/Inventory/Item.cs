using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : ScriptableObject
{
    public enum itemtype { WEAPON, ARMOR,HEALTH,AMMO,EMPTY }
    public itemtype ItemType;
    public string itemName;
    public Sprite icon;
    public int ItemAmount;



    public virtual Item GetCopy()
    {
        return this;
    }

    public itemtype CurrentItemType   // property
    {
        get { return ItemType; }   // get method
        set { ItemType = value; }  // set method
    }


    public virtual void Use()
    {

    }

   


}
