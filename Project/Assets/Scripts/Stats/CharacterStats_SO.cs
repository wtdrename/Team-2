using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Character Stats", menuName ="Character Stats/Stats")]
public class CharacterStats_SO : ScriptableObject
{

    #region Defenitions


    [Header("Level")]
    public int level = 1;
    public int currentExp = 0;
    public int maxExp = 100;
    public float expMaxlvlMultiplier = 1.25f; //how much the maxExp will increase on each level
    public int expGivenOnDeath;

    [Header("Status Stats")]
    public int maxHealth;
    public int currentHealth;
    public int maxShield;
    public int currentShield;


    [Header("Attributes")]
    public int baseArmor;
    public int currentArmor;
    public int baseDamage;
    public int currentDamage;
    public float criticalChance;

    [Header("Inventory")]
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

    public void LevelUpStatsChange() //method that will be called on level up
    {
        //temporal values
        maxHealth += 10;
        maxShield += 8;
        baseArmor += 3;

        currentHealth = maxHealth;
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

    #region Save and Load

    public void LoadStats()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            level = PlayerPrefs.GetInt("Level");
            maxShield = PlayerPrefs.GetInt("MaxShield");
            maxHealth = PlayerPrefs.GetInt("MaxHealth");
            maxExp = PlayerPrefs.GetInt("MaxExp");
            currentExp = PlayerPrefs.GetInt("CurrentExp");
            currentCredit = PlayerPrefs.GetInt("Credits");
            baseArmor = PlayerPrefs.GetInt("BaseArmor");
            criticalChance = PlayerPrefs.GetFloat("CriticalChance");
            baseDamage = PlayerPrefs.GetInt("BaseDamage");
        }
    }

    public void SaveStats()
    {
        PlayerPrefs.SetInt("Level",level);
        PlayerPrefs.SetInt("MaxShield",maxShield);
        PlayerPrefs.SetInt("MaxHealth",maxHealth);
        PlayerPrefs.SetInt("MaxExp",maxExp);
        PlayerPrefs.SetInt("CurrentExp",currentExp);
        PlayerPrefs.SetInt("Credits",currentCredit);
        PlayerPrefs.SetInt("BaseArmor",baseArmor);
        PlayerPrefs.SetFloat("CriticalChance",criticalChance);
        PlayerPrefs.SetInt("BaseDamage",baseDamage);
    }

    #endregion
}
