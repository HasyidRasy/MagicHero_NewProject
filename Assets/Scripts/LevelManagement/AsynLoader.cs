using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsynLoader : MonoBehaviour {
    public static AsynLoader Instance;
    [SerializeField] private string nextLevel;

    [Header("Scene")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject transition;

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    [Header("Transition")]
    [SerializeField] Animator transitionAnim;

    private void OnEnable() {
        LoadLevelOnCollision.OnTeleport += LoadLevel;
    }
    private void OnDisable() {
        LoadLevelOnCollision.OnTeleport -= LoadLevel;
    }
    //private void Awake() {
    //    if (Instance == null) {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    } else {
    //        Destroy(gameObject);
    //    }
    //}

    public void LoadLevel() {
        string levelToLoad = nextLevel;
        StartCoroutine(LoadLevelAsyn(levelToLoad));
    }
    public void LoadLevelBtn(string levelToLoad) {
        //loadingScreen.SetActive(true);
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        //run loading
        StartCoroutine(LoadLevelAsyn(levelToLoad));
    }

    public void TrigerLoadLevel(string levelToLoad) {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevelAsyn(levelToLoad));
    }

    IEnumerator LoadLevelAsyn(string levelToLoad) {  
        //transitionAnim.SetTrigger("TransitionEnd");
        yield return new WaitForSeconds(3f);
        loadingScreen.SetActive(true);
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        
        while (!loadOperation.isDone) {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        //transitionAnim.SetTrigger("TransitionStart");
        //loadingScreen.SetActive(false);
    }
}
