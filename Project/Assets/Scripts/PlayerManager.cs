using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    #region Singleton

    public static PlayerManager instance;

    public int maxSlots = 12;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("[PlayerManager] There is more then one player instance");
            return;
        }
        instance = this;
    }

    #endregion

    #region Events


    #endregion

    #region Initializations

    public CharacterStats playerStats;
    public StatusBar healthBar;

    #endregion

    #region Start and Update
    void Start()
    {
        playerStats = GetComponent<CharacterStats>();
        //onTakingDamage += TakingDamage;
        //onHealing += IncreasingHealth;

        UpdateHealthSlider();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            playerStats.TakeDamage(10);
            UpdateHealthSlider();
        }
    }

    #endregion

    #region Slider Updates

    public void UpdateHealthSlider()
    {
        healthBar.UpdateSlider((float)playerStats.stats.currentHealth / (float)playerStats.stats.maxHealth);
        //add an if for armor / shield
        //healthBar.TakingDamage(amount, playerStats.stats);
    }
    
    public void UpdateArmorSlider()
    {

    }
    
    public void UpdateExpSlider()
    {

    }
    
    /*public void IncreasingHealth(int amount)
    {
        healthBar.IncreasingHealth(amount, playerStats.stats);
    }*/

    #endregion
}
