using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject player;

    public Rigidbody rb;
    public NavMeshAgent agent;

    public CharacterStats enemyStats;
    public StatusBar healthBar;

    public float aggroDistance;
    float distance;
    public float gizmoRadius = 5f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");

        enemyStats = GetComponent<CharacterStats>();
        UpdateHealthSlider();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance <= aggroDistance)
        {
            agent.SetDestination(player.transform.position);
        }
        else if (distance > aggroDistance * 1.5)
        {
            agent.SetDestination(transform.position);
        }
    }

    #region Decreasers
    public void TakeDamage(int amount)
    {
        enemyStats.TakeDamage(amount);
        UpdateHealthSlider();
    }

    public void TakeCredit(int amount)
    {
        enemyStats.TakeCredit(amount);
    }
    #endregion


    public void UpdateHealthSlider()
    {
        healthBar.UpdateSlider((float)enemyStats.stats.currentHealth / (float)enemyStats.stats.maxHealth);
        //add an if for armor / shield
        //healthBar.TakingDamage(amount, playerStats.stats);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, gizmoRadius);
    }


}
