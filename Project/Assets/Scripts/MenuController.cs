using UnityEngine;
using UnityEngine.SceneManagement;
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
}
