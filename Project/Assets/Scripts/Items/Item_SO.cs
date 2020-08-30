using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
[CreateAssetMenu(fileName ="New Item",menuName ="Items/New Item")]
public class Item_SO : ScriptableObject
{

    #region Initializers

    public ItemType itemType = ItemType.HEALTH;
    public string itemName;
    public Sprite itemSprite;


    public int itemAmount = 0;
    public bool isStackable = true;
    public bool isConsumable = true;

    #endregion

    public virtual Item_SO GetCopy()
    {
        return this;
    }

    public ItemType CurrentItemType   
    {
        get { return itemType; }
    }


    public void UseItem(Item_SO item)
    {
        Debug.Log("[Item_SO] Using the item: " + item.itemName);
        switch (item.itemType)
        {
            //needs to add remove item
            case ItemType.HEALTH:
                PlayerManager.instance.playerStats.GiveHealth(item.itemAmount);
                RemoveItem(item);
                break;
            case ItemType.ARMOR:
                //eqiping armor
                RemoveItem(item);
                break;
            case ItemType.WEAPON:
                //eqiping weapon
                RemoveItem(item);
                break;
            case ItemType.AMMO:
                //giving ammo
                RemoveItem(item);
                break;
        }
    }
    public void RemoveItem(Item_SO item)
    {

    }
    public void AddItem(Item_SO item)
    {

    }


    /*use items
     *     public float givearmor = 0;
    public override void Use() //Armor Use Effect
    {
        Debug.Log("You used Armor Item."); //will be removed after tests and bugfixes

        Armor playerArmor = GameObject.Find("ArmorBar").GetComponent<Armor>();
        if (playerArmor.currentArmor >= 100)
        {
            Debug.Log("You have full armor. You cant use Armor");
        }
        else
        {
            playerArmor.GiveArmor(givearmor);
            _Inventory.instance.Remove(this);
        }

    }    public float heal=0;
    
    public override void Use()
    {
        //will be removed after tests and bugfixes
        Debug.Log("You used Health Item.");


        Health playerHealth = GameObject.Find("HealthBar").GetComponent<Health>();
        if(playerHealth.currentHealth >= 100)
        {
            Debug.Log("You have full health. You cant use Medkit");
        }
        else
        {
            playerHealth.Heal(heal);
            _Inventory.instance.Remove(this);
        }
       

    }
    public override void Use() //Ammo Use Effect
    {
        //Health playerHealth = GameObject.Find("HealthBar").GetComponent<Health>();
        //playerHealth.Heal(heal);
        //_Inventory.instance.Remove(this);

        Debug.Log("You used Ammo Item.");

    }
}

*/
}
public enum ItemType { WEAPON, ARMOR, HEALTH, AMMO, EMPTY }
