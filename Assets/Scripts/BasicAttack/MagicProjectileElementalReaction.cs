using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectileElementalReaction : MonoBehaviour
{
    [SerializeField] public ElementalType element; // Elemen proyektil
    [SerializeField] private float lifeTime;
    [SerializeField] private int damageAmount = 20;

    [SerializeField] GameObject fotiaVfx;
    [SerializeField] GameObject neroVfx;
    [SerializeField] GameObject anemosVfx;

    //untuk menghancurkan projectile dalam kurun waktu tertentu
    private void Awake()
    {
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
                neroVfx.SetActive(true);
                NewAudioManager.Instance.PlaySFX("WaterRelease");
                break;
            case ElementalType.Fire:
                fotiaVfx.SetActive(true);
                NewAudioManager.Instance.PlaySFX("FireRelease");
                break;
            case ElementalType.Wind:
                anemosVfx.SetActive(true);
                NewAudioManager.Instance.PlaySFX("WindRelease");
                break;
        }
    }

    // Logika tabrakan proyektil dengan musuh atau objek lain
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyControllerElReactio enemyControllerElReact = other.GetComponent<EnemyControllerElReactio>();
            enemyControllerElReact.TakeDamage(damageAmount);
            enemyControllerElReact.ApplyElementalStatus(element);
            Destroy(gameObject); // Hancurkan proyektil setelah bertabrakan
            Debug.Log("Menyerang Musuh");
        }        
    }
}
