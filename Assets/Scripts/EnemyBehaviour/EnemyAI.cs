using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Transform target; // Transform pemain yang akan dikejar
    private NavMeshAgent navMeshAgent;
    public float speedChase = 5f;

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
            //navMeshAgent.SetDestination(target.position);
            navMeshAgent.destination = target.position;
        }
    }
}
