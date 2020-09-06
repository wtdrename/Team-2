using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{

    public Button Play;
    public Button Options;
    public Button MainMenu;

    // Start is called before the first frame update
    void Start()
    {
        Play.onClick.AddListener(ChangeScenePlay);
        MainMenu.onClick.AddListener(ChangeSceneMainMenu);
    }

    void ChangeScenePlay()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }
    void ChangeSceneMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
