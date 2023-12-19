using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public List<Transform> spawnPoints;
    public List<Transform> safeZones;

    public float spawnInterval = 2.0f;
    public float minY = 1f;
    public float maxDistanceFromSpawnPoint = 5f;
    public int maxEnemies = 10;

    private void Start()
    {        
        SpawnEnemiesImmediately();
    }

    //Memunculkan musuh secara bertahap
    private IEnumerator SpawnEnemiesContinuously()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    private void SpawnEnemiesImmediately(){
            SpawnEnemy();
    }

    private void SpawnEnemy()
    {

        if (enemyPrefabs.Count == 0 || spawnPoints.Count == 0)
        {
            Debug.LogError("No enemy prefabs or spawn points assigned.");
            return;
        }
        
        bool validSpawnPos = false;

        for(int i = 0; i< maxEnemies;i++){
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            Vector3 spawnPos = Vector3.zero;

            validSpawnPos = false;

            while(!validSpawnPos)
            {

                Vector2 randomOffset = Random.insideUnitCircle * maxDistanceFromSpawnPoint;
                spawnPos = randomSpawnPoint.position + new Vector3(randomOffset.x, minY, randomOffset.y);

                Collider[] colliders = Physics.OverlapSphere(spawnPos, 2f);

                bool insideSafeZone = false;
                bool insideAnotherEnemy = false;

                foreach (Collider collider in colliders)
                {
                    if(safeZones.Contains(collider.transform))
                    {
                        insideSafeZone = true;
                        break;
                    }

                    if(collider.CompareTag("Enemy"))
                    {
                        insideAnotherEnemy = true;
                        break;
                    }
                }

                if(!insideSafeZone && !insideAnotherEnemy)
                {
                    validSpawnPos = true;
                }
            }

            Instantiate(randomEnemyPrefab, spawnPos, Quaternion.identity);   
        }
    }
}