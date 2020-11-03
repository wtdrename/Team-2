using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class BasicTakingDamage : MonoBehaviour, IAttackable
{
    private CharacterStats stats;

    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
    }
    public void OnAttack(GameObject attacker, Attack attack)
    {
        if (gameObject.GetComponent<PlayerManager>())
        {
            gameObject.GetComponent<PlayerManager>().TakeDamage(attack.Damage);

        }
        if (gameObject.GetComponent<EnemyController>())
        {
            gameObject.GetComponent<EnemyController>().TakeDamage(attack.Damage);
        }

        if (stats.GetHealth() <= 0)
        {
            var destructibles = GetComponents<IDestructable>();
            foreach (IDestructable d in destructibles)
            {
                d.OnDestruct(attacker);
            }
            }
    }
}
