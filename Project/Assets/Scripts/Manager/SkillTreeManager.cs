using TMPro;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    #region Initializations

    private byte actualSkillPoints;

    public GameObject skillPanel;

    public GameObject[] increaseStatsButtons;
    public TextMeshProUGUI availablePointsText;

    private CharacterStats_SO characterStatsSO;

    [Header("Actual stats points text variables")]
    public TextMeshProUGUI armor;
    public TextMeshProUGUI health;
    public TextMeshProUGUI shield;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI critChance;
    
    #endregion

    #region Singleton
    
    public static SkillTreeManager instance { private set; get; }
    
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("[SkillTreeManager] There is more than one skill tree instance");
            return;
        }
        instance = this;
    }
    
    #endregion

    private void Start()
    {
        characterStatsSO = PlayerManager.instance.playerStats.stats;
        UpdateAvailablePoints();
    }

    private void Update()  //Listener
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            skillPanel.SetActive(!skillPanel.activeSelf);
        }
    }

    public void ToggleSkillPanel()
    {
        skillPanel.SetActive(!skillPanel.activeSelf);
    }

    public void AddSkillPoints(byte numberOfPoints)
    {
        actualSkillPoints += numberOfPoints;
        UpdateAvailablePoints();
    }

    #region Methods used on the buttons in the skill panel

    public void AddPointsToHealth()
    {
        characterStatsSO.maxHealth += 5;
        actualSkillPoints--;
        UpdateAvailablePoints();
    }

    public void AddPointsToArmor()
    {
        characterStatsSO.baseArmor += 5;
        actualSkillPoints--;
        UpdateAvailablePoints();
    }

    public void AddPointsToShield()
    {
        characterStatsSO.maxShield += 10;
        actualSkillPoints--;
        UpdateAvailablePoints();
    }

    public void AddPointsToDamage()
    {
        characterStatsSO.baseDamage += 5;
        actualSkillPoints--;
        UpdateAvailablePoints();
    }

    public void AddPointsToCritChance()
    {
        characterStatsSO.criticalChance += 1f;
        actualSkillPoints--;
        UpdateAvailablePoints();
    }
    
    #endregion

    #region Update UI

    private void UpdateAvailablePoints()
    {
        availablePointsText.text = actualSkillPoints.ToString();
        if (actualSkillPoints > 0)
        {
            foreach (GameObject button in increaseStatsButtons)
            {
                button.SetActive(true);
            }
        }else
        {
            foreach (GameObject button in increaseStatsButtons)
            {
                button.SetActive(false);
            }
        }

        UpdateStatsText();
        PlayerManager.instance.RefreshStats();
    }

    private void UpdateStatsText()
    {
        armor.text = characterStatsSO.baseArmor.ToString();
        health.text = characterStatsSO.maxHealth.ToString();
        shield.text = characterStatsSO.maxShield.ToString();
        damage.text = characterStatsSO.baseDamage.ToString();
        critChance.text = characterStatsSO.criticalChance.ToString("#.00") +" %";
    }
    
    #endregion
    
}
