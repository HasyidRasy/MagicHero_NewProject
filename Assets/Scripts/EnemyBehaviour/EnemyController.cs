using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private EnemyModel enemyModel;
    private Transform target;
    private NavMeshAgent navMeshAgent;
    private Collider enemyCollider;
    private float speedChase;
    private Animator animator;
    public float minSpeed = 3f;
    public float maxSpeed = 10f;

    public float damageAmount;
    private bool isDeath = false;
    private bool isAttacking;
    private bool canAttack = true;

    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown = 2.0f; // Adjust the cooldown time as needed

    private void Awake()
    {
        enemyModel = GetComponent<EnemyModel>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        speedChase = Random.Range(minSpeed, maxSpeed);
        animator = GetComponentInChildren<Animator>();
        enemyCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        enemyModel.CurrentHealth = enemyModel.HealthPoint;
    }

    private void Update()
    {
        if (!isDeath && target != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, target.position);

            if (distanceToPlayer <= attackRange)
            {
                if (canAttack)
                {
                    StartCoroutine(AttackPlayer());
                }
            }
            else
            {
                if (!isAttacking)
                {
                    ChasePlayer();
                }
            }
        }
    }

    private void ChasePlayer()
    {
        animator.SetBool("isAttacking", false);
        navMeshAgent.speed = speedChase;
        navMeshAgent.destination = target.position;

        if (speedChase < 4)
        {
            animator.SetBool("isWalking", true);
        }
        else if (speedChase > 4)
        {
            animator.SetBool("isHopping", true);
        }
    }

    private IEnumerator AttackPlayer()
    {
        canAttack = false;
        isAttacking = true;
        navMeshAgent.destination = transform.position;
        animator.SetBool("isWalking", false);
        animator.SetBool("isHopping", false);
        animator.SetBool("isAttacking", true);
        // Implement your attack logic here, e.g., dealing damage to the player
        yield return new WaitForSeconds(attackCooldown); // Wait for the attack animation to finish

        animator.SetBool("isAttacking", false);
        isAttacking = false;
        canAttack = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.TakeDamage(damageAmount);
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {       
        enemyModel.CurrentHealth -= damageAmount;
        int randomHurtPattern = Random.Range(0, 3);
        animator.SetInteger("hurtPattern", randomHurtPattern);
        animator.SetTrigger("isHurt");
        if (enemyModel.CurrentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        enemyCollider.enabled = false;
        navMeshAgent.speed = speedChase/2;
        isDeath = true;
        Destroy(this.gameObject, 3f);
        animator.SetBool("Death", true);
    }
}
