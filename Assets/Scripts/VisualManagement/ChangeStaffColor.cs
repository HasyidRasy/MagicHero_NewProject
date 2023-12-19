using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStaffColor : MonoBehaviour
{
    public Material fireMaterial;
    public Material waterMaterial;
    public Material windMaterial;

    private int secondMaterialIndex = 1;
    private MeshRenderer meshRenderer;


    private void OnEnable() {
        CooldownAttackUI.OnFireNext += SetFireNext;
        CooldownAttackUI.OnWaterNext += SetWaterNext;
        CooldownAttackUI.OnWindNext += SetWindNext;
    }

    private void OnDisable() {
        CooldownAttackUI.OnFireNext -= SetFireNext;
        CooldownAttackUI.OnWaterNext -= SetWaterNext;
        CooldownAttackUI.OnWindNext -= SetWindNext;
    }

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        
    }
    void Start() {
        SetSecondMaterial(fireMaterial);
    }

    void SetFireNext() {
        SetSecondMaterial(fireMaterial);
    }
    void SetWaterNext() {
        SetSecondMaterial(waterMaterial);
    }
    void SetWindNext() {
        SetSecondMaterial(windMaterial);
    }
    void SetSecondMaterial(Material newMaterial) {

        if (newMaterial != null) {
            Material[] materials = meshRenderer.materials;
            materials[secondMaterialIndex] = newMaterial;
            meshRenderer.materials = materials;
        }
    }
}
