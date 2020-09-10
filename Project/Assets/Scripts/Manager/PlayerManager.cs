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

    public event EventHandler OnLevelChanged;
    public event EventHandler OnExpChanged;
    
    #endregion

    #region Initializations

    public CharacterStats playerStats;
    public StatusBar healthBar;
    public StatusBar expBar; //needs to be assigned

    public Button attackButton;
    public AttackDefenition baseAttack;

    public ProjectileManager projectileManager;

    #endregion

    #region Start and Update
    void Start()
    {
        playerStats = GetComponent<CharacterStats>();
        projectileManager = GetComponent<ProjectileManager>();
        
        //Initialize the two events with their method
        OnLevelChanged += OnLevelChange;
        OnExpChanged += OnExpChange;

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

        if (Input.GetKeyDown(KeyCode.L)) //Debug purposes, can be removed at any time
        {
            GiveExp(40);
            Debug.Log($"Actual level:{playerStats.GetLevel()} \n Actual Exp:{playerStats.GetActualExp()}   Actual Max EXP:{playerStats.GetMaxExp()}");
            //Remember to delete the Debug.log in the OnLevelChange() method when you are not longer going to use this debug tool.
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
       // expBar.UpdateSlider((float)playerStats.GetActualExp() / (float)playerStats.GetMaxExp());
       // remove the "//" after having the slider assigned
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

    public void GiveExp(int amount)
    {
        playerStats.GiveExp(amount);
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

    #region Event Calls

    public void LevelUpEventCall()
    {
        OnLevelChanged?.Invoke(this,EventArgs.Empty);
    }

    public void ExpChangeEventCall()
    {
        OnExpChanged?.Invoke(this, EventArgs.Empty);
    }
    
    private void OnLevelChange(object sender, EventArgs e) //Method called onlevelchanged event
    {
        //Visual effects or things that happen on the event of leveling up
        playerStats.IncreaseStats();
        UpdateExpSlider();
        Debug.Log($"LEVEL UP! \n New Stats:  MAXHEALTH = {playerStats.stats.maxHealth} MAXSHIELD = {playerStats.stats.maxShield} BASEARMOR = {playerStats.stats.baseArmor}  "); //Debug purposes, can be removed at any time
    }

    private void OnExpChange(object sender, EventArgs e) //Method called onexpchanged event
    {
        //Visual effects or things that happen on the event of getting exp
        UpdateExpSlider();
    }

    #endregion

    #region Save 

    public void SaveStats()
    {
        playerStats.SaveStats();
    }

    #endregion
}
