using System;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public Button attackButton;
    public AttackDefenition baseAttack;

    public ProjectileManager projectileManager;

    #endregion

    #region Start and Update
    void Start()
    {
        playerStats = GetComponent<CharacterStats>();
        projectileManager = GetComponent<ProjectileManager>();

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

    #endregion


    #region Increasers
    public void GiveHealth(int amount)
    {
        playerStats.GiveHealth(amount);
    }
    public void GiveShield(int amount)
    {
        playerStats.GiveShield(amount);
    }
    public void GiveCredit(int amount)
    {
        playerStats.GiveCredit(amount);
    }
    #endregion


    #region Decreasers
    public void TakeDamage(int amount)
    {
        playerStats.TakeDamage(amount);
        UpdateHealthSlider();
    }

    public void TakeCredit(int amount)
    {
        playerStats.TakeCredit(amount);
        //update inventory event
    }
    #endregion

    #region Attacking

    public void OnProjectileCollided(GameObject target)
    {
        var attack = baseAttack.CreateAttack(playerStats, target.GetComponent<CharacterStats>());

        var attackable = target.GetComponentsInChildren<IAttackable>();

        foreach (IAttackable e in attackable)
        {
            e.OnAttack(gameObject, attack);
        }
    }

    #endregion
}
