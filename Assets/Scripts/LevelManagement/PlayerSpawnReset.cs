using UnityEngine;

public class PlayerSpawnReset : MonoBehaviour {
    [SerializeField] private Vector3 defaultPosition;
    [SerializeField] private Camera cam;
    private Transform playerTransform;
    private NewPlayerController1 playerController;

    private void Awake() {
        playerTransform = GameObject.FindWithTag("Player").transform;
        playerTransform.position = defaultPosition;
    }

    private void Start() {
        playerController = FindAnyObjectByType<NewPlayerController1>();
        cam = Camera.main;
    }
}
