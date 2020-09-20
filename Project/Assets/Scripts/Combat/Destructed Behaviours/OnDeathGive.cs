using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathGive : MonoBehaviour, IDestructable
{
    public void OnDestruct(GameObject destroyer)
    {
        var expToGive = GetComponent<CharacterStats>();
        var whoToGive = destroyer.GetComponent<CharacterStats>();

        whoToGive.GiveExp(expToGive.GetExpOnDeath());
    }

}
