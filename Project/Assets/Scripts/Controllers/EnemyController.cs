using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player;

    public Rigidbody rb;
    public NavMeshAgent agent;

    public float aggroDistance;
    float distance;
    public float gizmoRadius = 5f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);

        if(distance <= aggroDistance)
        {
            agent.SetDestination(player.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, gizmoRadius);
    }


}
