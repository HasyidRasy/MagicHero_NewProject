using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;       // Prefab musuh
    public Transform[] spawnPoints;      // Array titik spawn musuh
    public float timeBetweenWaves = 10f; // Waktu antara wave
    public float spawnRadius = 10f;

    public int totalWaves = 5;           // Jumlah total wave
    public int enemiesPerWave = 6;       // Jumlah musuh per wave

    private int currentWave = 0;
    private bool isSpawningWave = false;

    public int id;

    private void Start()
    {
        StartNextWave();
    }

    private void Update()
    {
        if (isSpawningWave)
            return;

        if (IsWaveComplete())
        {
            // Wave selesai, tunggu sejenak, lalu mulai wave berikutnya
            StartCoroutine(StartNextWaveWithDelay());
        }
    }

    void StartNextWave()
    {
        currentWave++;
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        isSpawningWave = true;

        for (int i = 0; i < enemiesPerWave; i++)
        {
            // Pilih secara acak satu dari titik spawn
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Acak posisi dalam radius tertentu dari titik spawn
            Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
            randomOffset.y = 0f; // Pastikan musuh tetap di tingkat yang sama

            // Tentukan posisi akhir musuh
            Vector3 spawnPosition = randomSpawnPoint.position + randomOffset;

            // Instantiate musuh di posisi akhir
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }

        yield return new WaitForSeconds(timeBetweenWaves);

        isSpawningWave = false;

        if (currentWave >= totalWaves)
        {
            // Semua wave selesai
            Debug.Log("Permainan Selesai!");
            enabled = false; // Menonaktifkan skrip
        }
    }

    bool IsWaveComplete()
    {
        // Cek apakah masih ada musuh yang hidup
        // Anda dapat mengganti kondisi ini sesuai dengan cara Anda melacak musuh
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length == 0;
    }

    IEnumerator StartNextWaveWithDelay()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        if (currentWave < totalWaves)
        {
            StartNextWave();
        }
    }
}
