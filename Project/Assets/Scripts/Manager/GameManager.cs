using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance { private set; get; }


    public event EventHandler OnPlayerDeathEvent;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("[GameManager] There is more than one GM instance");
            return;
        }
        instance = this;
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        /*  Start on restart
         *  GoToRestartScene();
         */

        /* Start ingame
         *  GotoGameScene();
         */
        OnPlayerDeathEvent += OnPlayerDeath;
    }


    public void GoToRestartScene()
    {
        SceneChange("GameOver");
    }

    public void GoToGameScene()
    {
       SceneChange("Level1");   
    }

    public void GoToMainMenu()
    {
        SceneChange("MainMenu");
    }

    private void SceneChange(string SceneName)
    {
        //button animation and other things
        
        if(!isChangingToLoadScene)
            StartCoroutine(ChangeSceneOnLoad(SceneName));
    }

    private bool isChangingToLoadScene;
    
    IEnumerator ChangeSceneOnLoad(string sceneName)
    {
        Scene sceneToUnload = SceneManager.GetActiveScene();
        if (SceneManager.GetSceneByName("Scene") == sceneToUnload)
            yield break;
        
        isChangingToLoadScene = true;
        
        SceneManager.LoadScene("LoadScene");
        
        isChangingToLoadScene = false;
        
        yield return null; //wait one frame so the singleton can be loaded in the StatusBar script
        
        LoadSceneBar.Instance.LoadScene(sceneName);
    }

    private void OnPlayerDeath(object sender, EventArgs e)
    {
        SceneChange("GameOver");
    }
    public void DeathEventCall()
    {
        OnPlayerDeathEvent?.Invoke(this, EventArgs.Empty);
    }

}
