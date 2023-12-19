using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralLevelGenerator : MonoBehaviour
{
    [Header("Prefabs Assign")]
    [SerializeField] private GameObject[] playerSpawnAreas;      // Array prefab area spawn pemain
    [SerializeField] private GameObject[] challengeAreaPrefabs;  // Array prefab area tantangan
    [SerializeField] private GameObject[] lastAreaPrefabs;       // Array prefab last area
    [SerializeField] private GameObject corridorPrefab;          // Prefab lorong

    [Header("Stage Properties")]
    [SerializeField] private int minChallengeAreas = 3;          // Jumlah minimum area tantangan
    [SerializeField] private int maxChallengeAreas = 5;          // Jumlah maksimum area tantangan
    [SerializeField] private float roomSpacing = 40f;            // Jarak antara area

    [Header("Current Area Id")]
    [SerializeField] private int currentAreaID = 1;

    private void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        int spawnAreaIndex = Random.Range(0, playerSpawnAreas.Length);
        GameObject playerSpawnArea = playerSpawnAreas[spawnAreaIndex];

        Instantiate(playerSpawnArea, Vector3.zero, Quaternion.identity);

        int numChallengeAreas = Random.Range(minChallengeAreas, maxChallengeAreas + 1);

        Vector3 spawnPosition = Vector3.forward * roomSpacing;

        GameObject previousArea = playerSpawnArea;
        int lastAreaIndex = spawnAreaIndex; 

        for (int i = 0; i < numChallengeAreas; i++)
        {
            GameObject challengeAreaPrefab = challengeAreaPrefabs[Random.Range(0, challengeAreaPrefabs.Length)];

            GameObject challengeArea = Instantiate(challengeAreaPrefab, spawnPosition, Quaternion.identity);

            challengeArea.GetComponent<AreaProperties>().id = currentAreaID;
            challengeArea.GetComponentInChildren<TriggerArea>().id = currentAreaID;
            challengeArea.GetComponentInChildren<DoorController>().id = currentAreaID;
            challengeArea.GetComponentInChildren<EnemySpawnManagerTrigger>().id = currentAreaID;
            currentAreaID++;

            // Membuat lorong antara area sebelumnya dan area saat ini
            Vector3 corridorPosition = (spawnPosition + Vector3.back * roomSpacing/2);
            GameObject corridor = Instantiate(corridorPrefab, corridorPosition, Quaternion.identity);

            spawnPosition += Vector3.forward * (roomSpacing);

            previousArea = challengeArea;

            if (i == numChallengeAreas-1)
            {
                GameObject _lastAreaPrefab = lastAreaPrefabs[lastAreaIndex];
                GameObject _lastArea = Instantiate(_lastAreaPrefab, spawnPosition, Quaternion.identity);

                Vector3 lastCorridorPosition = (spawnPosition + Vector3.back * roomSpacing / 2);
                GameObject lastCorridor = Instantiate(corridorPrefab, lastCorridorPosition, Quaternion.identity);
            }
        }
    }
}
