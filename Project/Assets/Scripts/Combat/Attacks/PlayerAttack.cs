using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IAttackable
{
    public void OnAttack(GameObject attacker, Attack attack)
    {
        if(gameObject.GetComponent<PlayerManager>())
        {
            gameObject.GetComponent<PlayerManager>().TakeDamage(attack.Damage);
        }
        if (gameObject.GetComponent<EnemyController>())
        {
            gameObject.GetComponent<EnemyController>().TakeDamage(attack.Damage);
        }
    }

}
