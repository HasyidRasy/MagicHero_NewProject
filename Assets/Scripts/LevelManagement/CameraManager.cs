using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour {
    [SerializeField] private Camera mainCamera;

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        // Assign the mainCamera reference when a new scene is loaded
        mainCamera = Camera.main;
    }

    // Provide a method to retrieve the main camera reference
    public Camera GetMainCamera() {
        return mainCamera;
    }

    private void Awake() {
        // Ensure the script persists across scenes
        DontDestroyOnLoad(gameObject);

        // Assign the mainCamera reference when the script is first created
        mainCamera = Camera.main;
    }
}
