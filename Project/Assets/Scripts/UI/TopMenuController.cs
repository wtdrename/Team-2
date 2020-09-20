using UnityEngine;
using UnityEngine.UI;

public class TopMenuController : MonoBehaviour
{
    public Button inventoryButton;

    public Button skillsButton;


    public void InventoryToggle()
    {
        InventoryManager.instance.ToggleInventory();
    }
    public void SkillPointsToggle()
    {
        SkillTreeManager.instance.ToggleSkillPanel();
    }
}
