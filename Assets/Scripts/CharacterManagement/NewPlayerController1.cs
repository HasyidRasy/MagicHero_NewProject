using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewPlayerController1 : MonoBehaviour
{
    private CharacterModel characterModel;
    private Animator animator;
    private ElementSwitchSystem elementSwitchSystem;
    private CooldownAttackUI cooldownAtkUI;
    [SerializeField] private bool isDashing = false;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _model;
    [SerializeField] private Collider _collider;

    private Vector3 _input; 

    [Header("Attack Pattern")]
    [SerializeField] private ElementalType[] elementalSlots = new ElementalType[4]; 
    private int currentSlotIndex = 0;

    private ElementalType[] attackPattern = new ElementalType[4];
    private int currentAttackIndex = 0;

    [Space(5)]
    [SerializeField] private Transform projectileSpawnPoint;    
    [SerializeField] private GameObject magicProjectilePrefab;  

    [Header("Magic Speed")]
    [SerializeField] private float delayShoot = 2f;
    [SerializeField] private float magicProSpeed = 10f;         

    [Header("Casting Speed")]
    public float timeBetweenAttacks = 0.5f;   
    [SerializeField] private float timeBetweenSteps = 0.5f;  
    private bool isStepping = false;
    public float attackCooldown = 0f;
    private float stepCooldown = 0f;

    [SerializeField] private LayerMask groundMask;

    [Header("Jarak Antara Player dan Spawn Magic ")]
    [SerializeField] private float distanceInFront = 2.0f;

    [SerializeField] public Camera mainCamera;
    private bool isShooting = false;
    private Vector3 targetDirection;
    [SerializeField] private bool isAttacking = true;
    [SerializeField] private bool canMove = true;

    public static event Action OnPlayerDeath;
    public static event Action OnPlayerHurt;

    [Header("Player Info")]
    private Vector3 moveDir;
    public Slider _dashCooldownSlider;
    public float _currentDashCd = 0f;
    private bool isDeath = false;

    [HideInInspector]
    public int currentButtonIndex = 0;

    [Header("TeleportVfx")]
    [SerializeField] private GameObject vfxTeleport;
    [SerializeField] private GameObject vfxTeleportMaterial;

    private GameObject currentVfx;

    private void OnDestroy()
    {
        CharacterModel.Instance.SavePlayerStats();
        SaveElementalSlots();
        elementSwitchSystem.SaveElementStatus();
        ScoreManager.Instance.SavePlayerScore();
        LevelManager.Instance.IncreaseLevel();
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        UIManager.OnRestart += RestartPlayer;
        LoadLevelOnCollision.OnTeleport += VfxTeleport;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        UIManager.OnRestart -= RestartPlayer;
        LoadLevelOnCollision.OnTeleport -= VfxTeleport;

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        mainCamera = Camera.main;
    }
    private void Awake()
    {
        characterModel = GetComponent<CharacterModel>();
        animator = GetComponentInChildren<Animator>();
        elementSwitchSystem = FindObjectOfType<ElementSwitchSystem>();
        cooldownAtkUI = FindObjectOfType<CooldownAttackUI>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        if (PlayerPrefs.HasKey("PlayerHealth") && PlayerPrefs.HasKey("PlayerDefence") && PlayerPrefs.HasKey("PlayerAttack"))
        {
            characterModel.LoadPlayerStats();
        }
        else
        {
            characterModel.ResetStats();
        }
        ScoreManager.Instance.StartGame();
        attackPattern[0] = elementalSlots[0];
        elementSwitchSystem.LoadElementStatus();
        LoadElementalSlots();
        cooldownAtkUI.SetElement(attackPattern[currentAttackIndex]);
        FirstVfxTeleport();
        ScoreManager.Instance.LoadPlayerScore();
    }

    private void Update()
    {
        PlayerStat();
        if (attackCooldown > 0) {
            attackCooldown -= Time.deltaTime;
        }
        stepCooldown -= Time.deltaTime;
        if (_currentDashCd > 0) {
            _currentDashCd -= Time.deltaTime;
        }

        _dashCooldownSlider.value = Mathf.Lerp(0f, 1f, 1f - (_currentDashCd / (characterModel.DashCooldown 
                                                              + characterModel.DashDuration)));
        Aim();
    }

    private void FixedUpdate() {
        if (!isShooting && canMove) CharaMove();
        else _rb.velocity = Vector3.zero; 
    }

    private void CharaMove() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(horizontal, 0f, vertical).normalized;

        bool isWalking = moveDir != Vector3.zero;

        if (!isWalking) {
            animator.SetBool("isDashing", false);
        }
        if (moveDir != Vector3.zero && isStepping == false) {
            StartCoroutine(Stepping(timeBetweenSteps));
        }

        // Set the "isWalking" parameter in the animator
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isDashing", false);

        // Calculate the desired velocity
        Vector3 velocity = moveDir.ToIso() * moveDir.magnitude * characterModel.moveSpeed;

        velocity.y += -100f * Time.deltaTime;

        // Apply velocity to the Rigidbody
        _rb.velocity = velocity;

        if (moveDir == Vector3.zero) return;
        Quaternion rotation = Quaternion.LookRotation(moveDir.ToIso(), Vector3.up);
        _model.rotation = Quaternion.RotateTowards(_model.rotation, rotation, characterModel.rotationSpeed * Time.deltaTime);

        // Call Coroutine Dash
        if (Input.GetKey(KeyCode.LeftShift) && !isDashing && isWalking) {
            NewAudioManager.Instance.PlayPlayerSFX("Dash");
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash() {
        _currentDashCd = characterModel.DashCooldown + characterModel.dashDuration;
        isDashing = true;
        animator.SetBool("isWalking", false);
        animator.SetBool("isDashing", true);

        Vector3 dashDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        Vector3 dashVelocity = dashDir.ToIso() * dashDir.magnitude * characterModel.dashSpeed * Time.deltaTime;

        _rb.velocity = dashVelocity;

        DashTrail dashvfx = GetComponent<DashTrail>();

        if(dashvfx != null) dashvfx.StartDashVfx();

        yield return new WaitForSeconds(characterModel.dashDuration);

        _rb.velocity = Vector3.zero;

        animator.SetBool("isDashing", false); 
        yield return new WaitForSeconds(characterModel.DashCooldown);
        isDashing = false;
    }

    private IEnumerator Stepping(float duration)
    {
        isStepping = true;
        NewAudioManager.Instance.PlayStepSFX("StepOnDirt");
        yield return new WaitForSeconds(duration);
        isStepping = false;
    }

    private IEnumerator DisableShootingForDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        isShooting = false;
        animator.SetBool("isAttacking", false);
    }

    private void PlayerStat()
    {
        float playerHp = characterModel.HealthPoint;
        float playerDef = characterModel.Defence;
    }

    public void TakeDamage(float damageAmount)
    {
        if(damageAmount <= characterModel.defence) {
            characterModel.HealthPoint -= 1;
            } else {
            characterModel.HealthPoint -= (damageAmount-characterModel.defence); // Reduce current health by the damage amount
        }
        animator.SetTrigger("isHurt");
        if (isDeath == false) {
            OnPlayerHurt?.Invoke();
            NewAudioManager.Instance.PlayPlayerSFX("PlayerHurt");
        }
        if (characterModel.HealthPoint <= 0)
        {
            if (isDeath == false) {
                NewAudioManager.Instance.bgmSource.Stop();
                NewAudioManager.Instance.PlayPlayerSFX("PlayerDeath");
                Invoke(nameof(GameOver), 2f);
            }
            isDeath = true;
            Death(); 
            ShowDeathPanel();
        }
    }

    private void GameOver() {
        NewAudioManager.Instance.PlayPlayerSFX("GameOver");
    }

    private void Death()
    {
        animator.SetBool("isDeath", true);
        characterModel.rotationSpeed = 0;
        characterModel.moveSpeed = 0;
        ScoreManager.Instance.EndGame();
        ScoreManager.Instance.DisplayGameOverStats();
    }
    private void ShowDeathPanel() {
        OnPlayerDeath?.Invoke();
    }
    private void RestartPlayer() {
        Destroy(gameObject);
    }

    private void ShootMagic(ElementalType element)
    {
            targetDirection.Normalize(); 

            // Hitung posisi spawn di depan pemain
            Vector3 spawnPosition = projectileSpawnPoint.position + transform.forward * distanceInFront;
            GameObject magic = Instantiate(magicProjectilePrefab, spawnPosition, Quaternion.LookRotation(targetDirection));

            MagicProjectileElementalReaction magicProjectile = magic.GetComponent<MagicProjectileElementalReaction>();
            if (magicProjectile != null)
            {
                magicProjectile.SetElement(element);
            }
            // Mengatur kecepatan proyektil sesuai arah target
            Rigidbody rb = magic.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = targetDirection * magicProSpeed;
            }
            ChangeActiveElement();
            SetAttackCooldown();

            Debug.Log(elementalSlots[currentSlotIndex]);
        StartCoroutine(DisableShootingForDuration(timeBetweenAttacks));
    }
    public void ResetAttackIndex()
    {
        currentSlotIndex = 0;
        attackPattern[currentAttackIndex] = elementalSlots[currentAttackIndex];
    }
    public void SetAttackCooldown() {
        cooldownAtkUI.SetElement(elementalSlots[currentSlotIndex]);
    }
    private void ChangeActiveElement()
    {
        currentSlotIndex = (currentSlotIndex + 1) % 4;
        attackPattern[currentAttackIndex] = elementalSlots[currentSlotIndex];
    }
    public ElementalType[] GetCurrentAttackPattern()
    {
        return elementalSlots;
    }

    public int GetCurrentAttackIndex()
    {
        return currentSlotIndex;
    }

    private void Aim()
    {
        if (Input.GetButtonDown("Fire1") && attackCooldown <= 0f && isAttacking && !isShooting && Time.timeScale !=0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
            {

                isShooting = true;
                animator.SetBool("isAttacking", true);

                targetDirection = hit.point - transform.position;
                targetDirection.y = 0;
                transform.forward = targetDirection.normalized;

                Invoke("DelayedShootMagic", delayShoot);
                attackCooldown = timeBetweenAttacks;
            }
        }
    }

    void DelayedShootMagic()
    {
        ShootMagic(attackPattern[currentAttackIndex]);
        Debug.Log("current attack index = " + currentSlotIndex);
    }

    public void SetAttackPattern(ElementalType newElement)
    {
        elementalSlots[currentButtonIndex] = newElement;
    }
    public void SetDefaultElementSlots()
    {
        elementalSlots[0] = ElementalType.Fire; 
        elementalSlots[1] = ElementalType.Fire; 
        elementalSlots[2] = ElementalType.Fire;
        elementalSlots[3] = ElementalType.Fire;
        SaveElementalSlots();
    }
    private void SaveElementalSlots()
    {
        for (int i = 0; i < elementalSlots.Length; i++)
        {
            PlayerPrefs.SetInt("ElementalSlot_" + i, (int)elementalSlots[i]);
        }
        PlayerPrefs.Save();
    }
    public void LoadElementalSlots()
    {
        for (int i = 0; i < elementalSlots.Length; i++)
        {
            int savedElement = PlayerPrefs.GetInt("ElementalSlot_" + i, 0);
            elementalSlots[i] = (ElementalType)savedElement;
        }
    }
    private void VfxTeleport() {
        canMove = false;
        animator.SetBool("isWalking",false);
        vfxTeleportMaterial.SetActive(true);
        float yOffset = 1.0f;
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
        currentVfx = Instantiate(vfxTeleport, newPosition, transform.rotation, transform);
        Invoke(nameof(DestroyVfxTeleport), 3f);
        Destroy(currentVfx,3.5f);
    }

    private void FirstVfxTeleport() {
        canMove = false;
        animator.SetBool("isWalking", false);
        vfxTeleportMaterial.SetActive(true);
        float yOffset = 1.0f;
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
        currentVfx = Instantiate(vfxTeleport, newPosition, transform.rotation, transform);
        Invoke(nameof(DestroyVfxTeleport), 1.5f);
        Destroy(currentVfx, 2f);
    }

    private void DestroyVfxTeleport() {
        canMove = true;
        vfxTeleportMaterial.SetActive(false);
    }
}

public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0f, -45f, 0f));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
