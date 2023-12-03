using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
}
