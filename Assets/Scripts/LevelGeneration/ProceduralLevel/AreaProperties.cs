using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaProperties : MonoBehaviour
{
    [SerializeField] private string areaTag; // Tag yang digunakan untuk mengidentifikasi perilaku area
    [SerializeField] private Collider trigger;
    public int id = 1;

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
            // Jika tag adalah "Water", maka spawn musuh tipe Water
            SpawnWaterEnemies();
        }
        if (areaTag == "WindArea")
        {
            // Jika tag adalah "Wind", maka spawn musuh tipe wind
            SpawnWindEnemies();
        }
        if (areaTag == "LastArea")
        {
            // Jika tag adalah "Last Area", maka spawn teleport buat next level
            Debug.Log("Anda Telah sampai pada Area Terakhir");
        }
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
