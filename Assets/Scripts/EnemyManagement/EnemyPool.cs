using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance {get; private set;}
    private UpgradeUIManager upgradeUIManager;
    private UIAnimationManager aniUIManager;
    private EnemySpawnManagerTrigger trigger;

    [Header("Spawn Shenanigans")]
    public int elementTrigger = 0;
    public int spawnedEnemies = 0;
    public int id;

    // Call this method when an enemy dies
    private void Start()
    {
        upgradeUIManager = FindObjectOfType<UpgradeUIManager>();
        aniUIManager = FindObjectOfType<UIAnimationManager>();
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
            Invoke("TriggerElementalUI", 3f);
            Invoke("TriggerUpgradeUI", 3f);
        }
    }

    private void TriggerUpgradeUI()
    {
        upgradeUIManager.TriggerUI();
        aniUIManager.UpgradeUIAnimation();
    }

    private void TriggerElementalUI()
    {
        
        elementTrigger++;
        if(elementTrigger == 3)
        {           
            elementTrigger = 0;
            upgradeUIManager.TriggerElementalUI();
        }
    }

}