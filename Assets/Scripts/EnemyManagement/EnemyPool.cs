using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance {get; private set;}
    private UpgradeUIManager upgradeUIManager;
    private EnemySpawnManagerTrigger trigger;
    public int spawnedEnemies = 0;
    public int id;
    // Call this method when an enemy dies
    private void Start()
    {
        upgradeUIManager = FindObjectOfType<UpgradeUIManager>();
        trigger = FindObjectOfType<EnemySpawnManagerTrigger>();
    }

    public void SpawnEnemy(int enemy)
    {
        spawnedEnemies = enemy;
    }
    public void NotifyEnemyDied()
    {
        spawnedEnemies--;
        if (spawnedEnemies <= 0)
        {
            Invoke("TriggerUpgradeUI", 3f);
        }
    }

    private void TriggerUpgradeUI()
    {
        upgradeUIManager.TriggerUI();
    }

}