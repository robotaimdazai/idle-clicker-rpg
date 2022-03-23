using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using GIKIT;

namespace GIKIT
{
    public class GameManager : MonoBehaviour
    {
        static GameManager instance = null;
        public static GameManager Instance { get { return instance; } }

        public bool IsPaused { get; private set; }


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        public void LoadLevel(int index)
        {
            StartCoroutine(LoadLevelCoroutine(index));
        }

        private IEnumerator LoadLevelCoroutine(int index)
        {
            float progressValue = 0f;
            AsyncOperation async = SceneManager.LoadSceneAsync(index);

            // this part deals with loading screen and showing loading progress into default slider
            UIScreen loadingScreen = UIManager.Instance.GetScreen(UIScreenType.LoadingScreen);
            LoadingProgress loadingProgress = null;
            if (loadingScreen)
            {
                UIManager.Instance.SwitchScreen(loadingScreen);
                loadingProgress = loadingScreen.GetComponent<LoadingProgress>();
            }

            while (!async.isDone)
            {
                progressValue = async.progress;

                if (progressValue>=0.9f)
                {
                    progressValue = 1;
                }

                if (loadingProgress)
                {
                    //loadingProgress.SetValue(progressValue);
                }

                yield return null;
            }
            // this will introduce artificial delay if required on loading sceen

            float loadingDelay = 2f;
            if (loadingProgress)
            {
                loadingProgress.SetSmoothProgress(loadingDelay);
            }
            
            yield return new WaitForSeconds(loadingDelay);
        
           
            UIManager.Instance.SwitchScreen(UIScreenType.GameScreen);
            

        }

        public void PauseGame()
        {
            IsPaused = true;
        }

        public void UnPauseGame()
        {
            IsPaused = false;
        }

        public void TogglePause()
        {
            IsPaused = !IsPaused;
        }

    }
}

