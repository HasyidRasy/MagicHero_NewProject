using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class FTUEManager : MonoBehaviour {
    public static FTUEManager Instance;

    public GameObject _sceneStory;
    public GameObject _sceneArea2;
    public GameObject _sceneArea3;
    public GameObject _sceneArea4;
    public GameObject _sceneArea5;
    public GameObject _sceneEnemyIntro;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        Pause();
        if (_sceneStory != null) {
            _sceneStory.SetActive(true);
        }
        NewAudioManager.Instance.bgmSource.Stop();
        NewAudioManager.Instance.PlayBGM("Safezone");
    }

    public void Pause() {
        Time.timeScale = 0;
    }

    public void Continue() {
        Time.timeScale = 1f;
    }

    public void FTUEActive(string nameFtue) {
        if (nameFtue == "TutorialBattle")
        {
            Pause();
            _sceneArea2.SetActive(true);
        }
        if (nameFtue == "TutorialElemental") {
            Pause();
            _sceneArea3.SetActive(true);
        }
        if (nameFtue == "RewardExplain") {
            Pause();
            _sceneArea4.SetActive(true);
        }
        if (nameFtue == "GameObj") {
            Pause();
            _sceneArea5.SetActive(true);
        }
        if (nameFtue == "EnemyIntro") {
            Pause();
            _sceneEnemyIntro.SetActive(true);
        }
        if (nameFtue == "GoToMainLevel") {
            SceneManager.LoadScene("Level1");
        }
    }
}
