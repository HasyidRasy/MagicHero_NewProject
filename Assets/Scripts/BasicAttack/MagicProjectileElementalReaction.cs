using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectileElementalReaction : MonoBehaviour {
    [SerializeField] private CharacterModel characterModel;
    [SerializeField] public ElementalType element; // Elemen proyektil
    [SerializeField] private float lifeTime;
    [SerializeField] private int damageAmount = 20;
    private float offsetDistance = 0.1f;

    [SerializeField] private GameObject fotiaVfx;
    [SerializeField] private ParticleSystem fotiaOnImpact;
    [SerializeField] private ParticleSystem fotiaOnImpact2;

    [SerializeField] private GameObject neroVfx;
    [SerializeField] private ParticleSystem neroOnImpact;
    [SerializeField] private ParticleSystem neroOnImpact2;

    [SerializeField] private GameObject anemosVfx;
    [SerializeField] private ParticleSystem anemosOnImpact;

    private ParticleSystem onImpactVfx;
    private ParticleSystem onImpactVfx2;

    private static GameObject vfxContainer;
    //untuk menghancurkan projectile dalam kurun waktu tertentu
    private void Awake() {

        characterModel = FindObjectOfType<CharacterModel>();
        Destroy(gameObject, lifeTime);
    }


    // Mengatur elemen proyektil
    public void SetElement(ElementalType newElement) {
        element = newElement;

        // Mengatur visual proyektil berdasarkan elemen
        Renderer renderer = GetComponentInChildren<Renderer>();
        Material material = renderer.material;

        switch (element) {
            case ElementalType.Water:
                neroVfx.SetActive(true);
                NewAudioManager.Instance.PlayAtkSFX("WaterRelease");

                break;
            case ElementalType.Fire:
                fotiaVfx.SetActive(true);
                NewAudioManager.Instance.PlayAtkSFX("FireRelease");
                break;
            case ElementalType.Wind:
                anemosVfx.SetActive(true);
                NewAudioManager.Instance.PlayAtkSFX("WindRelease");
                break;
        }
    }

    // Logika tabrakan proyektil dengan musuh atau objek lain
    // Logika tabrakan proyektil dengan musuh atau objek lain
    private void OnTriggerEnter(Collider other) {
        Vector3 spawnPosition = transform.position + transform.forward * offsetDistance;
        if (!other.CompareTag("Player") && !other.CompareTag("Transparant")) {
            //switch (element) {
            //    case ElementalType.Water:
            //        onImpactVfx = Instantiate(neroOnImpact, spawnPosition, transform.rotation);
            //        onImpactVfx2 = Instantiate(neroOnImpact2, spawnPosition, transform.rotation);

            //        onImpactVfx.Play();
            //        neroOnImpact2.Play();

            //        Destroy(onImpactVfx.gameObject, 3.5f);
            //        Destroy(onImpactVfx2.gameObject, 3.5f);

            //        break;
            //    case ElementalType.Fire:
            //        onImpactVfx = Instantiate(fotiaOnImpact, spawnPosition, transform.rotation);
            //        onImpactVfx2 = Instantiate(fotiaOnImpact2, spawnPosition, transform.rotation);

            //        onImpactVfx.Play();
            //        fotiaOnImpact2.Play();

            //        Destroy(onImpactVfx.gameObject, 3.5f);
            //        Destroy(onImpactVfx2.gameObject, 3.5f);


            //        break;
            //    case ElementalType.Wind:
            //        onImpactVfx = Instantiate(anemosOnImpact, spawnPosition, transform.rotation);
            //        onImpactVfx.Play();

            //        Destroy(onImpactVfx.gameObject, 3.5f);

            //        // No second particle system for wind in the provided code
            //        break;
            //}
            Destroy(gameObject); // Destroy the projectile after collision
            Debug.Log("Collision with other game object");
        }

        if (other.CompareTag("Enemy")) {
            EnemyController enemyController = other.GetComponent<EnemyController>();
            float totalDamage = damageAmount + (characterModel.attack - enemyController.defense) + characterModel.elementalBonus;
            enemyController.TakeDamage(totalDamage);
            enemyController.ApplyElementalStatus(element);

             // Adjust offsetDistance as needed

            //switch (element) {
            //    case ElementalType.Water:
            //        onImpactVfx = Instantiate(neroOnImpact, spawnPosition, transform.rotation);
            //        onImpactVfx2 = Instantiate(neroOnImpact2, spawnPosition, transform.rotation);

            //        onImpactVfx.Play();
            //        neroOnImpact2.Play();

            //        Destroy(onImpactVfx.gameObject, 3.5f);
            //        Destroy(onImpactVfx2.gameObject, 3.5f);

            //        break;
            //    case ElementalType.Fire:
            //        onImpactVfx = Instantiate(fotiaOnImpact, spawnPosition, transform.rotation);
            //        onImpactVfx2 =  Instantiate(fotiaOnImpact2, spawnPosition, transform.rotation);

            //        onImpactVfx.Play();
            //        fotiaOnImpact2.Play();

            //        Destroy(onImpactVfx.gameObject, 3.5f);
            //        Destroy(onImpactVfx2.gameObject, 3.5f);


            //        break;
            //    case ElementalType.Wind:
            //        onImpactVfx = Instantiate(anemosOnImpact, spawnPosition, transform.rotation);
            //        onImpactVfx.Play();

            //        Destroy(onImpactVfx.gameObject, 3.5f);

            //        // No second particle system for wind in the provided code
            //        break;
                    
            //}
            // Destroy the impact VFX after a delay
            Destroy(gameObject); // Destroy the projectile after collision
            Debug.Log("Attacking Enemy");
        }
       
    }

    private void OnDestroy() {

        Vector3 spawnPosition = transform.position + transform.forward * offsetDistance;
        switch (element) {
            case ElementalType.Water:
                onImpactVfx = Instantiate(neroOnImpact, spawnPosition, transform.rotation);
                onImpactVfx2 = Instantiate(neroOnImpact2, spawnPosition, transform.rotation);

                onImpactVfx.Play();
                neroOnImpact2.Play();

                Destroy(onImpactVfx.gameObject, 3.5f);
                Destroy(onImpactVfx2.gameObject, 3.5f);

                break;
            case ElementalType.Fire:
                onImpactVfx = Instantiate(fotiaOnImpact, spawnPosition, transform.rotation);
                onImpactVfx2 = Instantiate(fotiaOnImpact2, spawnPosition, transform.rotation);

                onImpactVfx.Play();
                fotiaOnImpact2.Play();

                Destroy(onImpactVfx.gameObject, 3.5f);
                Destroy(onImpactVfx2.gameObject, 3.5f);


                break;
            case ElementalType.Wind:
                onImpactVfx = Instantiate(anemosOnImpact, spawnPosition, transform.rotation);
                onImpactVfx.Play();

                Destroy(onImpactVfx.gameObject, 3.5f);

                // No second particle system for wind in the provided code
                break;
        }
        if (vfxContainer == null) {
            vfxContainer = new GameObject("vfxContainer");
        }

        onImpactVfx.transform.SetParent(vfxContainer.transform);

        if (onImpactVfx2 != null) {
            onImpactVfx2.transform.SetParent(vfxContainer.transform);
        }
    }


}
