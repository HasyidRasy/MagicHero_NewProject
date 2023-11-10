using UnityEngine;

public class PlayerSpawnReset : MonoBehaviour {
    [SerializeField] private Vector3 defaultPosition;
    [SerializeField] private Camera cam;
    private Transform playerTransform;
    private NewPlayerController1 playerController;

    private void Awake() {
        // Find the player object and get its components
        playerTransform = GameObject.FindWithTag("Player").transform;
        // Reset the player's position and healthpt 
        playerTransform.position = defaultPosition;
    }

    private void Start() {
        playerController = FindAnyObjectByType<NewPlayerController1>();
        cam = Camera.main;
        //if (playerController.mainCamera == null) {
        //    playerController.GetCamera(cam);
        //    Debug.Log("A key pressed. Camera: " + (cam != null ? cam.name : "null"));
        //} else {
        //    Debug.Log("Xamera not assigned");
        //}

    }

    private void Update() {
        //if (Input.GetKeyDown(KeyCode.A)) {
        //    playerController.GetCamera(cam);
        //    Debug.Log("A key pressed. Camera: " + (cam != null ? cam.name : "null"));
        //}
    }


}
