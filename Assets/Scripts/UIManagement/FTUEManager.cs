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
    private int _id;

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
    }

    public void Pause() {
        Time.timeScale = 0;
    }

    public void Continue() {
        Time.timeScale = 1f;
    }

    public void FTUEActive(int id) {
        if (id == 1)
        {
            Pause();
            _sceneArea2.SetActive(true);
        }
        if (id == 2) {
            Pause();
            _sceneArea3.SetActive(true);
        }
        if (id == 3) {
            Pause();
            _sceneArea4.SetActive(true);
        }
        if (id == 4) {
            Pause();
            _sceneArea5.SetActive(true);
        }
    }
}
