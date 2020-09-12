using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneBar : MonoBehaviour
{
    public static LoadSceneBar Instance { private set; get; }

    private Slider loadBar;

    private void Awake()
    {
        loadBar = GetComponent<Slider>();
        Instance = this;
    }

    public void LoadScene(string sceneLoading) //called by GameManager
    {
        StartCoroutine(UpdateProgressBar(sceneLoading));
    }
    
    private IEnumerator UpdateProgressBar(string sceneLoading)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneLoading); //scene loading async operation
        
        while (!operation.isDone)
        {
            loadBar.value = operation.progress;
            yield return null;
        }
    }
}
