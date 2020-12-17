using Manager;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public void ChangeScenePlay()
    {
        GameManager.Instance.GoToGameScene();
    }

    public void ChangeSceneMainMenu()
    {
        GameManager.Instance.GoToMainMenu();
    }

    public void ToggleInventory()
    {
        InventoryManager.Instance.ToggleInventory();
    }
    public void ToggleEquipment()
    {
        EquipmentManager.Instance.ToggleEquipment();
    }
}
