﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerManager : MonoBehaviour
{
        #region Singleton

        public static PlayerManager Instance;

        public int maxSlots = 12;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("[PlayerManager] There is more then one player Instance");
                return;
            }
            Instance = this;
        }

        #endregion Singleton

        #region Events

        public event EventHandler OnLevelChanged;

        public event EventHandler OnExpChanged;

        #endregion Events

        #region Initializations

        [Header("Health Bar")]
        public StatusBar healthBar;

        public TextMeshProUGUI healthBarText;

        [Header("Exp Bar")]
        public StatusBar expBar; //needs to be assigned

        public TextMeshProUGUI expBarText;

        [Header("Level and Stats")]
        public CharacterStats playerStats;

        public TextMeshProUGUI levelText;

        public PlayerAnimator playerAnimator;

        public Button attackButton;
        public AttackDefenition baseAttack;

        public ProjectileManager projectileManager;

        public Item_SO weapon;
        public TextMeshProUGUI ammoAmountText;

        //shooting variables
        private bool readyToShoot = true;

        private bool reloading = false;
        public bool isWeaponRaycast;

    public MissionInventory missionInventory;

    #endregion Initializations

        #region Start and Update

        private void Start()
        {
            playerStats = GetComponent<CharacterStats>();
            projectileManager = GetComponent<ProjectileManager>();

            //Initialize the two events with their method
            OnLevelChanged += OnLevelChange;
            OnExpChanged += OnExpChange;

            //updates all the UI
            UpdateAmmoText();
            RefreshStats();
            UpdateLevelText();
            //missionInventory.ResetBag();
            SkillTreeManager.Instance.UpdateAvailablePoints();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                TakeDamage(10);
                UpdateHealthSlider();
            }

            if (Input.GetKeyDown(KeyCode.L)) //Debug purposes, can be removed at any time
            {
                GiveExp(40);
                Debug.Log($"Actual level:{playerStats.GetLevel()} \n Actual Exp:{playerStats.GetActualExp()}   Actual Max EXP:{playerStats.GetMaxExp()}");
                //Remember to delete the Debug.log in the OnLevelChange() method when you are not longer going to use this debug tool.
            }

            //I dont have Smaprthone so I am shooting with mouse click
            if (Input.GetMouseButtonDown(0))
                projectileManager.ShootWeapon(isWeaponRaycast);
        }

        #endregion Start and Update

        #region Animations

        public void ShootingAnimation()
        {
            playerAnimator.Shooting();
        }

        #endregion Animations

        #region Pickup via Collision
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<ItemPickup>())
            {
                var item = other.GetComponent<ItemPickup>();
                if (item)
                {
                    missionInventory.AddItem(item.item, 1);
                    Destroy(other.gameObject);
                }
            }
        }

        #endregion Pickup via Collision

        #region UI Updates

        public void UpdateHealthSlider()
        {
            UpdateStatusBarText(healthBarText, playerStats.GetHealth().ToString(), playerStats.GetMaxHealth().ToString());
            healthBar.UpdateSlider((float)playerStats.GetHealth() / (float)playerStats.GetMaxHealth());
            //add an if for armor / shield
            //healthBar.TakingDamage(amount, playerStats.stats);
        }

        public void UpdateArmorSlider()
        {
        }

        public void UpdateExpSlider()
        {
            UpdateStatusBarText(expBarText, playerStats.GetActualExp().ToString(), playerStats.GetMaxExp().ToString());
            expBar.UpdateSlider((float)playerStats.GetActualExp() / (float)playerStats.GetMaxExp());
        }

        public void UpdateLevelText()
        {
            levelText.text = "Level " + playerStats.GetLevel().ToString();
        }

        public void UpdateAmmoText()
        {
            ammoAmountText.text = weapon.currentAmmo + " / " + weapon.magazineSize + " (" + weapon.ammoAmountInInv + ") ";
        }

        public void UpdateStatusBarText(TextMeshProUGUI barText, string min, string max)
        {
            barText.text = min + " / " + max;
        }

        #endregion UI Updates

        #region Stats Updates

        public void RefreshStats()
        {
            playerStats.stats.currentDamage = playerStats.stats.baseDamage;
            playerStats.stats.currentArmor = playerStats.stats.baseArmor;
            UpdateArmorSlider();
            UpdateExpSlider();
            UpdateHealthSlider();
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
            if (playerStats.GetHealth() <= 0)
            {
                Debug.Log("Player Died");

                GameManager.Instance.DeathEventCall();
            }

            UpdateHealthSlider();
        }

        public void TakeCredit(int amount)
        {
            playerStats.TakeCredit(amount);
            //update inventory event
        }

        #endregion Decreasers

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

        public void Shooting()
        {
            if (readyToShoot == false || reloading == true)
            {
                return;
            }

            readyToShoot = false;
            if (weapon.currentAmmo > 0)
            {
                weapon.currentAmmo--;
                if (weapon.ammoAmountInInv > 0)
                {
                    weapon.ammoAmountInInv--;
                }

                projectileManager.ShootWeapon(isWeaponRaycast);
                AudioManager.Instance.Play("Shoot");

                UpdateAmmoText();
                Invoke("ResetShot", weapon.shotsPerSec);
            }
            else if (weapon.currentAmmo == 0 && weapon.ammoAmountInInv != 0)
            {
                Invoke("Reload", 0f);
            }
            else if (weapon.currentAmmo == 0 && weapon.ammoAmountInInv == 0)
            {
                Debug.Log("[Player Manager] There is not enough ammo in the inventory");
                ResetShot();
                return;
            }
            else
            {
                Debug.Log("[Player Manager] You dont have enought ammo!");
                ResetShot();
                return;
            }
        }

        private void ResetShot()
        {
            readyToShoot = true;
        }

        private void Reload()
        {
            reloading = true;
            AudioManager.Instance.Play("Reload");
            Invoke("ReloadFinished", weapon.reloadTime);
        }

        private void ReloadFinished()
        {
            //if there is not enough ammo in the inventory, only load the amount u have
            if (weapon.ammoAmountInInv < weapon.magazineSize)
            {
                weapon.currentAmmo = weapon.ammoAmountInInv;
                weapon.ammoAmountInInv -= weapon.currentAmmo;
                UpdateAmmoText();
                reloading = false;
                ResetShot();
                return;
            }

            //reset magazine and remove the ammo from the inventory
            weapon.currentAmmo = weapon.magazineSize;
            weapon.ammoAmountInInv -= weapon.magazineSize;
            UpdateAmmoText();
            reloading = false;
            ResetShot();
        }

        #endregion Attacking

        #region Event Calls

        public void LevelUpEventCall()
        {
            OnLevelChanged?.Invoke(this, EventArgs.Empty);
        }

        public void ExpChangeEventCall()
        {
            OnExpChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion Event Calls

        #region Leveling Up and EXP

        private void OnLevelChange(object sender, EventArgs e) //Method called onlevelchanged event
        {
            //Visual effects or things that happen on the event of leveling up
            playerStats.LevelUpStatsChange();
            UpdateExpSlider();
            UpdateLevelText();
            RefreshStats();
            SkillTreeManager.Instance.AddSkillPoints(3);
            Debug.Log($"LEVEL UP! \n New Stats:  MAXHEALTH = {playerStats.stats.maxHealth} MAXSHIELD = {playerStats.stats.maxShield} BASEARMOR = {playerStats.stats.baseArmor}  "); //Debug purposes, can be removed at any time
        }

        private void OnExpChange(object sender, EventArgs e) //Method called onexpchanged event
        {
            //Visual effects or things that happen on the event of getting exp
            UpdateExpSlider();
        }

        #endregion Leveling Up and EXP

        #region Save

        public void SaveStats()
        {
            playerStats.SaveStats();
        }

        #endregion Save
        
}