using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralLevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] playerSpawnAreas;      // Array prefab area spawn pemain
    [SerializeField] private GameObject[] challengeAreaPrefabs;  // Array prefab area tantangan
    [SerializeField] private GameObject lastAreaPrefab;

    //public GameObject[] obstaclePrefabs; // Array prefab rintangan
    //public int maxObstaclesPerArea = 5; // Jumlah maksimum rintangan per area
    //public Vector3 areaSize = new Vector3(20f, 0f, 20f); // Ganti dengan ukuran yang sesuai

    [SerializeField] private int minChallengeAreas = 3;          // Jumlah minimum area tantangan
    [SerializeField] private int maxChallengeAreas = 5;          // Jumlah maksimum area tantangan
    [SerializeField] private float roomSpacing = 40f;            // Jarak antara area

    [SerializeField] private int currentAreaID = 1;

    private void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        // Memilih secara acak prefab area spawn pemain
        GameObject playerSpawnArea = playerSpawnAreas[Random.Range(0, playerSpawnAreas.Length)];

        // Instantiate area spawn pemain di posisi awal
        Instantiate(playerSpawnArea, Vector3.zero, Quaternion.identity);

        int numChallengeAreas = Random.Range(minChallengeAreas, maxChallengeAreas + 1);

        Vector3 spawnPosition = Vector3.forward * roomSpacing;

        GameObject previousArea = playerSpawnArea;

        for (int i = 0; i < numChallengeAreas; i++)
        {
            // Memilih secara acak prefab area tantangan
            GameObject challengeAreaPrefab = challengeAreaPrefabs[Random.Range(0, challengeAreaPrefabs.Length)];

            // Instantiate prefab area tantangan di posisi spawn
            GameObject challengeArea = Instantiate(challengeAreaPrefab, spawnPosition, Quaternion.identity);

            // Mengatur ID area pada komponen AreaProperties
            challengeArea.GetComponent<AreaProperties>().id = currentAreaID;
            challengeArea.GetComponentInChildren<TriggerArea>().id = currentAreaID;
            challengeArea.GetComponentInChildren<DoorController>().id = currentAreaID;

            // Update ID area saat ini
            currentAreaID++;

            // Update posisi spawn untuk area berikutnya
            spawnPosition += Vector3.forward * (roomSpacing);

            // Panggil GenerateObstacles untuk menambahkan rintangan
            //GenerateObstacles(challengeArea);

            // Mengatur area saat ini sebagai area sebelumnya untuk lorong berikutnya
            previousArea = challengeArea;

            if (i == numChallengeAreas-1)
            {
                GameObject _lastArea = Instantiate(lastAreaPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    /*
    void GenerateObstacles(GameObject area)
    {
        // Jumlah rintangan yang akan dihasilkan
        int numObstacles = Random.Range(0, maxObstaclesPerArea + 1);

        for (int i = 0; i < numObstacles; i++)
        {
            // Pilih secara acak prefab rintangan
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

            // Buat instance prefab rintangan di posisi acak di dalam area
            Vector3 randomPosition = new Vector3(
                Random.Range(-areaSize.x / 0.5f, areaSize.x / 0.5f),
                0.5f, // Tinggi rintangan dari tanah
                Random.Range(-areaSize.z / 0.5f, areaSize.z / 0.5f)
            );

            Instantiate(obstaclePrefab, area.transform.position + randomPosition, Quaternion.identity);
        }
    }
    */
}
