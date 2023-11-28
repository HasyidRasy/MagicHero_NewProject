using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewPlayerController1 : MonoBehaviour
{
    //get model
    private CharacterModel characterModel;
    private Animator animator;
    private ElementSwitchSystem elementSwitchSystem;
    private CooldownAttackUI cooldownAtkUI;
    //cek dash logic
    private bool isDashing = false;
    //rigidbody
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _model;
    [SerializeField] private Collider _collider;

    private Vector3 _input; 

    [Header("Attack Pattern")]
    [SerializeField] private ElementalType[] elementalSlots = new ElementalType[4]; // Set Attack Pattern
    private int currentSlotIndex = 0;

    private ElementalType[] attackPattern = new ElementalType[4];
    private int currentAttackIndex = 0;

    [Space(5)]
    [SerializeField] private Transform projectileSpawnPoint;    // Titik spawn proyektil
    [SerializeField] private GameObject magicProjectilePrefab;  // Prefab untuk sihir

    [Header("Magic Speed")]
    [SerializeField] private float delayShoot = 2f;
    [SerializeField] private float magicProSpeed = 10f;         // Kecepatan proyektil

    [Header("Casting Speed")]
    public float timeBetweenAttacks = 0.5f;   // Waktu antara serangan
    [SerializeField] private float timeBetweenSteps = 0.5f;   // Waktu antara serangan
    public float attackCooldown = 0f;
    private float stepCooldown = 0f;

    [SerializeField] private LayerMask groundMask;

    [Header("Jarak Antara Player dan Spawn Magic ")]
    [SerializeField] private float distanceInFront = 2.0f; // Sesuaikan dengan jarak yang Anda inginkan

    [SerializeField] public Camera mainCamera;
    private bool isShooting = false;
    private Vector3 targetDirection;
    [SerializeField] private bool isAttacking = true;

    public static event Action OnPlayerDeath;
    public static event Action OnPlayerHurt;

    [Header("Player Info")]
    private Vector3 moveDir;
    public Slider _dashCooldownSlider;
    public float _currentDashCd;
    public bool _isIncrease;
    private bool isDeath = false;

    [HideInInspector]
    public int currentButtonIndex = 0;

    private void OnDestroy()
    {
        CharacterModel.Instance.SavePlayerStats();
        SaveElementalSlots();
        elementSwitchSystem.SaveElementStatus();
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        UIManager.OnRestart += RestartPlayer;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        UIManager.OnRestart -= RestartPlayer;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        // Assign the mainCamera reference when a new scene is loaded
        mainCamera = Camera.main;
        Debug.Log("kamera : " + mainCamera);
    }
    private void Awake()
    {
        characterModel = GetComponent<CharacterModel>();
        animator = GetComponentInChildren<Animator>();
        elementSwitchSystem = FindObjectOfType<ElementSwitchSystem>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        attackPattern[0] = elementalSlots[0];
    }

    private void Update()
    {
        PlayerStat();
        attackCooldown -= Time.deltaTime;
        stepCooldown -= Time.deltaTime;
        _currentDashCd += Time.deltaTime;

        Aim();

        //Call sfx player walk
        if ((Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.1f) && stepCooldown <= 0f) {
            Stepping(stepCooldown);
            stepCooldown = timeBetweenSteps;
            StartCoroutine(Stepping(stepCooldown));
        }

        //if (!isDashing) {
        //    if (_isIncrease && isDashing) {
        //        float cdValue = Mathf.Lerp(0f, 1f, _currentDashCd / characterModel.DashCooldown);
        //        _dashCooldownSlider.value = cdValue;

        //        if (_currentDashCd >= characterModel.DashCooldown) {
        //            _currentDashCd = 0f;
        //            _isIncrease = false;
        //        }
        //    } else {
        //        float cdValue = Mathf.Lerp(1f, 0f, _currentDashCd / characterModel.DashDuration);
        //        _dashCooldownSlider.value = cdValue;

        //        if (_currentDashCd >= characterModel.DashDuration) {
        //            _currentDashCd = 0f;
        //            _isIncrease = true;
        //        }
        //    }
        //}

        if (_isIncrease) {
            float cdValue = Mathf.Lerp(0f, 1f, _currentDashCd / characterModel.DashCooldown);
            _dashCooldownSlider.value = cdValue;

            if (_currentDashCd >= characterModel.DashCooldown) {
                _currentDashCd = 0f;
                _isIncrease = false;
            }
        } else if (!_isIncrease && Input.GetKeyDown(KeyCode.LeftShift) && moveDir != Vector3.zero) {
            float cdValue = Mathf.Lerp(1f, 0f, _currentDashCd / characterModel.DashDuration);
            _dashCooldownSlider.value = cdValue;

            if (_currentDashCd >= characterModel.DashDuration) {
                _currentDashCd = 0f;
                _isIncrease = true;
            }
        }
    }

    private void FixedUpdate() {
        if (!isShooting) CharaMove();
        else _rb.velocity = Vector3.zero; //stop move & sliding
    }

    private void CharaMove() {
        // Input movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Move direction
        moveDir = new Vector3(horizontal, 0f, vertical).normalized;

        // Check if the character is walking
        bool isWalking = moveDir != Vector3.zero;

        if (!isWalking) {
            animator.SetBool("isDashing", false);
        }


        // Set the "isWalking" parameter in the animator
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isDashing", false);

        // Calculate the desired velocity
        Vector3 velocity = moveDir.ToIso() * moveDir.magnitude * characterModel.moveSpeed;

        // Apply velocity to the Rigidbody
        _rb.velocity = velocity;

        // New Look Logic (rotation)
        if (moveDir == Vector3.zero) return;
        Quaternion rotation = Quaternion.LookRotation(moveDir.ToIso(), Vector3.up);
        _model.rotation = Quaternion.RotateTowards(_model.rotation, rotation, characterModel.rotationSpeed * Time.deltaTime);

        // Call Coroutine Dash
        if (Input.GetKey(KeyCode.LeftShift) && !isDashing && isWalking) {
            StartCoroutine(Dash());
        }
    }

    // Coroutine Dash Logic
    private IEnumerator Dash() {
        isDashing = true;
        animator.SetBool("isWalking", false);
        animator.SetBool("isDashing", true); // Set isDashing to true while dashing

        float startTime = Time.time;
        Vector3 pos = transform.position;
        Vector3 dashDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        //while (Time.time < startTime + characterModel.DashDuration) {
        //    _rb.MovePosition(transform.position + dashDir.ToIso() * dashDir.magnitude * characterModel.dashSpeed * Time.deltaTime);
        //    yield return null;
        //}

        Vector3 dashVelocity = dashDir.ToIso() * dashDir.magnitude * characterModel.dashSpeed * Time.deltaTime;

        _rb.velocity = dashVelocity;

        DashTrail dashvfx = GetComponent<DashTrail>();

        if(dashvfx != null) dashvfx.StartDashVfx();

        yield return new WaitForSeconds(characterModel.dashDuration);

        _rb.velocity = Vector3.zero;

        animator.SetBool("isDashing", false); // Set isDashing back to false after dashing
        yield return new WaitForSeconds(characterModel.DashCooldown);
        isDashing = false;
    }

    private IEnumerator Stepping(float duration)
    {
        NewAudioManager.Instance.PlayStepSFX("StepOnDirt");
        yield return new WaitForSeconds(duration);
    }

    // Coroutine untuk menonaktifkan isShooting selama durasi tertentu
    private IEnumerator DisableShootingForDuration(float duration)
    {
        //isShooting = true;
        yield return new WaitForSeconds(duration);
        isShooting = false;
        animator.SetBool("isAttacking", false);
    }

    //Player Stat
    private void PlayerStat()
    {
        float playerHp = characterModel.HealthPoint;
        float playerDef = characterModel.Defence;
    }

    public void TakeDamage(float damageAmount)
    {
if(damageAmount < characterModel.defence)
        {
            characterModel.HealthPoint -= 1;
        }
        else
        {
        characterModel.HealthPoint -= (damageAmount-characterModel.defence); // Reduce current health by the damage amount
}
        animator.SetTrigger("isHurt");
        if (isDeath == false) {
            OnPlayerHurt?.Invoke();
        }
        if (characterModel.HealthPoint <= 0)
        {
            isDeath = true;
            Death(); // If health drops to or below zero, call a method to handle enemy death
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
        //CharacterModel.Instance.ResetStats();
    }
    private void ShowDeathPanel() {
        OnPlayerDeath?.Invoke();
    }
    private void RestartPlayer() {
        Destroy(gameObject);
    }

    private void ShootMagic(ElementalType element)
    {
            targetDirection.Normalize(); // Normalisasi agar memiliki panjang 1

            // Hitung posisi spawn di depan pemain
            Vector3 spawnPosition = projectileSpawnPoint.position + transform.forward * distanceInFront;

            // Instansiasi proyektil di titik spawn
            GameObject magic = Instantiate(magicProjectilePrefab, spawnPosition, Quaternion.LookRotation(targetDirection));

            // Implementasi logika menembakkan sihir sesuai elemen
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
        // Menonaktifkan isShooting setelah menembak
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
        // Mengganti elemen aktif ke slot berikutnya
        currentSlotIndex = (currentSlotIndex + 1) % 4;
        // Memperbarui pola serangan berdasarkan elemen yang baru aktif
        attackPattern[currentAttackIndex] = elementalSlots[currentSlotIndex];
    }
    public ElementalType[] GetCurrentAttackPattern()
    {
        // Mengembalikan elemen yang sedang digunakan dalam pola serangan saat ini
        return elementalSlots;
    }

    // Method untuk mendapatkan indeks attack saat ini
    public int GetCurrentAttackIndex()
    {
        return currentSlotIndex;
    }

    private void CheckElementalReaction()
    {
        // Mengecek apakah ada 2 elemen berturut-turut dalam pola serangan
        if (currentAttackIndex >= 2)
        {
            ElementalType firstElement = attackPattern[currentAttackIndex - 2];
            ElementalType secondElement = attackPattern[currentAttackIndex - 1];

            // Melakukan pengecekan elemen reaksi
            ElementalReaction(firstElement, secondElement);
        }
    }

    private void ElementalReaction(ElementalType element1, ElementalType element2)
    {
        // Implementasi efek-efek elemen reaksi
        if ((element1 == ElementalType.Fire && element2 == ElementalType.Wind) ||
            (element1 == ElementalType.Wind && element2 == ElementalType.Fire))
        {
            Debug.Log("Combustion!");
        }
        else if ((element1 == ElementalType.Fire && element2 == ElementalType.Water) ||
                 (element1 == ElementalType.Water && element2 == ElementalType.Fire))
        {
            Debug.Log("Steam!");
        }
        else if ((element1 == ElementalType.Wind && element2 == ElementalType.Water) ||
                 (element1 == ElementalType.Water && element2 == ElementalType.Wind))
        {
            Debug.Log("Freeze!");
        }
    }

    private void Aim()
    {
        if (Input.GetButtonDown("Fire1") && attackCooldown <= 0f && isAttacking && !isShooting && Time.timeScale !=0)
        {
            // Raycast dari kursor mouse ke dunia 3D
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
            {

                isShooting = true;
                animator.SetBool("isAttacking", true);

                // Menghitung vektor arah ke titik klik mouse
                targetDirection = hit.point - transform.position;

                // Mengabaikan perubahan tinggi (sumbu Y)
                targetDirection.y = 0;

                // Membuat pemain melihat ke arah klik mouse
                transform.forward = targetDirection.normalized;

                // Menembak proyektil ke arah tersebut
                Invoke("DelayedShootMagic", delayShoot);
                //ShootMagic(attackPattern[currentAttackIndex]);

                // Mengatur waktu cooldown
                attackCooldown = timeBetweenAttacks;

                CheckElementalReaction();
            }
        }
    }

    void DelayedShootMagic()
    {
        // Here you can call ShootMagic with the attack pattern.
        ShootMagic(attackPattern[currentAttackIndex]);
        Debug.Log("current attack index = " + currentSlotIndex);
    }

    public void SetAttackPattern(ElementalType newElement)
    {
        elementalSlots[currentButtonIndex] = newElement;
        //Debug.Log("curent button index = " + currentButtonIndex);
    }
    
    public void GetCamera(Camera cam) {
        var newCamera = cam;
        mainCamera = newCamera;
        Debug.Log("A key pressed. Camera: " + (cam != null ? cam.name : "null"));
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
            // Baca data PlayerPrefs dan konversi ke enum ElementalType
            int savedElement = PlayerPrefs.GetInt("ElementalSlot_" + i, 0);
            elementalSlots[i] = (ElementalType)savedElement;
        }
    }
    private (bool success, Vector3 position) GetMousePosition() {

            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask)) {
                // The Raycast hit something, return with the position.
                return (success: true, position: hitInfo.point);
            } else {
                // The Raycast did not hit anything.
                return (success: false, position: Vector3.zero);
            }
  
    }

    //helpers
    //public static class Helpers
    //{
    //    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0f, -45f, 0f));
    //    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
    //}
}
