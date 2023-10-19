using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance {get; private set;}
    public UpgradeUIManager upgradeUIManager;
    public UpgradeRandomizer upgradeRandomizer;
    public int spawnedEnemies = 0;

     // Call this method when an enemy dies
    public void NotifyEnemyDied()
    {
        spawnedEnemies--;
        if (spawnedEnemies <= 0)
        {
            upgradeUIManager.TriggerUI();
        }
    }
}