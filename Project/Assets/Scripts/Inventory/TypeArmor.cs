using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Armor")]
public class TypeArmor : Item
{
    public float givearmor = 0;

    public override void Use() //Armor Use Effect
    {
        Debug.Log("You used Armor Item."); //will be removed after tests and bugfixes

        Armor playerArmor = GameObject.Find("ArmorBar").GetComponent<Armor>();
        if(playerArmor.currentArmor >= 100)
        {
            Debug.Log("You have full armor. You cant use Armor");
        }
        else
        {
            playerArmor.GiveArmor(givearmor);
            _Inventory.instance.Remove(this);
        }
        
    }
}