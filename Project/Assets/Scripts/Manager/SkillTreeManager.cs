using TMPro;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    #region Initializations

    private byte availableAttributePoints;

    public GameObject attributePanel;

    public TextMeshProUGUI availablePointsText;

    public AttributeUI[] attributePFs;

    
    #endregion

    #region Singleton
    
    public static SkillTreeManager Instance { private set; get; }
    
    void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[SkillTreeManager] There is more than one skill tree Instance");
            return;
        }
        Instance = this;
    }
    
    #endregion

    public void ToggleSkillPanel()
    {
        attributePanel.SetActive(!attributePanel.activeSelf);
    }

    public void AddSkillPoints(byte numberOfPoints)
    {
        availableAttributePoints += numberOfPoints;
        UpdateAvailablePoints();
    }

    #region Methods used on the buttons in the skill panel

    public void AddAttribute(AttributeUI attribute)
    {
        if(attribute == null)
        {
            return;
        }

        attribute.AddAttribute();

        availableAttributePoints--;

        UpdateAvailablePoints();
    }
    #endregion

    #region Update UI

    public void UpdateAvailablePoints()
    {
        availablePointsText.text = availableAttributePoints.ToString();

        for (int i = 0; i < attributePFs.Length; i++)
        {
            if (availableAttributePoints > 0)
            {
                attributePFs[i].IsActive(true);
            }
            else
            {
                attributePFs[i].IsActive(false);
            }

        }

        PlayerManager.Instance.RefreshStats();
        UpdateStatsText();
    }

    private void UpdateStatsText()
    {
        foreach(AttributeUI ui in attributePFs)
        {
            ui.UpdateText();
        }
    }
    
    #endregion
    
}
