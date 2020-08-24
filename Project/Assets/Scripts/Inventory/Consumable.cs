using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Consumable", menuName = "Items/Consumable")]
public class Consumable : Item
{
    public float heal=0;
    public override void Use()
    {
        Health playerHealth = GameObject.Find("HealthBar").GetComponent<Health>();
        playerHealth.Heal(heal);
        _Inventory.instance.Remove(this);
    }
}
