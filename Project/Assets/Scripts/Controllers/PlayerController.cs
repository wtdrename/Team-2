using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent agent;
    public Joystick joystick;
    public float speed = 6f;
    public float rotationSpeed = 10f;
    public Camera camera;

    float horizontalMove = 0f;
    float verticalMove = 0f;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        camera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {

        #region Joystick and Movement Controllers

        //gets the movement of the joystick
        if (joystick.Horizontal >= 0.3f)
        {
            horizontalMove = speed;
        }
        else if(joystick.Horizontal <= -0.3f)
        {
            horizontalMove = -speed;
        }
        else
        {
            horizontalMove = 0f;
        }

        if (joystick.Vertical >= 0.3f)
        {
            verticalMove = speed;
        }
        else if (joystick.Vertical <= -0.3f)
        {
            verticalMove = -speed;
        }
        else
        {
            verticalMove = 0f;
        }

        //gets the movement and moves the agent
        Vector3 movement = new Vector3(horizontalMove, 0f, verticalMove);
        Vector3 moveDestination = transform.position + movement;
        agent.destination = moveDestination;
        if(movement != Vector3.zero)
        {
            Vector3 dir = moveDestination - transform.position;
            dir.y = 0;//This allows the object to only rotate on its y axis
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
        }

        #endregion

        //InventoryOpenClose.Menu(); 
        /* Removed TAB Key Event.
         * Inventory now opens when you click on the Inventory Bag image on the UI. 
         * 
         * 
         */
    }
}
