using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    [SerializeField] private float speedChase;

    private ElementalType elementStatus = ElementalType.Null;
    private bool isActive = false;

    //elemental PopupUI
    private UIPopupElementHP uiPopupElementHP;
    private Sprite elementSprite;
    private Sprite reactionSprite;
    private Element elementScrptObj;
    private Coroutine isElementApplied;
    private Coroutine isReaction;

    private Animator animator;
    public float minSpeed = 3f;
    public float maxSpeed = 10f;
    private float spawnDuration = 2.0f;

    private VfxTest vfx;

    public float damageAmount;
    private bool isDeath = false;
    private bool isAttacking;
    private bool canAttack = true;
    private bool freezing = false;
    private bool burning = false;
    private bool slowness = false;
    private bool isSpawning;
    [SerializeField] private bool isInstantiate = true;

    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown = 2.0f; // Adjust the cooldown time as needed

    [SerializeField] private float attackVfxWait = 0f;

    private void Awake()
    {
        characterModel = FindObjectOfType<CharacterModel>();
        uiPopupElementHP = GetComponent<UIPopupElementHP>();
        enemyPool = FindObjectOfType<EnemyPool>();
        enemySpawnManagerTrigger = FindObjectOfType<EnemySpawnManagerTrigger>();
        enemyModel = GetComponent<EnemyModel>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        speedChase = Random.Range(minSpeed, maxSpeed);
        animator = GetComponentInChildren<Animator>();
        enemyCollider = GetComponent<Collider>();
        //enemyModel.LoadEnemyStats();
    }

    private void Start()
    {
        enemyModel.LoadEnemyStats();
        enemyModel.CurrentHealth = enemyModel.HealthPoint;
        if(isInstantiate) NewAudioManager.Instance.PlayEnemySFX("EnemySpawn");
        vfx = GetComponent<VfxTest>();

        if (vfx == null) {
            Debug.LogWarning("VfxTest component not found on this GameObject.");
        }
    }

    private void Update()
    {
        Invoke("EnemyBehavior", spawnDuration);

        //Logic for Showing HP Bar
        if (enemyModel.currentHealth != enemyModel.HealthPoint)
        {
            // Show HP Bar
            uiPopupElementHP.ShowUpdateHealthBarUI(enemyModel.currentHealth, enemyModel.healthPoint);
        }
    }

    private void EnemyBehavior() {
        if (!isDeath && target != null && !isSpawning) {
            float distanceToPlayer = Vector3.Distance(transform.position, target.position);

            if (distanceToPlayer <= attackRange) {
                if (canAttack) StartCoroutine(StartAttackPlayer());
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

        if (navMeshAgent.speed < 4)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isHopping", false);
        }
        else if (navMeshAgent.speed > 4)
        {
            animator.SetBool("isHopping", true);
            animator.SetBool("isWalking", false);
        }
    }

    private IEnumerator StartAttackPlayer()
    {
        canAttack = false;
        isAttacking = true;
        navMeshAgent.destination = transform.position;
        animator.SetBool("isWalking", false);
        animator.SetBool("isHopping", false);
        animator.SetBool("isAttacking", true);
        // Implement your attack logic here, e.g., dealing damage to the player
        //Invoke(nameof(AttackPlayer), attackVfxWait);
        //NewAudioManager.Instance.PlayEnemyAtkSFX("EnemyAtk");
        if (characterModel.healthPoint == 0) {
            NewAudioManager.Instance.enemyAtkSource.Stop();
        } else {
            NewAudioManager.Instance.PlayEnemyAtkSFX("EnemyAtk");
        }
        yield return new WaitForSeconds(attackCooldown); // Wait for the attack animation to finish

        animator.SetBool("isAttacking", false);
        isAttacking = false;
        canAttack = true;
    }

    //private void AttackPlayer() {
    //    AttackVfx();
    //    Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
    //    foreach (Collider collider in hitColliders) {
    //        if (collider.gameObject.CompareTag("Player")) {
    //            NewPlayerController1 playerController = collider.gameObject.GetComponent<NewPlayerController1>();
    //            if (playerController != null) {
    //                playerController.TakeDamage(damageAmount);
    //                Debug.Log("Player attacked");
    //            }
    //        }
    //    }
    //}

    //private void AttackVfx() {
    //    //vfx.AttackVFX();     
    //    //Debug.Log("Bam");
    //}


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        NewPlayerController1 playerController = collision.gameObject.GetComponent<NewPlayerController1>();

    //        if (playerController != null)
    //        {
                //playerController.TakeDamage(damageAmount);
    //        }
    //    }
    //}

    public void TakeDamage(float damageAmount)
    {       
        float damageTaken = (damageAmount - defense) + characterModel.elementalBonus;
        enemyModel.CurrentHealth -= damageTaken; // Reduce current health by the damage amount
        int randomHurtPattern = Random.Range(0, 3);
        animator.SetInteger("hurtPattern", randomHurtPattern);
        animator.SetTrigger("isHurt");
        navMeshAgent.speed = 0;
        NewAudioManager.Instance.sfxSource.Stop();
        NewAudioManager.Instance.PlayEnemySFX("EnemyHurt");
        if (enemyModel.CurrentHealth <= 0 && isDeath == false)
        {
            Death();
        }
    }

    public void ApplyElementalStatus(ElementalType elementType) {
        elementScrptObj = ElementalReactionController.Instance.GetElementScrptObj(elementType); //Get Element Scriptable Object

        if (elementStatus == ElementalType.Null) {
            elementStatus = elementType;
            Debug.Log("Applied Status " + elementStatus);

            //PopupUI
            //Reset ReactionUI When still occured
            if (isReaction != null)
            {
                StopCoroutine(isReaction);
                uiPopupElementHP.ResetReactionUI();
            }
            //Show Popup UI When No Element Appllied
            if (isElementApplied != null)
            {
                StopCoroutine(isElementApplied);
                uiPopupElementHP.ResetPopupUI();
            }
            elementSprite = elementScrptObj.elementSprite;
            uiPopupElementHP.ShowElementalPopup(elementSprite);
            isElementApplied = StartCoroutine(uiPopupElementHP.ElementPopupDuration());
        }
        else if (elementStatus == elementType)
        {
            //Reset Applied Element Duration if Same Type
            StopCoroutine(isElementApplied);
            isElementApplied = StartCoroutine(uiPopupElementHP.ElementPopupDuration());
        } else if (elementStatus != elementType) {
            //Trigger Elemental Reaction & Popup 2 Element
            uiPopupElementHP.ResetPopupUI();
            StopCoroutine(isElementApplied);
            uiPopupElementHP.ShowReactionPopupUI(elementSprite, elementScrptObj.elementSprite);
            isElementApplied = StartCoroutine(uiPopupElementHP.ResetReactionPopupUI());
            HandleElementalInteraction(elementStatus, elementType);
            ResetElementalStatus();
        }
    }

    // Function for resetting element
    public void ResetElementalStatus()
    {
        elementStatus = ElementalType.Null;
    }

    // Function to handle elemental interactions
    public void HandleElementalInteraction(ElementalType currentElement, ElementalType otherElement) {
        // Check for an elemental reaction between the player's element and the other element
        ElementalReaction reaction = ElementalReactionController.Instance.CheckElementalReaction(currentElement, otherElement);
        // Get reaction Sprite
        reactionSprite = reaction.reactionSprite;

        if (reaction != null) {
            // Handle the reaction, e.g., apply damage, change visuals, etc.
            // You can define specific logic for each reaction in this function.
            bool isStacking = reaction.stacking;

            switch (isStacking) {
                case true:
                    StartCoroutine(DamageOverTime(reaction.damageReaction, reaction.reactionInterval, reaction.reactionDuration));
                    speedChase -= reaction.movespeedChange;
                    StartCoroutine(ChangeSpeed(reaction.movespeedChange, reaction.reactionDuration));
                    HandleReaction(reaction.resultReaction, reaction.reactionDuration);
                    break;
                case false:
                    if (!isActive) {
                        isActive = true;
                        StartCoroutine(DamageOverTime(reaction.damageReaction, reaction.reactionInterval, reaction.reactionDuration));
                        speedChase -= reaction.movespeedChange;
                        StartCoroutine(ChangeSpeed(reaction.movespeedChange, reaction.reactionDuration));
                        HandleReaction(reaction.resultReaction, reaction.reactionDuration);
                    }
                    break;
            }
        }
    }

    private IEnumerator DamageOverTime(int damage, float interval, float duration) {
        if (damage != 0) {
            float endTime = Time.time + duration;

            while (Time.time < endTime) {
                TakeDamage(damage);
                yield return new WaitForSeconds(interval);
            }
            isActive = false;
        }
    }

    private IEnumerator ChangeSpeed(float speedValue, float duration) {
        if (speedValue != 0) {
            yield return new WaitForSeconds(duration);
            speedChase += speedValue;
            isActive = false;
        }
    }

    // Function to handle the reaction result
    private void HandleReaction(string resultReaction, float reactionDuration) {

        // Reaction Popup
        isReaction = StartCoroutine(ReactionUIDuration(reactionSprite, reactionDuration));

        if (resultReaction == "Freezing" && !freezing) {
            Debug.Log("Terjadi Reaksi " + resultReaction);
            freezing = true;
            vfx.Freeze(reactionDuration);
            //Invoke("Unfreeze", reactionDuration);
            //Invoke("HandleFreezing", reactionDuration + 0.5f);
            StartCoroutine(VfxHandleElemental(resultReaction, reactionDuration));
            StartCoroutine(VfxHandleElementalState(resultReaction, reactionDuration + 0.5f));
        } else if (resultReaction == "Burning") {
            burning = true;
            vfx.Combustion(reactionDuration);
            StartCoroutine(VfxHandleElemental(resultReaction, reactionDuration));
            StartCoroutine(VfxHandleElementalState(resultReaction, reactionDuration + 0.5f));
            //vfx.Slowness(reactionDuration);
        } else if (resultReaction == "Slowness") {
            slowness = true;
            vfx.Steam(reactionDuration);
            StartCoroutine(VfxHandleElemental(resultReaction, reactionDuration));
            StartCoroutine(VfxHandleElementalState(resultReaction, reactionDuration + 0.5f));
        }
    }

    IEnumerator ReactionUIDuration(Sprite spriteReaction, float reactionTime) {
        yield return new WaitForSeconds(0.5f);
        uiPopupElementHP.ResetPopupUI();
        uiPopupElementHP.ShowReactionUI(spriteReaction, reactionTime - 0.5f);
        //yield return new WaitForSeconds(reactionTime);
        //uiPopupElementHP.ResetReactionUI();
    }

        IEnumerator VfxHandleElemental(string resultReaction, float delayTime) {
        yield return new WaitForSeconds(delayTime);

        if (resultReaction == "Freezing") {
            vfx.Unfreeze();
        } else if (resultReaction == "Burning") {
            vfx.UnCombustion();
        } else if (resultReaction == "Slowness") {
            vfx.UnSteam();
            speedChase = Random.Range(minSpeed, maxSpeed);
            ChasePlayer();
            Debug.Log("Steam done");
        } 

    }

    IEnumerator VfxHandleElementalState(string resultReaction, float delayTime) {
        yield return new WaitForSeconds(delayTime);

        if (resultReaction == "Freezing") {
            freezing = false;
        } else if (resultReaction == "Burning") {
            burning = false;
        } else if (resultReaction == "Slowness") {
            slowness = false;
        }

    }


    private void Death()
    {
        ScoreManager.Instance.EnemyKilled();
        Destroy(this.gameObject, 3f);
        enemyPool.NotifyEnemyDied();
        enemyCollider.enabled = false;
        //navMeshAgent.speed = speedChase/2;
        navMeshAgent.speed = 0;
        isDeath = true;
        animator.SetBool("Death", true);
        NewAudioManager.Instance.PlayEnemySFX("EnemyDeath");
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
