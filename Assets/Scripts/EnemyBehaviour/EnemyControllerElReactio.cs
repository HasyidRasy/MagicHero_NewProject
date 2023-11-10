using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyControllerElReactio : MonoBehaviour
{
    private EnemyModel enemyModel;
    private Transform target; // Transform pemain yang akan dikejar
    private NavMeshAgent navMeshAgent;    
    private Renderer enemyRenderer;
    private Material originalMaterial;
    private Color originalColor;
    private float speedChase;

    //elemental system
    private ElementalType elementStatus = ElementalType.Null;
    private bool isActive = false;

    //elemental PopupUI
    private UIElementPopup uiElementPopup;
    private Sprite elementSprite;
    private Element elementScrptObj;
    private Coroutine isElementApplied;
    
    public float minSpeed = 3f;
    public float maxSpeed = 10f;

    public float damageAmount;

    private void Awake()
    {
        enemyModel = GetComponent<EnemyModel>();
        uiElementPopup = GetComponent<UIElementPopup>();
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

        if (enemyModel.currentHealth != enemyModel.HealthPoint)
        {
            // Show HP Bar
            uiElementPopup.ShowUpdateHealthBarUI(enemyModel.currentHealth, enemyModel.healthPoint);
        }

        UpdateEnemyColor();
    }

    private void OnCollisionEnter(Collision collision)
    {      
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerControllerElementalReaction playerControllerElementalReaction = collision.gameObject.GetComponent<PlayerControllerElementalReaction>();

                if (playerControllerElementalReaction != null)
                {
                    playerControllerElementalReaction.TakeDamage(damageAmount);
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

    public void ApplyElementalStatus(ElementalType elementType)
    {   
        elementScrptObj = ElementalReactionController.Instance.GetElementScrptObj(elementType);

        if (elementStatus == ElementalType.Null)
        {
            elementStatus = elementType;
            Debug.Log("Applied Status "+ elementStatus);

            //PopupUI
            if (isElementApplied != null)
            {
                StopCoroutine(isElementApplied);
                uiElementPopup.ResetPopupUI();
            }
            elementSprite = elementScrptObj.elementSprite;
            uiElementPopup.ShowElementalPopup(elementSprite);
            isElementApplied = StartCoroutine(uiElementPopup.ElementPopupDuration());
        }
        else if (elementStatus == elementType)
        {
            StopCoroutine(isElementApplied);
            isElementApplied = StartCoroutine(uiElementPopup.ElementPopupDuration());
        }
        else if (elementStatus != elementType)
        {
            uiElementPopup.ResetPopupUI();
            StopCoroutine(isElementApplied);
            uiElementPopup.ShowReactionPopupUI(elementSprite, elementScrptObj.elementSprite);
            isElementApplied = StartCoroutine(uiElementPopup.ResetReactionPopupUI());
            HandleElementalInteraction(elementStatus, elementType);
            ResetElementalStatus();
        }
    }

    public void ResetElementalStatus()
    {
        elementStatus = ElementalType.Null;
    }

    // Function to handle elemental interactions
    public void HandleElementalInteraction(ElementalType currentElement, ElementalType otherElement)
    {
        // Check for an elemental reaction between the player's element and the other element
        ElementalReaction reaction = ElementalReactionController.Instance.CheckElementalReaction(currentElement, otherElement);

        if (reaction != null)
        {
            // Handle the reaction, e.g., apply damage, change visuals, etc.
            // You can define specific logic for each reaction in this function.
            bool isStacking = reaction.stacking;

            switch (isStacking)
            {
                case true:
                    StartCoroutine(DamageOverTime(reaction.damageReaction, reaction.reactionInterval, reaction.reactionDuration));
                    speedChase -= reaction.movespeedChange;
                    StartCoroutine(ChangeSpeed(reaction.movespeedChange, reaction.reactionDuration));
                    HandleReaction(reaction.resultReaction);
                    break;
                case false:
                    if (!isActive)
                    {
                        isActive = true;
                        StartCoroutine(DamageOverTime(reaction.damageReaction, reaction.reactionInterval, reaction.reactionDuration));
                        speedChase -= reaction.movespeedChange;
                        StartCoroutine(ChangeSpeed(reaction.movespeedChange, reaction.reactionDuration));
                        HandleReaction(reaction.resultReaction);
                    }
                    break;
            } 
        }
    }

    private IEnumerator DamageOverTime(int damage, float interval, float duration)
    {
        if (damage != 0)
        {
            float endTime = Time.time + duration;

            while (Time.time < endTime)
            {
                TakeDamage(damage);
                yield return new WaitForSeconds(interval);
            }
            isActive = false;
        }
    }

    private IEnumerator ChangeSpeed(float speedValue, float duration)
    {
        if (speedValue != 0)
        {
            yield return new WaitForSeconds(duration);
            speedChase += speedValue;
            isActive = false;
        }
    }

    // Function to handle the reaction result
    private void HandleReaction(string resultReaction)
    {
        // Implement logic for the reaction, e.g., change player's appearance, apply effects, etc.
        Debug.Log("Terjadi Reaksi " + resultReaction);
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

