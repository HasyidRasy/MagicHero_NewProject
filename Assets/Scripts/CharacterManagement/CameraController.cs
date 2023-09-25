using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private Vector3 _offset;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    [SerializeField] private Transform boundary;
    private Vector3 _currentVelocity = Vector3.zero;

    private void Awake() {
        _offset = transform.position - target.position;
    }

    //Follow player logic
    private void LateUpdate() {
        Vector3 targetPosition = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);

        if (boundary != null) {
            targetPosition.x = Mathf.Clamp(targetPosition.x, boundary.position.x - boundary.localScale.x / 2, boundary.position.x + boundary.localScale.x / 2);
            targetPosition.z = Mathf.Clamp(targetPosition.z, boundary.position.z - boundary.localScale.z / 2, boundary.position.z + boundary.localScale.z / 2);
        }
    }
}