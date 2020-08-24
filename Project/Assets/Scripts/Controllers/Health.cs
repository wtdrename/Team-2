using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //Prototype. Not Usable for now
    //-batur
    private Slider healthBar;
    public float currentHealth=50, maxHealth=100;


    void Awake()
    {
        healthBar = GetComponent<Slider>();

        // IsTest = true;
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            maxHealth = currentHealth;
        }

        healthBar.value = currentHealth;

    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        healthBar.value = currentHealth;
    }

}
