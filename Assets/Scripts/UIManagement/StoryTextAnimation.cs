using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class TextWriter : MonoBehaviour {
    public float delay = 0.1f; // Time delay between each character
    public float delaySfx = 0.1f; // Time delay between each character
    private TMP_Text textComponent;
    private string fullText;
    private string currentText = "";
    private bool isTyping = true;

    private void Start() {
        textComponent = GetComponent<TMP_Text>();

        if (textComponent == null) {
            Debug.LogError("TextMeshPro component not found on the GameObject.");
            return;
        }

        fullText = textComponent.text;
        textComponent.text = ""; 

        StartCoroutine(WriteText());
        StartCoroutine(WriteTextSound());
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
        isTyping = false;
    }

    private IEnumerator WriteTextSound() {
        while (isTyping) {
            NewAudioManager.Instance.PlaySFX("Type");
            yield return new WaitForSeconds(delaySfx);
        }
    }
}
