using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController1 : MonoBehaviour
{
    //get model
    private CharacterModel characterModel;
    private Animator animator;
    //cek dash logic
    private bool isDashing = false;
    //rigidbody
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _model;

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
    [SerializeField] private float timeBetweenAttacks = 0.5f;   // Waktu antara serangan
    [SerializeField] private float timeBetweenSteps = 0.5f;   // Waktu antara serangan
    private float attackCooldown = 0f;
    private float stepCooldown = 0f;

    [SerializeField] private LayerMask groundMask;

    private Camera mainCamera;
    private bool isShooting = false;
    [SerializeField] private bool isAttacking = true;

    public static event Action OnPlayerDeath;

    private void Awake()
    {
        characterModel = GetComponent<CharacterModel>();
        animator = GetComponentInChildren<Animator>();
        
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        //if (!isShooting) CharaMove();
        PlayerStat();
        attackCooldown -= Time.deltaTime;
        stepCooldown -= Time.deltaTime;

        

        Aim();

        //Call sfx player walk
        if ((Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.1f) && stepCooldown <= 0f) {
            Stepping(stepCooldown);
            stepCooldown = timeBetweenSteps;
            StartCoroutine(Stepping(stepCooldown));
        }
    }

    private void FixedUpdate() {
        if (!isShooting) CharaMove();
    }

    private void CharaMove() {
        // Input movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Move direction
        Vector3 moveDir = new Vector3(horizontal, 0f, vertical).normalized;

        // Check if the character is walking
        bool isWalking = moveDir != Vector3.zero;

        // Set the "isWalking" parameter in the animator
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isDashing", isDashing);

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

        while (Time.time < startTime + characterModel.DashDuration) {
            _rb.MovePosition(transform.position + dashDir.ToIso() * dashDir.magnitude * characterModel.dashSpeed * Time.deltaTime);
            yield return null;
        }

        animator.SetBool("isDashing", false); // Set isDashing back to false after dashing
        yield return new WaitForSeconds(characterModel.DashCooldown);
        isDashing = false;
    }


    private IEnumerator Stepping(float duration)
    {
        NewAudioManager.Instance.PlaySFX("StepOnDirt");
        yield return new WaitForSeconds(duration);
    }

    // Coroutine untuk menonaktifkan isShooting selama durasi tertentu
    private IEnumerator DisableShootingForDuration(float duration)
    {
        isShooting = true;
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
        characterModel.HealthPoint -= damageAmount; // Reduce current health by the damage amount
        animator.SetTrigger("isHurt");
        if (characterModel.HealthPoint <= 0)
        {          
            Death(); // If health drops to or below zero, call a method to handle enemy death
            OnPlayerDeath?.Invoke();
        }
    }

    private void Death()
    {
        animator.SetBool("isDeath", true);
        characterModel.rotationSpeed = 0;
        characterModel.moveSpeed = 0;
    }

    private void ShootMagic(ElementalType element)
    {
        // Raycast dari kursor mouse ke dunia 3D
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetDirection = hit.point - projectileSpawnPoint.position;    // Menghitung vektor arah ke target
            targetDirection.Normalize(); // Normalisasi agar memiliki panjang 1

            // Instansiasi proyektil di titik spawn
            GameObject magic = Instantiate(magicProjectilePrefab, projectileSpawnPoint.position, Quaternion.LookRotation(targetDirection));

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
            // Menonaktifkan isShooting setelah menembak
            StartCoroutine(DisableShootingForDuration(timeBetweenAttacks));
        }
    }

    private void ChangeActiveElement()
    {
        // Mengganti elemen aktif ke slot berikutnya
        currentSlotIndex = (currentSlotIndex + 1) % 4;
        // Memperbarui pola serangan berdasarkan elemen yang baru aktif
        attackPattern[currentAttackIndex] = elementalSlots[currentSlotIndex];
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
        var (success, position) = GetMousePosition();
        if (success)
        {
            var direction = position - transform.position;

            // mengabaikan sumbu y
            direction.y = 0;

            //cek raycast jika berada di groundMask layer
            RaycastHit hitInfo;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // raycast jika berada di groundMask layer maka boleh shooting
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, groundMask))
            {
                if (Input.GetButtonDown("Fire1") && attackCooldown <= 0f && isAttacking)
                {
                    animator.SetBool("isAttacking", true);
                    //Invoke("DelayedShootMagic", delayShoot);
                    ShootMagic(attackPattern[currentAttackIndex]);
                    attackCooldown = timeBetweenAttacks;
                    currentAttackIndex = (currentAttackIndex + 1) % 4;
                    ChangeActiveElement();
                    CheckElementalReaction();                   
                    // membuat playe melihat ke arah klik mouse
                    transform.forward = direction;
                }
            }
        }
    }

    //void DelayedShootMagic() {
    //    // Here you can call ShootMagic with the attack pattern.
    //    ShootMagic(attackPattern[currentAttackIndex]);
    //}

    private (bool success, Vector3 position) GetMousePosition()
{
    if (mainCamera == null)
    {
        // Handle the case where mainCamera is not yet available.
        return (success: false, position: Vector3.zero);
    }

    var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

    if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
    {
        // The Raycast hit something, return with the position.
        return (success: true, position: hitInfo.point);
    }
    else
    {
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
