using System;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int _areaCleared;
    public int _enemyKilled;
    public float _playTime;
    public int _buffCollected;

    [Header("UI Text")]
    public TMP_Text clearAreaTxt;
    public TMP_Text killEnemyTxt;
    public TMP_Text collectBuffTxt;
    public TMP_Text playTimeTxt;

    public bool isGameRunning = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (isGameRunning)
        {
            StartPlaytime();
        }
    }

    public void StartPlaytime()
    {
        _playTime += Time.deltaTime;
    }

    public void EnemyKilled()
    {
        _enemyKilled++;
    }
    public void AreaCleared()
    {
        _areaCleared++;
    }
    public void BuffCollected()
    {
        _buffCollected++;
    }

    public void ResetScore()
    {
        _areaCleared = 0;
        _enemyKilled = 0;
        _playTime = 0;
        _buffCollected = 0;
    }
    // Save player stats to PlayerPrefs
    public void SavePlayerScore()
    {
        PlayerPrefs.SetInt("AreaCleared", _areaCleared);
        PlayerPrefs.SetInt("EnemyKilled", _enemyKilled);
        PlayerPrefs.SetInt("BuffCollected", _buffCollected);
        PlayerPrefs.SetFloat("PlayTime", _playTime);

        // Save PlayerPrefs
        PlayerPrefs.Save();
    }

    // Load player stats from PlayerPrefs
    public void LoadPlayerScore()
    {
        _areaCleared = PlayerPrefs.GetInt("AreaCleared");
        _enemyKilled = PlayerPrefs.GetInt("EnemyKilled");
        _buffCollected = PlayerPrefs.GetInt("BuffCollected");
        _playTime = PlayerPrefs.GetFloat("PlayTime");
    }
    public void DisplayGameOverStats()
    {
        playTimeTxt.text = $"Playtime: {FormatPlaytime(ScoreManager.Instance._playTime)}";
        clearAreaTxt.text = $"Stages Complete: {ScoreManager.Instance._areaCleared}";
        killEnemyTxt.text = $"Enemy Killed: {ScoreManager.Instance._enemyKilled}";
        collectBuffTxt.text = $"Buff Collected: {ScoreManager.Instance._buffCollected}";
    }

    private string FormatPlaytime(float seconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        return $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
    }
    public void StartGame()
    {
        isGameRunning = true;
        ResetScore();
    }
    public void EndGame()
    {
        isGameRunning = false;
    }
}
