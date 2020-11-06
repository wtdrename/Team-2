using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    #region Initializers

    public NavMeshAgent agent;
    public Joystick joystick;

    public TouchFieldDrag touchField;
    public float touchPlayerRotation = 0.2f;

    public float speed = .5f;
    public float rotationSpeed = 10f;
    public new Camera camera;

    float horizontalMove = 0f;
    float verticalMove = 0f;

    #endregion

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (touchField == null)
        {
            touchField = FindObjectOfType<TouchFieldDrag>();
        }
        camera = Camera.main;
    }

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

        #region Rotation

        if (touchField.Pressed)
        {
            Vector3 dir = touchField.TouchDist;
            Vector3 rot = new Vector3 (0, dir.x);
            transform.Rotate(rot);
        }


        #endregion
    }
}
