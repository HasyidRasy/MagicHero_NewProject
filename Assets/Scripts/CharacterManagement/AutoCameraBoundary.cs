using UnityEngine;

public class AutoCameraBoundary : MonoBehaviour
{
    [Header("Player Follow")]
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    [SerializeField] private GameObject boundaryName;

    private Vector3 _offset;
    private Vector3 _currentVelocity = Vector3.zero;

    [Header("Turn on/off visual")]
    [Tooltip("Min/Max Visual")]
    public bool enableFeature = true;

    private Transform centerMov; // Maximum Z coordinate of the boundary
    private Transform minXPosition; // Minimum X coordinate of the boundary
    private Transform maxXPosition; // Maximum X coordinate of the boundary
    private Transform minZPosition; // Minimum Z coordinate of the boundary
    private Transform maxZPosition; // Maximum Z coordinate of the boundary
    private Transform frontTrigger; // Maximum front coordinate of the boundary
    private Transform backTrigger; // Maximum back coordinate of the boundary

    [SerializeField] private float offsetCenterFront; // Offset for frontTrigger relative to centerMov
    [SerializeField] private float offsetCenterBack;  // Offset for backTrigger relative to centerMov

    private bool blankArea = false;

    private void Start() {
        //play audio
        NewAudioManager.Instance.PlayBGM("Safezone");
    }

    private void Awake()
    {
        _offset = transform.position - target.position;

        // Find the centerMov GameObject based on the specified name
        GameObject centerMovObject = boundaryName;

        if (centerMovObject)
        {
            // Set centerMov to the found GameObject's transform
            centerMov = centerMovObject.transform;

            // Find child objects based on their names within centerMov
            minXPosition = centerMov.Find("minX");
            maxXPosition = centerMov.Find("maxX");
            minZPosition = centerMov.Find("minZ");
            maxZPosition = centerMov.Find("maxZ");
            frontTrigger = centerMov.Find("FrontTrigger");
            backTrigger = centerMov.Find("BackTrigger");
        }
        else
        {
            Debug.LogError("GameObject with name '" + boundaryName + "' not found.");
        }
    }

    private void LateUpdate()
    {
        CameraMovement();
        TurnOffVisual();
    }

    private void CameraMovement()
    {
        if (target == null || backTrigger == null || frontTrigger == null || centerMov == null ||
        minXPosition == null || maxXPosition == null || minZPosition == null || maxZPosition == null)
        {
            // Handle null references here, or return early if necessary
            Debug.Log("Game object null");
            return;
        }

        Vector3 targetPosition = target.position;

        if (target.position.z < backTrigger.position.z)
        {
            // Use the offsetCenterBack to set the position of centerMov
            centerMov.position = new Vector3(
                centerMov.position.x,
                centerMov.position.y,
                centerMov.position.z + offsetCenterBack
            );
        }
        else if (targetPosition.z > frontTrigger.position.z && !blankArea)
        {
            // Use the offsetCenterFront to set the position of centerMov
            centerMov.position = new Vector3(
                centerMov.position.x,
                centerMov.position.y,
                centerMov.position.z + offsetCenterFront
            );
            blankArea = true;
        }
        else if (target.position.z > backTrigger.position.z)
        {
            blankArea = false;
        }

        // Clamp camera position based on the specified boundary coordinates
        targetPosition.x = Mathf.Clamp(targetPosition.x, minXPosition.position.x, maxXPosition.position.x);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minZPosition.position.z, maxZPosition.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition + _offset, ref _currentVelocity, smoothTime);
    }

    private void TurnOffVisual()
    {
        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            renderer.enabled = enableFeature;
        }
    }
}
