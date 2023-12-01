using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStaffColor : MonoBehaviour
{
    public Material fireMaterial;
    public Material waterMaterial;
    public Material windMaterial;

    // The index of the second material in the materials array
    private int secondMaterialIndex = 1;

    // Assuming your object has a MeshRenderer component
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
    void Start() {
        // Assuming the MeshRenderer is attached to the same GameObject
        meshRenderer = GetComponent<MeshRenderer>();
        // Initialize the material for the second slot
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
        // Get the current materials array
        Material[] materials = meshRenderer.materials;

        // Update the material in the second slot
        materials[secondMaterialIndex] = newMaterial;

        // Apply the updated materials array to the MeshRenderer
        meshRenderer.materials = materials;
    }
}
