using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private EnemyModel enemyModel;
    private Transform target; // Transform pemain yang akan dikejar
    private NavMeshAgent navMeshAgent;    
    private Renderer enemyRenderer;
    private Material originalMaterial;
    private Color originalColor;
    private float speedChase;

    public float minSpeed = 3f;
    public float maxSpeed = 10f;

    public float damageAmount;

    private void Awake()
    {
        enemyModel = GetComponent<EnemyModel>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyRenderer = GetComponentInChildren<Renderer>();
        originalMaterial = enemyRenderer.material;
        originalColor = originalMaterial.color;

        speedChase = Random.Range(minSpeed, maxSpeed);
    }

    private void Start()
    {
        enemyModel.CurrentHealth = enemyModel.HealthPoint;
    }

    private void Update()
    {
        if (target != null)
        {
            navMeshAgent.speed = speedChase;
            // Set target pemain untuk dikejar
            navMeshAgent.destination = target.position;
        }

        UpdateEnemyColor();
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

    private void Attack()
    {
        Debug.Log("Player terkena DMG");
    }

    public void TakeDamage(int damageAmount)
    {
        enemyModel.CurrentHealth -= damageAmount; // Reduce current health by the damage amount

        if (enemyModel.CurrentHealth <= 0)
        {
            Death(); // If health drops to or below zero, call a method to handle enemy death
        }

        // Update enemy color based on current health
        UpdateEnemyColor();
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private void UpdateEnemyColor()
    {
        // Calculate the lerp value based on current health (0 to 100)
        float lerpValue = 1.0f - (float)enemyModel.CurrentHealth / enemyModel.HealthPoint;

        // Interpolate between the original color and white
        Color lerpedColor = Color.Lerp(originalColor, Color.white, lerpValue);

        // Update the material's color
        enemyRenderer.material.color = lerpedColor;
    }
}
