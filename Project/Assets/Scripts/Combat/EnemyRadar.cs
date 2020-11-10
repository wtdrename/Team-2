using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadar : MonoBehaviour
{
    private GameObject[] multipleEnemys;
    public Transform closestEnemy;
    public bool enemyContact;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {        
        closestEnemy = null;
        enemyContact = false;
    }
        

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            print("trigger");
            closestEnemy = getClosestEnemy();

            enemyContact = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ( other.CompareTag("Enemy"))
        {
            enemyContact = false;
        }
    }

    public Transform getClosestEnemy()
    {
        multipleEnemys = GameObject.FindGameObjectsWithTag("Enemy");
        float closetDistance = 100;
        Transform trans = null;

        foreach (GameObject GO in multipleEnemys)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, GO.transform.position);
            if (currentDistance < closetDistance)
            {
                closetDistance = currentDistance;
                trans = GO.transform;
            }
        }
        return trans;
    }
}
