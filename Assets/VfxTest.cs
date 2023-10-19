using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VfxTest : MonoBehaviour
{
    public GameObject FreezeEffect;
    public GameObject FreezeOff;
    public GameObject SteamEffect;
    public float despawnDelay = 3.0f; // Adjust this to the desired despawn time.
    private Animator animator;
    private GameObject spawnedObject;
    private bool Freezed = false;
    private bool Steamed = false;

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
        if (Input.GetKeyDown(KeyCode.U) && !Freezed)
        {
            Freeze();
        }

        // Check if the object is spawned and the despawn time has passed.
        if (Input.GetKeyDown(KeyCode.I) && Freezed)
        {
            Unfreeze();
        }

        if (Input.GetKeyDown(KeyCode.O) && !Steamed)
        {
            Steam();
            Debug.Log("Steamed");
        }

        // Check if the object is spawned and the despawn time has passed.
        if (Input.GetKeyDown(KeyCode.P) && Steamed)
        {
            Unsteam();
            Debug.Log("Unsteamed");
        }
    }

    void Freeze()
    {
        if (FreezeEffect != null)
        {
            // Use the position and rotation of the current GameObject's transform.
            spawnedObject = Instantiate(FreezeEffect, this.transform.position, this.transform.rotation);
            Freezed = true;
            animator.speed = 0;
            enemyController.FreezeChara(Freezed);
            spawnedObject.transform.SetParent(this.transform);
        }
    }

    void Unfreeze()
    {
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
            var freezeOff = Instantiate(FreezeOff, this.transform.position, this.transform.rotation);
            Destroy(freezeOff, 5f);
            Freezed = false;
            animator.speed = 1;
            enemyController.FreezeChara(Freezed);
        }
    }

    void Steam()
    {
        if (SteamEffect != null)
        {
            // Use the position and rotation of the current GameObject's transform.
            spawnedObject = Instantiate(SteamEffect, this.transform.position, this.transform.rotation);
            Steamed = true;
            spawnedObject.transform.SetParent(this.transform);
        }
    }

    void Unsteam()
    {
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
            Steamed = false;
        }
    }
}
