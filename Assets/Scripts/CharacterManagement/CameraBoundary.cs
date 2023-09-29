using UnityEngine;

public class CameraBoundary : MonoBehaviour
{
    [Header("Player Follow")]
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;

    [Header("Minimal X and maximal X position")]
    [SerializeField] private Transform minXPosition; // Minimum X coordinate of the boundary
    [SerializeField] private Transform maxXPosition; // Maximum X coordinate of the boundary

    [Header("Minimal Z and maximal Z position")]
    [SerializeField] private Transform minZPosition; // Minimum Z coordinate of the boundary
    [SerializeField] private Transform maxZPosition; // Maximum Z coordinate of the boundary

    [Header("Camera center and Next camera center")]
    [SerializeField] private Transform centerMov; // Maximum Z coordinate of the boundary
    [SerializeField] private Transform nextCenterMov; // Maximum Z coordinate of the boundary
    [SerializeField] private float offsetCenter; // Maximum Z coordinate of the boundary

    private Vector3 _offset;
    private Vector3 _currentVelocity = Vector3.zero;

    [Header("Turn on/off visual")]
    [Tooltip("Min/Max Visual")]
    public bool enableFeature = true;

    private void Awake()
    {
        _offset = transform.position - target.position;       
    }

    private void LateUpdate()
    {
        CameraMovement();
        TurnOffVisual();
    }

    private void CameraMovement()
    {
        Vector3 targetPosition = target.position;
        float minX = minXPosition.transform.position.x;
        float maxX = maxXPosition.transform.position.x;
        float minZ = minZPosition.transform.position.z;
        float maxZ = maxZPosition.transform.position.z;
        Vector3 nextCM = nextCenterMov.transform.position;

        // Check if the Z position of target is greater than the Z position of nextCM
        if (targetPosition.z > nextCM.z)
        {
            // Update the Z position of centerMov by adding offsetCenter
            centerMov.transform.position = new Vector3(
                centerMov.transform.position.x,
                centerMov.transform.position.y,
                nextCM.z + offsetCenter
            );
        }

        // Clamp camera position based on the specified boundary coordinates
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minZ, maxZ);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition + _offset, ref _currentVelocity, smoothTime);
    }

    private void TurnOffVisual()
    {
        if (enableFeature == false)
        {
            minXPosition.GetComponent<MeshRenderer>().enabled = false;
            maxXPosition.GetComponent<MeshRenderer>().enabled = false;
            minZPosition.GetComponent<MeshRenderer>().enabled = false;
            maxZPosition.GetComponent<MeshRenderer>().enabled = false;
            nextCenterMov.GetComponent<MeshRenderer>().enabled = false;
            centerMov.GetComponent<MeshRenderer>().enabled = false; 
        }
        else
        {
            minXPosition.GetComponent<MeshRenderer>().enabled = true;
            maxXPosition.GetComponent<MeshRenderer>().enabled = true;
            minZPosition.GetComponent<MeshRenderer>().enabled = true;
            maxZPosition.GetComponent<MeshRenderer>().enabled = true;
            nextCenterMov.GetComponent <MeshRenderer>().enabled = true;
            centerMov.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a line from nextCenterMov to the offset position
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(nextCenterMov.transform.position, nextCenterMov.transform.position + new Vector3(0f, 0f, offsetCenter));
    }
}
