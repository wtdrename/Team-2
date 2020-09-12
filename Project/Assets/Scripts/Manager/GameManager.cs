using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { private set; get; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[GameManager] There is more than one GM instance");
            return;
        }
        Instance = this;
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
    }
    

    public void GoToRestartScene()
    {
        SceneChange("GameOver");
    }

    public void GoToGameScene()
    {
       SceneChange("SampleScene");   
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
    
    
}
