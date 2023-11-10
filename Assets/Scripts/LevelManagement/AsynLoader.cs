using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsynLoader : MonoBehaviour {
    public static AsynLoader Instance;

    [Header("Scene")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    //private void Awake() {
    //    if (Instance == null) {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    } else {
    //        Destroy(gameObject);
    //    }
    //}

    public void LoadLevelBtn(string levelToLoad) { 
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        //run loading
        StartCoroutine(LoadLevelAsyn(levelToLoad));
    }

    public void TrigerLoadLevel(string levelToLoad) {
        if (levelToLoad == "Level1") {
            mainMenu.SetActive(false);
            loadingScreen.SetActive(true);

            StartCoroutine(LoadLevelAsyn("Level1"));
        }
    }

    IEnumerator LoadLevelAsyn(string levelToLoad) {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        
        while (!loadOperation.isDone) {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }
}
