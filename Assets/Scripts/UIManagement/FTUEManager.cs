using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class FTUEManager : MonoBehaviour {
    public static FTUEManager Instance;

    [Header("POPUP")]
    public GameObject _sceneElementIntro;
    [Header("Prasasti")]
    public GameObject _scenePrasastiMovement;
    public GameObject _scenePrasastiDash;
    public GameObject _scenePrasastiAtk;
    public GameObject _sceneKillEnemy;
    public GameObject _sceneChangeElement;
    public GameObject _sceneAtkElement;
    public GameObject _scenePortal;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        //Pause();
        //if (_sceneStory != null) {
        //    _sceneStory.SetActive(true);
        //}
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
        //if (nameFtue == "Change")
        //{
        //    _sceneChangeElement.SetActive(true);
        //    if (Input.GetKeyDown(KeyCode.Tab)) {
        //        _sceneChangeElement.SetActive(false);
        //    } 
        //}
        if (nameFtue == "Element") {
            Pause();
            _sceneElementIntro.SetActive(true);
        }
        if (nameFtue == "GoToMainLevel") {
            NewAudioManager.Instance.bgmSource.Stop();
            NewAudioManager.Instance.sfxSource.Stop();
            NewAudioManager.Instance.PlaySFX("Teleport");
            SceneManager.LoadScene("Level1");
        }
    }

    public void PrasastiEnable(string prasastiName) {
        if (prasastiName == "Movement") {
            _scenePrasastiMovement.SetActive(true);
        }
        if (prasastiName == "Dash") {
            _scenePrasastiDash.SetActive(true);
        }
        if (prasastiName == "Atk") {
            _scenePrasastiAtk.SetActive(true);
        }
        if (prasastiName == "Kill") {
            _sceneKillEnemy.SetActive(true);
        }
        if (prasastiName == "Change") {
            _sceneChangeElement.SetActive(true);
        }
        if (prasastiName == "AtkElemental") {
            _sceneAtkElement.SetActive(true);
        }
        if (prasastiName == "Portal") {
            _scenePortal.SetActive(true);
        }
    }

    public void PrasastiDisable(string prasastiName) {
        if (prasastiName == "Movement") {
            _scenePrasastiMovement.SetActive(false);
        }
        if (prasastiName == "Dash") {
            _scenePrasastiDash.SetActive(false);
        }
        if (prasastiName == "Atk") {
            _scenePrasastiAtk.SetActive(false);
        }
        if (prasastiName == "Kill") {
            _sceneKillEnemy.SetActive(false);
        }
        if (prasastiName == "Change") {
            _sceneChangeElement.SetActive(false);
        }
        if (prasastiName == "AtkElemental") {
            _sceneAtkElement.SetActive(false);
        }
        if (prasastiName == "Portal") {
            _scenePortal.SetActive(false);
        }
    }
}
