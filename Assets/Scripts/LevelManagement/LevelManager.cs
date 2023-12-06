using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public TMP_Text levelText; 
    public int currentLevel = 0;
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
    }

    private void Start()
    {
        UpdateLevelText();
    }

    public void IncreaseLevel()
    {
        currentLevel+=1;
        UpdateLevelText();
        EnemyModel.Instance.SaveEnemyStats();
    }

    public void UpdateLevelText()
    {
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
    }
}
