using UnityEngine;

public class Dummy_FollowPlayerCamera : MonoBehaviour
{
    public Transform target;     // Transform pemain yang akan diikuti
    public float smoothSpeed = 10f; // Kecepatan smoothing kamera

    private Vector3 offset; // Jarak antara kamera dan pemain

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        // Hitung posisi target yang diikuti oleh kamera
        Vector3 desiredPosition = target.position + offset;

        // Smoothly lerp kamera menuju posisi target
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
