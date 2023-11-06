using UnityEngine;

public class PlayerSpawnReset : MonoBehaviour {
    [SerializeField] private Vector3 defaultPosition;
    private Transform playerTransform;

    private void Awake() {
        // Find the player object and get its components
        playerTransform = GameObject.FindWithTag("Player").transform;
        // Reset the player's position and health
        playerTransform.position = defaultPosition;
    }
}
