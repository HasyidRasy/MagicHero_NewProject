using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Transform target; // Transform pemain yang akan dikejar
    private NavMeshAgent navMeshAgent;
    [SerializeField] private float speedChase = 5f;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (target != null)
        {
            navMeshAgent.speed = speedChase;
            // Set target pemain untuk dikejar
            navMeshAgent.destination = target.position;
        }
    }

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

