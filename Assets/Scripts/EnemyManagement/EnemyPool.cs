using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance {get; private set;}
    private UpgradeUIManager upgradeUIManager;
    private EnemySpawnManagerTrigger trigger;

    [Header("Spawn Shenanigans")]
    public int elementTrigger = 0;
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
            elementTrigger++;
            Invoke("TriggerUpgradeUI", 4f);
        }
    }

    private void TriggerUpgradeUI()
    {
        upgradeUIManager.TriggerUI();
    }

    private void TriggerElementalUI()
    {
        if(elementTrigger >= 3)
        {
            upgradeUIManager.TriggerElementalUI();
            elementTrigger = 0;
        }
    }

}