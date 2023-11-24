using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VfxTest : MonoBehaviour
{
    //[Header("Attack Effect")]
    //public GameObject attackEffect;
    //private GameObject attackVfxIns;
    //public float attackDespawnDuration;

    [Header("Freeze Effect")]
    public GameObject FreezeEffect;
    public GameObject FreezeOff;
    public float freezeYPosition = 0;
    public float freezeMovementAnimation = 0;

    [Header("Steam Effect")]
    public GameObject SteamEffect;
    public Material steamOverlay;
    public float steamMovementAnimation = 0.50f;

    [Header("Combustion Effect")]
    public GameObject CombustionEffect;
    public Material combustionOverlay;

    private Animator animator;
    private GameObject vfxUsed;

    private List<Material> originalMaterials = new List<Material>();
    private SkinnedMeshRenderer skinnedMeshRenderer;

    private EnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemyController = GetComponent<EnemyController>();  
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        originalMaterials.AddRange(skinnedMeshRenderer.materials);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Freeze(float reactionDuration) {
            Debug.Log("Freezing VFX");
            NewAudioManager.Instance.PlaySFX("Freeze");
            Vector3 spawnPosition = new Vector3(this.transform.position.x, freezeYPosition, this.transform.position.z);
            vfxUsed = Instantiate(FreezeEffect, spawnPosition, this.transform.rotation);
            animator.speed = freezeMovementAnimation;
            vfxUsed.transform.SetParent(this.transform);

        ParticleSystem particleSystem = vfxUsed.GetComponentInChildren<ParticleSystem>();
        if (particleSystem != null) {
            ParticleSystem.MainModule mainModule = particleSystem.main;
            mainModule.startLifetime = new ParticleSystem.MinMaxCurve(reactionDuration);
        }

        Destroy(vfxUsed, reactionDuration);


    }


    public void Unfreeze()
    {
            //var freezeOff = Instantiate(FreezeOff, this.transform.position, this.transform.rotation);
            //Destroy(freezeOff, 5f);
            animator.speed = 1;
    }

    public void Steam(float reactionDuration) {
        Debug.Log("Slowness VFX");
        NewAudioManager.Instance.PlaySFX("Steam");
        vfxUsed = Instantiate(SteamEffect, this.transform.position, this.transform.rotation);
        vfxUsed.transform.SetParent(this.transform);

        animator.speed = steamMovementAnimation;

        Material[] materials = skinnedMeshRenderer.materials;
        Array.Resize(ref materials, materials.Length + 1);
        materials[materials.Length - 1] = steamOverlay;
        skinnedMeshRenderer.materials = materials;

        Destroy(vfxUsed, reactionDuration);
    }

    public void UnSteam() {
        Debug.Log("Steam done");
        skinnedMeshRenderer.materials = originalMaterials.ToArray();
        animator.speed = 1;
    }
    public void Combustion(float reactionDuration) {
        Debug.Log("Combustion VFX");
        NewAudioManager.Instance.PlaySFX("Combustion");
        vfxUsed = Instantiate(CombustionEffect, this.transform.position, this.transform.rotation);
        vfxUsed.transform.SetParent(this.transform);

        // Get the Particle System component in the children of vfxUsed
        ParticleSystem particleSystem = vfxUsed.GetComponentInChildren<ParticleSystem>();

        if (particleSystem != null) {
            SkinnedMeshRenderer skinnedMeshRenderer = GetComponentInChildren <SkinnedMeshRenderer>();
            if (skinnedMeshRenderer != null) {
                ParticleSystem.ShapeModule shapeModule = particleSystem.shape;
                shapeModule.shapeType = ParticleSystemShapeType.SkinnedMeshRenderer;
                shapeModule.skinnedMeshRenderer = skinnedMeshRenderer;
            }
        }
        Material[] materials = skinnedMeshRenderer.materials;
        Array.Resize(ref materials, materials.Length + 1);
        materials[materials.Length - 1] = combustionOverlay;
        skinnedMeshRenderer.materials = materials;

        Destroy(vfxUsed, reactionDuration);
    }

    public void UnCombustion() {
        skinnedMeshRenderer.materials = originalMaterials.ToArray();
    }

    //public void AttackVFX() {
    //    attackVfxIns = Instantiate(attackEffect, this.transform.position, attackEffect.transform.rotation, this.transform);
    //    Destroy(attackVfxIns, attackDespawnDuration);
    //}


    //void Steam()
    //{
    //    if (SteamEffect != null)
    //    {
    //        // Use the position and rotation of the current GameObject's transform.
    //        spawnedObject = Instantiate(SteamEffect, this.transform.position, this.transform.rotation);
    //        Steamed = true;
    //        spawnedObject.transform.SetParent(this.transform);
    //    }
    //}

    //void Unsteam()
    //{
    //    if (spawnedObject != null)
    //    {
    //        Destroy(spawnedObject);
    //        Steamed = false;
    //    }
    //}
}
