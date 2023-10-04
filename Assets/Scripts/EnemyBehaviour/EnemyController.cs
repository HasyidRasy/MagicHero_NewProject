using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.CompareTag("Player");
        Attack();
    }

    private void Attack()
    {
        Debug.Log("Player terkena DMG");
    }
}
