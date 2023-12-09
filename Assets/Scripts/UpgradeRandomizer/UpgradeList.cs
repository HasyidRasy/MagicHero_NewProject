using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeList : MonoBehaviour
{
    public static UpgradeList Instance {get; private set;}

    public List<UpgradeData> chosenUpgrades = new List<UpgradeData>();

    private string tutorialSceneName = "Tutorial";

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

    public void RemoveTutorialList()
    {
        chosenUpgrades.Clear(); // This line clears all elements from the list
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != tutorialSceneName && SceneManager.GetActiveScene().name == tutorialSceneName)
        {
            // The current scene is not the tutorial scene, but the previous one was
            RemoveTutorialList();
        }
    }
}
