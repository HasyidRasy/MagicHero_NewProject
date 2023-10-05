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
    [SerializeField] private Transform frontTrigger; // Maximum Z coordinate of the boundary
    [SerializeField] private Transform BackTrigger; // Maximum Z coordinate of the boundary
    [SerializeField] private float offsetCenterFront; // Maximum Z coordinate of the boundary
    [SerializeField] private float offsetCenterBack;
    private Vector3 _offset;
    private Vector3 _currentVelocity = Vector3.zero;

    [Header("Turn on/off visual")]
    [Tooltip("Min/Max Visual")]
    public bool enableFeature = true;

    private bool blankArea = false;

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
        Vector3 nextCM = frontTrigger.transform.position;
        Vector3 backCM = BackTrigger.transform.position;

        if (target.position.z < BackTrigger.transform.position.z)
        {
            centerMov.transform.position = new Vector3(
                centerMov.transform.position.x,
                centerMov.transform.position.y,
                backCM.z + offsetCenterBack
            );          
        }
        else if (targetPosition.z > nextCM.z && !blankArea)
        {
            // Update the Z position of centerMov by adding offsetCenter
            centerMov.transform.position = new Vector3(
                centerMov.transform.position.x,
                centerMov.transform.position.y,
                nextCM.z + offsetCenterFront
            );
            blankArea = true;
        }
        else if (target.position.z > BackTrigger.transform.position.z)
        {
            blankArea = false;
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
            frontTrigger.GetComponent<MeshRenderer>().enabled = false;
            centerMov.GetComponent<MeshRenderer>().enabled = false; 
            BackTrigger.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            minXPosition.GetComponent<MeshRenderer>().enabled = true;
            maxXPosition.GetComponent<MeshRenderer>().enabled = true;
            minZPosition.GetComponent<MeshRenderer>().enabled = true;
            maxZPosition.GetComponent<MeshRenderer>().enabled = true;
            frontTrigger.GetComponent <MeshRenderer>().enabled = true;
            centerMov.GetComponent<MeshRenderer>().enabled = true;
            BackTrigger.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a line from frontTrigger to the offset position
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(frontTrigger.transform.position, frontTrigger.transform.position + new Vector3(0f, 0f, offsetCenterFront));
        // Draw a line from BackTrigger to the offset position (red)
        Gizmos.color = Color.red;
        Gizmos.DrawLine(BackTrigger.transform.position, BackTrigger.transform.position + new Vector3(0f, 0f, offsetCenterBack));
    }
}
