using UnityEngine;
using UnityEngine.UI;

public class TopMenuController : MonoBehaviour
{
    public Button inventoryButton;

    public Button skillsButton;


    public void InventoryToggle()
    {
        InventoryManager.Instance.ToggleInventory();
    }
    public void SkillPointsToggle()
    {
        SkillTreeManager.Instance.ToggleSkillPanel();
    }
}
