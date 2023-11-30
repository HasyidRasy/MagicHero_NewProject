using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int _areaCleared;
    public int _enemyKilled;
    public float _playTime;
    public int _buffCollected;

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

    public void StartPlayTime()
    {
        _playTime = Time.time;
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
}
