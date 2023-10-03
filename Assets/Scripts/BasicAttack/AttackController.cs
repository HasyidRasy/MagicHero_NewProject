using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementalType
{
    Water,
    Fire,
    Wind
}
public class AttackController : MonoBehaviour
{
    [Header("Attack Pattern")]
    [SerializeField] private ElementalType[] elementalSlots = new ElementalType[4]; // Set Attack Pattern
    private int currentSlotIndex = 0;

    private ElementalType[] attackPattern = new ElementalType[4];
    private int currentAttackIndex = 0;

    [Space(5)]
    [SerializeField] private Transform projectileSpawnPoint;    // Titik spawn proyektil
    [SerializeField] private GameObject magicProjectilePrefab;  // Prefab untuk sihir

    [Header("Magic Speed")]
    [SerializeField] private float magicProSpeed = 10f;         // Kecepatan proyektil

    [Header("Casting Speed")]
    [SerializeField] private float timeBetweenAttacks = 0.5f;   // Waktu antara serangan
    private float attackCooldown = 0f;

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && attackCooldown <= 0f)
        {
            ShootMagic(attackPattern[currentAttackIndex]);          // Menembakkan sihir sesuai dengan pola serangan saat ini
            attackCooldown = timeBetweenAttacks;
            currentAttackIndex = (currentAttackIndex + 1) % 4;      // Pindah ke elemen berikutnya dalam pola serangan
            ChangeActiveElement();
        }
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
            MagicProjectile magicProjectile = magic.GetComponent<MagicProjectile>();
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
