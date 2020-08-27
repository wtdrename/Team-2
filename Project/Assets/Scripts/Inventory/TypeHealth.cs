using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Item", menuName = "Items/Health")]
public class TypeHealth : Item 
{

    public float heal=0;
    
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
}
