using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
public class Item_SO : ScriptableObject
{
    #region Initializers

    public ItemType itemType = ItemType.HEALTH;
    public ArmourType armourType = ArmourType.EMPTY;
    public WeaponType weaponType = WeaponType.EMPTY;

    public string itemName;
    public Sprite itemSprite;

    public int itemAmount = 0;
    public bool isStackable = true;
    public bool isConsumable = true;

    public int stackSize = 0;
    public int maxStackSize = 20;

    //weapon stats
    public int maxAmmo = 0;
    public int currentAmmo = 0;
    public float reloadTime;
    public float shotsPerSec;
    public int magazineSize;
    public int ammoAmountInInv;
    public int weaponDamage;

    //armour stats
    public int armourAmount;

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
        if (item == null) return;

        Debug.Log("[Item_SO] Using the item: " + item.itemName);
        switch (item.itemType)
        {
            //needs to add remove item
            case ItemType.HEALTH:
                Debug.Log("Gave Health");
                PlayerManager.Instance.playerStats.GiveHealth(item.itemAmount);
                PlayerManager.Instance.UpdateHealthSlider();
                break;
            case ItemType.ARMOR:
                //equiping armor
                break;
            case ItemType.WEAPON:
                //equiping weapon
                break;
            case ItemType.AMMO:
                if (item.currentAmmo == item.maxAmmo)
                {
                    Debug.Log("Ammo is full");
                }
                else if (item.currentAmmo + item.itemAmount >= maxAmmo)
                {
                    item.currentAmmo = item.maxAmmo;
                }
                else
                {
                    item.currentAmmo += item.itemAmount;
                }
                break;
        }

        if (item.stackSize >= 1)
        {
            item.stackSize--;
        }
    }

    public void RemoveItem(Item_SO item)
    {

    }

    public void AddItem(Item_SO item)
    {

    }

    public void EquipItem(Item_SO item)
    {

    }

    public void UnequipItem()
    {

    }
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
            _Inventory.Instance.Remove(this);
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
            _Inventory.Instance.Remove(this);
        }
       

    }
    public override void Use() //Ammo Use Effect
    {
        //Health playerHealth = GameObject.Find("HealthBar").GetComponent<Health>();
        //playerHealth.Heal(heal);
        //_Inventory.Instance.Remove(this);

        Debug.Log("You used Ammo Item.");

    }
}

*/

    public enum ArmourType
    {
        HEAD_ITEM,
        CHEST_ITEM,
        TORSO_ITEM,
        LEG_ITEM,
        HAND_ITEM,
        EMPTY
    }

    public enum WeaponType
    {
        PISTOL,
        RIFLE,
        EMPTY
    }

    public enum ItemType
    {
        WEAPON,
        ARMOR,
        HEALTH, 
        AMMO, 
        EMPTY
    }
