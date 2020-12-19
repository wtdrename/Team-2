using TMPro;
using UnityEngine;

public class AttributeUI : MonoBehaviour
{

    public AttributeUI_SO attribute;

    private CharacterStats stats;

    public GameObject attributeAddButton;

    public TextMeshProUGUI title;
    public TextMeshProUGUI amountOfAttribute;
    void Start()
    {
        stats = PlayerManager.Instance.playerStats;
        title.text = attribute.attributeName;
    }
    public void UpdateText()
    {
        if (stats == null)
        {
            stats = PlayerManager.Instance.playerStats;
        }
        switch (attribute.attributeType)
        {
            case TypeOfAttributes.HP:
                amountOfAttribute.text = stats.GetMaxHealth().ToString();
                break;

            case TypeOfAttributes.SHIELD:
                amountOfAttribute.text = stats.GetMaxShield().ToString();
                break;

            case TypeOfAttributes.DAMAGE:
                amountOfAttribute.text = stats.GetDamage().ToString();
                break;

            case TypeOfAttributes.DEFENCE:
                amountOfAttribute.text = stats.GetArmor().ToString();
                break;

            case TypeOfAttributes.CRITCHANCE:
                amountOfAttribute.text = stats.GetCriticalChance().ToString("#.00") + " %"; ;
                break;

            case TypeOfAttributes.EMPTY:
                break;
        }

    }

    public void IsActive(bool active)
    {
        attributeAddButton.SetActive(active);
    }

    public void AddAttribute()
    {
        if (stats == null)
        {
            stats = PlayerManager.Instance.playerStats;
        }
        switch (attribute.attributeType)
        {
            case TypeOfAttributes.HP:
                stats.stats.maxHealth += (int)attribute.addValue;
                break;

            case TypeOfAttributes.SHIELD:
                stats.stats.maxShield += (int)attribute.addValue;
                break;

            case TypeOfAttributes.DAMAGE:
                stats.stats.baseDamage += (int)attribute.addValue;
                break;

            case TypeOfAttributes.DEFENCE:
                stats.stats.baseArmor += (int)attribute.addValue;
                break;

            case TypeOfAttributes.CRITCHANCE:
                stats.stats.criticalChance += attribute.addValue;
                break;

            case TypeOfAttributes.EMPTY:
                break;
        }
    }

    public void OnClickAddAttribute()
    {
        if (stats == null)
        {
            stats = PlayerManager.Instance.playerStats;
        }
        SkillTreeManager.Instance.AddAttribute(this);
    }
}
