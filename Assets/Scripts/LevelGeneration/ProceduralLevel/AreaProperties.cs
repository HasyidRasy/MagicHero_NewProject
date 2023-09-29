using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaProperties : MonoBehaviour
{
    public string areaTag; // Tag yang digunakan untuk mengidentifikasi perilaku area
    public GameObject[] enemyPrefabs; // Array prefab musuh yang akan di-generate

    public Collider trigger;

    public int id = 1;

    private bool isAreaCompleted; // Status area selesai atau belum

    private void GenerateAreaBehavior()
    {
        // Cek tag area
        if (areaTag == "FireArea")
        {
            // Jika tag adalah "Api", maka spawn musuh tipe api
            SpawnFireEnemies();
        }
        if (areaTag == "WaterArea")
        {
            // Jika tag adalah "Api", maka spawn musuh tipe api
            SpawnWaterEnemies();
        }
        if (areaTag == "WindArea")
        {
            // Jika tag adalah "Air", maka spawn musuh tipe udara
            SpawnWindEnemies();
        }
        if (areaTag == "LastArea")
        {
            // Jika tag adalah "Air", maka spawn musuh tipe udara
            Debug.Log("Anda Telah sampai pada Area Terakhir");
        }
        // Tambahkan kondisi lain berdasarkan tag yang diinginkan
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.CompareTag("Player");
        GenerateAreaBehavior();
    }

    void SpawnFireEnemies()
    {

    }
    void SpawnWaterEnemies()
    {

    }
    void SpawnWindEnemies()
    {

    }

}
