using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public TMP_Text levelText; 
    public int currentLevel = 0;

    [Header("Pop Up Properties")]
    public float fadeInDuration = 1.0f;
    public float fadeOutDuration = 1.0f;
    public float bannerDuration = 1.0f;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    public void IncreaseLevel()
    {
        currentLevel+=1;
        UpdateLevelText();
        EnemyModel.Instance.SaveEnemyStats();
    }

    public void UpdateLevelText()
    {
        Invoke("FadeIn", 1);

        if (levelText != null)
        {
            if(currentLevel == 0)
            {
                levelText.text = "Level: Tutorial";
            }
            else
            {
                levelText.text = "Level: " + currentLevel.ToString();
            }
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            Destroy(gameObject);
        }
        else
        {
            UpdateLevelText();
        }
    }
    private void FadeIn()
    {
        gameObject.SetActive(true);
        canvasGroup.DOFade(1f, fadeInDuration).OnComplete(() =>
        {
            Invoke("FadeOut", bannerDuration);
        });
    }

    private void FadeOut()
    {
        canvasGroup.DOFade(0f, fadeOutDuration);
    }
}
