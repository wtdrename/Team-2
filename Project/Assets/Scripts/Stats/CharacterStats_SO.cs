using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Character Stats", menuName ="Character Stats/Stats")]
public class CharacterStats_SO : ScriptableObject
{

    #region Defenitions

    public int level = 1;
    public int currentExp = 0;
    public int maxExp = 100;
    public float expMaxlvlMultiplier = 1.25f; //how much the maxExp will increase on each level


    public int maxHealth;
    public int currentHealth;
    public int maxShield;
    public int currentShield;

    public int baseArmor;
    public int currentArmor;
    public int baseDamage;
    public int currentDamage;

    public float criticalChance;

    public int currentCredit;

    #endregion

    #region Increasers
    public void GiveHealth(int amount)
    {
        if (currentHealth + amount >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }
    }
    public void GiveShield(int amount)
    {
        if (currentShield + amount >= maxShield)
        {
            currentShield = maxShield;
        }
        else
        {
            currentShield += amount;
        }
    }
    public void GiveCredit(int amount)
    {
        currentCredit += amount;
    }

    public void GiveExp(int amount)
    {
        currentExp += amount;
        if (currentExp >= maxExp)
        {
            while (currentExp >= maxExp) //level up
            {
                level++;
                currentExp -= maxExp;
                maxExp = (int) (maxExp * expMaxlvlMultiplier);
                PlayerManager.instance.LevelUpEventCall();
            }
        }else
        {
            PlayerManager.instance.ExpChangeEventCall();
        }
    }

    public void IncreaseStats() //method that will be called on level up
    {
        //temporal values
        maxHealth += 10;
        maxShield += 8;
        baseArmor += 3;
    }
    
    
    #endregion

    #region Decreasers
    public void TakeDamage(int amount)
    {
        if (currentHealth - amount <= 0)
        {
            currentHealth = 0;
            //die
        }
        else
        {
            currentHealth -= amount;
        }
    }
    public void LoseShield(int amount)
    {
        if (currentShield - amount <= 0)
        {
            currentShield = 0;
            TakeDamage(amount - currentShield);
        }
        else
        {
            currentShield -= amount;
        }
    }
    public void TakeCredit(int amount)
    {
        if(currentCredit - amount < 0)
        {
            Debug.Log("Not enought Credits");
            return;
        }
        currentCredit -= amount;
    }
    #endregion
}
