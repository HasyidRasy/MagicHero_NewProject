using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VfxTest : MonoBehaviour
{
    public GameObject FreezeEffect;
    public GameObject FreezeOff;
    public GameObject SteamEffect;
    public GameObject CombustionEffect;
    private Animator animator;
    private GameObject vfxUsed;

    private List<Material> originalMaterials = new List<Material>();
    public Material combustionOverlay;
    public Material steamOverlay;

    private Material originalMaterial;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    //private bool Freezed = false;
    //private bool Steamed = false;

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
            vfxUsed = Instantiate(FreezeEffect, this.transform.position, this.transform.rotation);
            animator.speed = 0;
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
            var freezeOff = Instantiate(FreezeOff, this.transform.position, this.transform.rotation);
            Destroy(freezeOff, 5f);
            animator.speed = 1;
    }

    public void Slowness(float reactionDuration) {
        Debug.Log("Slowness VFX");
        vfxUsed = Instantiate(SteamEffect, this.transform.position, this.transform.rotation);
        vfxUsed.transform.SetParent(this.transform);
        Destroy(vfxUsed, reactionDuration);
    }

    public void UnSteam() {
        animator.speed = 1;
    }
    public void Combustion(float reactionDuration) {
        Debug.Log("Combustion VFX");
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
