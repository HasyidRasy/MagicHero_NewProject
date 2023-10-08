using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;      // Kecepatan gerakan pemain
    public float rotationSpeed = 10f; // Kecepatan rotasi pemain
    public float dashSpeed = 10f;    // Kecepatan dash pemain
    public float dashDuration = 0.2f; // Durasi dash dalam detik
    public float dashCooldown = 1f;  // Waktu cooldown antara dash
    public KeyCode dashKey = KeyCode.Space; // Tombol untuk melakukan dash

    private Rigidbody rb;
    private Transform playerTransform;
    private bool isDashing = false;
    private float dashCooldownTimer = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = transform;
    }

    private void Update()
    {
        // Membaca input pemain
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Menghitung vektor gerakan
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Menggerakkan pemain
        if (!isDashing)
        {
            MovePlayer(moveDirection);

            // Mengaktifkan dash saat tombol "dashKey" ditekan dan cooldown sudah berakhir
            if (Input.GetKeyDown(dashKey) && dashCooldownTimer <= 0f)
            {
                StartCoroutine(Dash());
            }
        }

        // Menghitung cooldown dash
        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    void MovePlayer(Vector3 moveDirection)
    {
        // Menggerakkan pemain dengan Rigidbody
        rb.velocity = moveDirection * (isDashing ? dashSpeed : moveSpeed);

        // Rotasi karakter sesuai arah gerakan
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator Dash()
    {
        // Aktifkan status dash
        isDashing = true;

        // Ubah kecepatan pemain selama dash
        rb.velocity = playerTransform.forward * dashSpeed;

        // Tunggu durasi dash
        yield return new WaitForSeconds(dashDuration);

        // Kembalikan kecepatan pemain ke normal setelah dash
        rb.velocity = Vector3.zero;

        // Matikan status dash
        isDashing = false;

        // Mulai cooldown dash
        dashCooldownTimer = dashCooldown;
    }
}
