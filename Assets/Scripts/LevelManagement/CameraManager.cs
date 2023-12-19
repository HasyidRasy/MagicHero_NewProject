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
        mainCamera = Camera.main;
    }

    public Camera GetMainCamera() {
        return mainCamera;
    }

    private void Awake() {
        DontDestroyOnLoad(gameObject);

        mainCamera = Camera.main;
    }
}
