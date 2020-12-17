using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
    
        public CharacterStats_SO stats;

        public static GameManager Instance { private set; get; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("[GameManager] There is more than one GM Instance");
                return;
            }
            Instance = this;
        }

        #endregion

        public enum GameState { GamePause, GameWon, GameLoose};

        public event EventHandler OnPlayerDeathEvent;

        // subscribe to this whenever you want to do something when level starts
        public UnityAction OnLevelStarted;
        // subscribe to this whenever you want to do something when level ends
        public UnityAction OnLevelEnded;
        // subscribe to this whenever you want to do something when the player wins
        public UnityAction OnLevelWon;

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

        private void Update()
        {
            #region Scene Sound

            Scene currentScene = SceneManager.GetActiveScene();
            PlaySceneSound(currentScene.name);

            #endregion
        }

        private void PlaySceneSound(string sceneName)
        {
            var sound = AudioManager.Instance.IsPlayingSound(sceneName + "Sound_1");
            if (!sound)
            {
                sound = AudioManager.Instance.IsPlayingSound(sceneName + "Sound_2");
                if (!sound)
                {
                    var random = UnityEngine.Random.Range(0, 1);
                    if (random < 0.5f)
                    {
                        AudioManager.Instance.Play(sceneName + "Sound_1");
                    }
                    else
                    {
                        AudioManager.Instance.Play(sceneName + "Sound_2");
                    }
                }
            }
        }

        public void GoToRestartScene()
        {
            SceneChange("GameOver");
            // player loses everything he earned
            Destroy(InventoryManager.Instance.inventoryCache);
        }

        public void GoToGameScene()
        {
            SceneChange("Desert");
            OnLevelStarted?.Invoke();
        }

        public void GoToMainMenu()
        {
            SceneChange("MainMenu");
            OnLevelEnded?.Invoke();
        }

        private void SceneChange(string SceneName)
        {
            //button animation and other things
        
            if(!_isChangingToLoadScene)
            {
                StartCoroutine(ChangeSceneOnLoad(SceneName));
            }
        }

        private bool _isChangingToLoadScene;
    
        IEnumerator ChangeSceneOnLoad(string sceneName)
        {
            Scene sceneToUnload = SceneManager.GetActiveScene();
            if (SceneManager.GetSceneByName("Scene") == sceneToUnload)
                yield break;

            if(sceneName == "GameOver")
            {
                SceneManager.LoadSceneAsync(sceneName);
                yield break;
            }
            _isChangingToLoadScene = true;

            SceneManager.LoadScene("LoadScene");

            _isChangingToLoadScene = false;

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
}
