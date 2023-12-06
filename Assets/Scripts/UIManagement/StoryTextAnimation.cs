using UnityEngine;
using TMPro;
using System.Collections; // Add this line

public class TextWriter : MonoBehaviour {
    public float delay = 0.1f; // Time delay between each character
    private TMP_Text textComponent;
    private string fullText;
    private string currentText = "";

    private void Start() {
        textComponent = GetComponent<TMP_Text>();

        if (textComponent == null) {
            Debug.LogError("TextMeshPro component not found on the GameObject.");
            return;
        }

        fullText = textComponent.text;
        textComponent.text = ""; // Clear text initially
        StartCoroutine(WriteText());
    }

    private IEnumerator WriteText() {
        if (textComponent == null) {
            Debug.LogError("TextMeshPro component is null. Make sure it's assigned.");
            yield break;
        }

        for (int i = 0; i <= fullText.Length; i++) {
            currentText = fullText.Substring(0, i);
            textComponent.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
