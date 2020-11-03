using UnityEngine;

[CreateAssetMenu(fileName = " New UI Attribute", menuName ="Character Stats/UI/Attributes")]
public class AttributeUI_SO : ScriptableObject
{
    [Header("Name of Attribute:")]
    public string attributeName;

    [Header("Values of the Attribute:")]
    public int maxValue;
    public float addValue;
    public TypeOfAttributes attributeType = TypeOfAttributes.HP;

}

public enum TypeOfAttributes 
{
    HP,
    SHIELD,
    DAMAGE,
    DEFENCE,
    CRITCHANCE,
    CRITMULTIPLIER,
    EMPTY
}
