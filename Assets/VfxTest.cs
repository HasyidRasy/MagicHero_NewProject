using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VfxTest : MonoBehaviour
{
    public GameObject FreezeEffect;
    public GameObject FreezeOff;
    public GameObject SteamEffect;
    private Animator animator;
    private GameObject vfxUsed;
    //private bool Freezed = false;
    //private bool Steamed = false;

    private EnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemyController = GetComponent<EnemyController>();
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
