using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VfxTest : MonoBehaviour
{
    [Header("Spawn Effect")]
    public GameObject spawnEffect;
    private ParticleSystem spawnEffectParticle;
    private GameObject spawnEffectContainer;

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

    void Start() {
        animator = GetComponentInChildren<Animator>();
        enemyController = GetComponent<EnemyController>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        originalMaterials.AddRange(skinnedMeshRenderer.materials);

        spawnEffectContainer = spawnEffect;
        SpawnVfxPlay();
    }

    private void SpawnVfxPlay() {
        if (spawnEffectContainer != null) {
            Vector3 spawnPosition = transform.position;
            spawnPosition.y = 0;

            GameObject instance = Instantiate(spawnEffectContainer, spawnPosition, Quaternion.identity);
            ParticleSystem instanceParticleSystem = instance.GetComponent<ParticleSystem>();
            instanceParticleSystem?.Play();

            Destroy(instance, instanceParticleSystem.main.duration);
        }
    }

    public void Freeze(float reactionDuration) {
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
        animator.speed = 1;
    }

    public void Steam(float reactionDuration) {
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
        skinnedMeshRenderer.materials = originalMaterials.ToArray();
        animator.speed = 1;
    }
    public void Combustion(float reactionDuration) {
        NewAudioManager.Instance.PlaySFX("Combustion");
        vfxUsed = Instantiate(CombustionEffect, this.transform.position, this.transform.rotation);
        vfxUsed.transform.SetParent(this.transform);

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
}
