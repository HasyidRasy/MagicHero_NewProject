using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementalType
{
    Water,
    Fire,
    Wind
}

public class MagicProjectile : MonoBehaviour
{
    [SerializeField] private CharacterModel characterModel;
    [SerializeField] private ElementalType element; // Elemen proyektil
    [SerializeField] private float lifeTime;
    [SerializeField] private float damageAmount = 10;

    //untuk menghancurkan projectile dalam kurun waktu tertentu
    private void Awake()
    {
        characterModel = FindObjectOfType<CharacterModel>();
        Destroy(gameObject, lifeTime);
    }

    // Mengatur elemen proyektil
    public void SetElement(ElementalType newElement)
    {
        element = newElement;

        // Mengatur visual proyektil berdasarkan elemen
        Renderer renderer = GetComponentInChildren<Renderer>();
        Material material = renderer.material;

        switch (element)
        {
            case ElementalType.Water:
                material.color = Color.blue;
                Debug.Log("tembak proyeqtil "+ element);
                break;
            case ElementalType.Fire:
                material.color = Color.red;
                Debug.Log("tembak proyeqtil " + element);
                break;
            case ElementalType.Wind:
                material.color = Color.green;
                Debug.Log("tembak proyeqtil " + element);
                break;
        }
    }

    // Logika tabrakan proyektil dengan musuh atau objek lain
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemyController = other.GetComponent<EnemyController>();
            enemyController.TakeDamage(damageAmount + characterModel.attack);            
            Destroy(gameObject); // Hancurkan proyektil setelah bertabrakan
            Debug.Log("Menyerang Musuh");
        }        
    }
}
