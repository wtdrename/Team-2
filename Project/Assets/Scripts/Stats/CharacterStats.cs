using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CharacterStats : MonoBehaviour
{
    #region Defenitions

    public CharacterStats_SO stats;

    #endregion

    #region Increasers
    public void GiveHealth(int amount)
    {
        stats.GiveHealth(amount);
    }
    public void GiveShield(int amount)
    {
        stats.GiveShield(amount);
    }
    public void GiveCredit(int amount)
    {
        stats.GiveCredit(amount);
    }
    #endregion

    #region Decreasers
    public void TakeDamage(int amount)
    {
        if(stats.currentHealth - amount <= 0)
        {
            SceneManager.LoadSceneAsync("GameOver");
        }
        else
        {
            stats.TakeDamage(amount);
        }
    }
    public void LoseShield(int amount)
    {
        int leftOverAmount;

        if (stats.currentShield - amount < 0)
        {
            leftOverAmount = amount - stats.currentShield;
            stats.LoseShield(stats.currentShield);
            stats.TakeDamage(leftOverAmount);
        }
        else
        {
            stats.TakeDamage(amount);
        }
    }
    public void TakeCredit(int amount)
    {
        stats.TakeCredit(amount);
        //update inventory event
    }
    #endregion
}
