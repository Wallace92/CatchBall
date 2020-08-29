using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Progrss bar in menu scene, loading first scenes
public class LoadingScreenScript : MonoBehaviour
{
    
    public GameObject canvasMenu;
    public GameObject loadingInterface;
    public Image loadingProgressBar;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    void Start()
    {
        ShowLoadingScreen();
       
        scenesToLoad.Add(SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Additive));
        StartCoroutine(LoadingScreen());
    }

    
    IEnumerator LoadingScreen()
    {
        float totalProgress = 0;
        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {
                totalProgress += scenesToLoad[i].progress;
                loadingProgressBar.fillAmount = totalProgress / scenesToLoad.Count;
                yield return null;
            }
        }
        HideLoadingScreen();
    }

    public void ShowLoadingScreen()
    {
        loadingInterface.SetActive(true);
    }
    public void HideLoadingScreen()
    {
        loadingInterface.SetActive(false);
    }
}
