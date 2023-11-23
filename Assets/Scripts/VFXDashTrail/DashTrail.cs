using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTrail : MonoBehaviour
{
    public float activeTime;
    public GameObject lightningTrail;

    [Header("Speedline")]
    public GameObject speedlineOverlay;
    public Transform overlaySpawn;

    [Header("Mesh Related")]
    public float meshRefreshRate;
    public float meshDestroyDelay;
    public Transform posToSpawn;

    [Header("Shader Related")]
    public Material mat;
    public string shaderVarRef;
    public float shaderVarRate, shaderVarRefreshRate;

    bool isTrailActive;
    SkinnedMeshRenderer[] skinnedMeshRenderers;
    GameObject FX;

    private NewPlayerController1 playerController1;

    private void Awake() {
        playerController1 = GetComponent<NewPlayerController1>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isTrailActive && playerController1._dashCooldownSlider.value == 1f)
        {
            isTrailActive = true;
            StartCoroutine(ActivateTrail(activeTime));

            FX = Instantiate(speedlineOverlay, overlaySpawn.transform);
            FX = Instantiate(lightningTrail, posToSpawn.transform);
        }
    }

    IEnumerator ActivateTrail (float timeActive)
    {
        while (timeActive > 0)
        {
            timeActive -= meshRefreshRate;

            if (skinnedMeshRenderers == null)
            {
                skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            }

            for (int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                GameObject gObj = new GameObject();
                gObj.transform.SetPositionAndRotation(posToSpawn.position, posToSpawn.rotation);

                MeshRenderer mr = gObj.AddComponent<MeshRenderer>();
                MeshFilter mf = gObj.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh();
                skinnedMeshRenderers[i].BakeMesh(mesh);

                mf.mesh = mesh;
                mr.material = mat;

                StartCoroutine(AnimateMaterialFloat(mr.material, 0, shaderVarRate, shaderVarRefreshRate));

                Destroy(gObj, meshDestroyDelay);
            }

            yield return new WaitForSeconds(meshRefreshRate);
        }

        isTrailActive = false;
    }

    IEnumerator AnimateMaterialFloat(Material mat, float goal, float rate, float refreshRate)
    {
        float valueToAnimate = mat.GetFloat(shaderVarRef);

        while (valueToAnimate > goal)
        {
            valueToAnimate -= rate;
            mat.SetFloat(shaderVarRef, valueToAnimate);
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
