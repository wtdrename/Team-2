using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent agent;
    public Rigidbody rb;
    public float speed = 6f;

    public Camera camera;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        camera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookat = new Vector3(0f, Input.mousePosition.y, 0f);
        Vector3 lookpos = camera.ScreenToWorldPoint(lookat);
        transform.LookAt(lookat);

        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horInput, 0f, verInput);
        Vector3 moveDestination = transform.position + movement;
        agent.destination = moveDestination;

        InventoryOpenClose.Menu(); //Inventory Control
    }
}
