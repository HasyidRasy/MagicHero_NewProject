using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class FTUEManager : MonoBehaviour {
    public static FTUEManager Instance;

    [Header("POPUP")]
    public GameObject _sceneInfo;


    [Header("Prasasti")]
    [SerializeField] private float _fadeInDuration = 0.25f;
    [SerializeField] private float _fadeOutDuration = 0.25f;

    public GameObject _scenePrasastiMovement;
    public GameObject _scenePrasastiDash;
    public GameObject _scenePrasastiAtk;
    public GameObject _sceneKillEnemy;
    public GameObject _sceneChangeElement;
    public GameObject _sceneAtkElement;
    public GameObject _scenePortal;

    private GameObject _currentActive;
    private CanvasGroup _currentActiveCanvasGroup;

    private ElementSwitchSystem elementSwitchSystem;
    private NewPlayerController1 newPlayerController1;


    private void Awake() {
        if (Instance == null) {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        elementSwitchSystem = FindObjectOfType<ElementSwitchSystem>();
        newPlayerController1 = FindObjectOfType<NewPlayerController1>();

    }


    private void OnDestroy()
    {
        elementSwitchSystem.SetDefaultElementStatus();
        newPlayerController1.SetDefaultElementSlots();
        CharacterModel.Instance.ResetStats();
        CharacterModel.Instance.SavePlayerStats();
    }

    private void Start() {
        //Pause();
        //if (_sceneStory != null) {
        //    _sceneStory.SetActive(true);
        //}
        NewAudioManager.Instance.bgmSource.Stop();
        NewAudioManager.Instance.PlayBGM("Safezone");
        CharacterModel.Instance.ResetStats();
        ScoreManager.Instance.StartGame();
        elementSwitchSystem.SetDefaultElementStatus();
        newPlayerController1.SetDefaultElementSlots();
    }

    public void Pause() {
        Time.timeScale = 0;
    }

    public void Continue() {
        Time.timeScale = 1f;
    }

    public void FTUEActive(string nameFtue) {
        if (nameFtue == "Info") {
            Pause();
            _sceneInfo.SetActive(true);
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
            _currentActive = _scenePrasastiMovement;
             FadeIn();
        }
        if (prasastiName == "Dash") {
            _currentActive = _scenePrasastiDash;
            FadeIn();
        }
        if (prasastiName == "Atk") {
            _currentActive = _scenePrasastiAtk;
            FadeIn();
        }
        if (prasastiName == "Kill") {
            _currentActive = _sceneKillEnemy;
            FadeIn();
        }
        if (prasastiName == "Change") {
            _currentActive = _sceneChangeElement;
           FadeIn();
        }
        if (prasastiName == "AtkElemental") {
            _currentActive = _sceneAtkElement;
            FadeIn();
        }
        if (prasastiName == "Portal") {
            _currentActive = _scenePortal;
            FadeIn();
        }

    }

    private void FadeIn() {
        _currentActive.SetActive(true);
        _currentActiveCanvasGroup = _currentActive.GetComponent<CanvasGroup>();
        _currentActiveCanvasGroup.DOFade(1f, _fadeInDuration)
                                 .From(0f);
    }

    public void PrasastiDisable(string prasastiName) {
        _currentActiveCanvasGroup.DOFade(0f, _fadeOutDuration)
                                       .OnComplete(() => { _currentActive.SetActive(false); });
    }
}
