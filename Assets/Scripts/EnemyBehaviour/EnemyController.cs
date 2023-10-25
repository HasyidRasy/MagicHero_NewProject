using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private CharacterModel characterModel;
    private EnemyPool enemyPool;
    private EnemySpawnManagerTrigger enemySpawnManagerTrigger;
    private EnemyModel enemyModel;

    private Transform target; // Transform pemain yang akan dikejar
    private NavMeshAgent navMeshAgent;    
    public float defense;
    private Collider enemyCollider;
    private float speedChase;
    private Animator animator;
    public float minSpeed = 3f;
    public float maxSpeed = 10f;
    private float spawnDuration = 2.0f;


    public float damageAmount;
    private bool isDeath = false;
    private bool isAttacking;
    private bool canAttack = true;
    private bool freezing;
    private bool isSpawning;

    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown = 2.0f; // Adjust the cooldown time as needed

    private void Awake()
    {
        characterModel = FindObjectOfType<CharacterModel>();
        enemyPool = FindObjectOfType<EnemyPool>();
        enemySpawnManagerTrigger = FindObjectOfType<EnemySpawnManagerTrigger>();
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
        NewAudioManager.Instance.PlaySFX("EnemySpawn");
    }

    private void Update()
    {
        Invoke("EnemyBehavior", spawnDuration);
    }

    private void EnemyBehavior() {
        if (!isDeath && target != null && !isSpawning) {
            float distanceToPlayer = Vector3.Distance(transform.position, target.position);

            if (distanceToPlayer <= attackRange) {
                if (canAttack) StartCoroutine(AttackPlayer());
            } else {
                if (!isAttacking && !freezing) ChasePlayer();
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
        NewAudioManager.Instance.PlaySFX("EnemyAtk");
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

    public void TakeDamage(float damageAmount)
    {       
        float damageTaken = (damageAmount - defense) + characterModel.elementalBonus;
        enemyModel.CurrentHealth -= damageTaken; // Reduce current health by the damage amount
        int randomHurtPattern = Random.Range(0, 3);
        animator.SetInteger("hurtPattern", randomHurtPattern);
        animator.SetTrigger("isHurt");
        navMeshAgent.speed = 0;
        NewAudioManager.Instance.PlaySFX("EnemyHurt");
        if (enemyModel.CurrentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(this.gameObject, 3f);
        enemyPool.NotifyEnemyDied();
        enemyCollider.enabled = false;
        //navMeshAgent.speed = speedChase/2;
        navMeshAgent.speed = 0;
        isDeath = true;
        animator.SetBool("Death", true);
        NewAudioManager.Instance.PlaySFX("EnemyDeath");
    }

    public void FreezeChara(bool _freeze)
    {
        if (!freezing == _freeze)
        {
            navMeshAgent.speed = 0;
            Debug.Log("freezed");
            freezing = _freeze;
        }
        else if(freezing == _freeze) {           
            navMeshAgent.speed = speedChase;
            Debug.Log("unfreezed");
        }
    }


}
