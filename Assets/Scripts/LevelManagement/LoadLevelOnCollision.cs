using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class LoadLevelOnCollision : MonoBehaviour
{
    public string sceneIndexToLoad; // Change this to the desired scene index.
    public float delayBeforeLoad = 2.0f; // Delay in seconds before loading.
    public Renderer objectRenderer; // Reference to the object's renderer.

    private Color originalColor; // Store the original color of the object.
    private bool isFading = false; // Flag to track if the fading animation is in progress.
    private bool hasTriggered = false;

    public static Action OnTeleport;
    private void Start()
    {
        // Cache the original color of the object.
        originalColor = objectRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isFading && !hasTriggered)
            {
                //play sfx teleport
                NewAudioManager.Instance.bgmSource.Stop();
                NewAudioManager.Instance.PlaySFX("Teleport");
                //StartCoroutine(FadeAndLoadScene());
                OnTeleport?.Invoke();

                hasTriggered = true;
                GetComponent<Collider>().enabled = false;
            }
        }
    }

    IEnumerator FadeAndLoadScene()
    {
        isFading = true;

        float elapsedTime = 0f;
        Color currentColor = originalColor;
        Color targetColor = Color.black;

        while (elapsedTime < delayBeforeLoad)
        {
            // Calculate the lerped color over time.
            currentColor = Color.Lerp(originalColor, targetColor, elapsedTime / delayBeforeLoad);
            objectRenderer.material.color = currentColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final color is set to black.
        objectRenderer.material.color = targetColor;

        // Load the scene with the specified index.
        SceneManager.LoadScene(sceneIndexToLoad);
        //AsynLoader.Instance.TrigerLoadLevel(sceneIndexToLoad);
    }
}
