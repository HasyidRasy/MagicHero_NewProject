using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerElementalReaction : MonoBehaviour
{
    //get model
    private CharacterModel characterModel;
    //cek dash logic
    private bool isDashing = false;
    //rigidbody
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _model;

    [Header("Attack Pattern")]
    [SerializeField] private ElementalType[] elementalSlots = new ElementalType[4];
    private int currentSlotIndex = 0;

    private ElementalType[] attackPattern = new ElementalType[4];
    private int currentAttackIndex = 0;

    [Space(5)]
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject magicProjectilePrefab;

    [Header("Magic Speed")]
    [SerializeField] private float magicProSpeed = 10f;
    [Header("Casting Speed")]
    [SerializeField] private float timeBetweenAttacks = 0.5f;
    private float attackCooldown = 0f;

    [Header("Stats Stuff")]
    private float playerHp;
    private float playerDef;


    private void Awake()
    {
        characterModel = GetComponent<CharacterModel>();
    }
    private void Start()
    {
        PlayerStat();
    }

    private void Update()
    {
        CharaMove();
        PlayerStat();

        attackCooldown -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && attackCooldown <= 0f)
        {
            ShootMagic(attackPattern[currentAttackIndex]);          // Menembakkan sihir sesuai dengan pola serangan saat ini
            if (characterModel.attackSpeed > 0)
                attackCooldown = timeBetweenAttacks / (characterModel.attackSpeed);
            else
                attackCooldown = timeBetweenAttacks;
            currentAttackIndex = (currentAttackIndex + 1) % 4;      // Pindah ke elemen berikutnya dalam pola serangan
            ChangeActiveElement();

        }
    }

    private void CharaMove()
    {
        //input movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //move direction
        Vector3 moveDir = new Vector3(horizontal, 0f, vertical).normalized;

        _rb.MovePosition(transform.position + moveDir.ToIso() * moveDir.magnitude * characterModel.moveSpeed * Time.deltaTime);

        //New Look Logic (rotation)
        if (moveDir == Vector3.zero) return;
        Quaternion rotation = Quaternion.LookRotation(moveDir.ToIso(), Vector3.up);
        _model.rotation = Quaternion.RotateTowards(_model.rotation, rotation, characterModel.rotationSpeed * Time.deltaTime);

        //Call Coroutine Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f))
        {
            StartCoroutine(Dash());
        }
    }

    //Coroutine Dash Logic
    private IEnumerator Dash()
    {
        isDashing = true;
        float startTime = Time.time;
        Vector3 pos = transform.position;
        Vector3 dashDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        while (Time.time < startTime + characterModel.DashDuration)
        {
            //transform.position += characterModel.DashSpeed * Time.deltaTime * dashDir;
            //New Logic Dash
            _rb.MovePosition(transform.position + dashDir.ToIso() * dashDir.magnitude * characterModel.dashSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(characterModel.DashCooldown);
        isDashing = false;
    }

    private void PlayerStat()
    {
        playerHp = characterModel.HealthPoint;
        playerDef = characterModel.Defence;
    }

    public void TakeDamage(float damageAmount)
    {
        damageAmount -= playerDef;

        characterModel.HealthPoint -= damageAmount; // Reduce current health by the damage amount

        if (characterModel.HealthPoint <= 0)
        {
            Death(); // If health drops to or below zero, call a method to handle enemy death
        }
    }

    private void Death()
    {
        Destroy(gameObject);
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
            GameObject magic = Instantiate(magicProjectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

            // Implementasi logika menembakkan sihir sesuai elemen
            MagicProjectileElementalReaction magicProjectileElReact = magic.GetComponent<MagicProjectileElementalReaction>();
            if (magicProjectileElReact != null)
            {
                magicProjectileElReact.SetElement(element);
            }
            // Mengatur kecepatan proyektil sesuai arah target
            Rigidbody rb = magic.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = targetDirection * magicProSpeed;
            }
        }
    }

    private void ChangeActiveElement()
    {
        // Mengganti elemen aktif ke slot berikutnya
        currentSlotIndex = (currentSlotIndex + 1) % 4;
        // Memperbarui pola serangan berdasarkan elemen yang baru aktif
        attackPattern[currentAttackIndex] = elementalSlots[currentSlotIndex];
    }
}
