using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Attack.asset", menuName ="Attack/New Attack")]
public class AttackDefenition : ScriptableObject
{
    public float coolDown;

    public float range;

    public float criticalMultiplier = 2;
    public float criticalChance = 1;

    public Attack CreateAttack (CharacterStats attacker, CharacterStats defender)
    {
        float baseDamage = attacker.GetDamage();
        float critChance = criticalChance + attacker.GetCriticalChance();

        bool isCritical = Random.value < critChance;
        if (isCritical)
        {
            baseDamage = baseDamage * criticalMultiplier;
        }
        if(defender != null)
        {
            baseDamage -= defender.GetArmor();
        }

        return new Attack((int)baseDamage, isCritical);
    }
}
