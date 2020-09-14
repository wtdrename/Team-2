using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public void ChangeScenePlay()
    {
        GameManager.instance.GoToGameScene();
    }
    public void ChangeSceneMainMenu()
    {
        GameManager.instance.GoToMainMenu();
    }
    
}
