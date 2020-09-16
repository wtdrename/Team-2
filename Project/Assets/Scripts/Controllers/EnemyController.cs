using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public GameObject player;

    public Rigidbody rb;
    public NavMeshAgent agent;
    public EnemyAnimationController animationController;

    public CharacterStats enemyStats;
    public StatusBar healthBar;

    public AttackDefenition attack;

    private float timeOfLastAttack;

    private bool isAlive = true;
    public float aggroDistance;
    float distance;
    public float gizmoRadius = 5f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        animationController = GetComponent<EnemyAnimationController>();

        enemyStats = GetComponent<CharacterStats>();
        UpdateHealthSlider();

        timeOfLastAttack = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled == true)
        {
            distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance <= aggroDistance)
            {
                agent.SetDestination(player.transform.position);
                animationController.EnemyMovement();
            }
            else if (distance > aggroDistance * 1.5)
            {
                agent.SetDestination(transform.position);
                animationController.EnemyMovement();
            }


            float timeSinceLastAttack = Time.time - timeOfLastAttack;
            bool attackOnCoolDown = timeSinceLastAttack < attack.coolDown;
            bool attackInRange = distance < attack.range;
            agent.isStopped = attackOnCoolDown;
            if (!attackOnCoolDown && attackInRange)
            {
                agent.velocity = Vector3.zero;

                animationController.EnemyAttackAnimation();
                transform.LookAt(player.transform);
                timeOfLastAttack = Time.time;
            }
        }

    }

    #region Attack and Defence

    //executed by the animation event "Hit"
    public void Hit()
    {
        attack.ExecuteAttack(gameObject, player);
    }

    //called on DestroyObject script
    public void Dying()
    {
        if (isAlive)
        {
            isAlive = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            animationController.EnemyDiesAnimation();
            agent.enabled = false;
        }
    }

    #endregion


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
        if (healthBar != null)
        {
            healthBar.UpdateSlider((float)enemyStats.stats.currentHealth / (float)enemyStats.stats.maxHealth);
        }
        else
        {
            healthBar = GetComponentInChildren<StatusBar>();
            healthBar.UpdateSlider((float)enemyStats.stats.currentHealth / (float)enemyStats.stats.maxHealth);
        }
        //add an if for armor / shield
        //healthBar.TakingDamage(amount, playerStats.stats);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, gizmoRadius);
    }


}
