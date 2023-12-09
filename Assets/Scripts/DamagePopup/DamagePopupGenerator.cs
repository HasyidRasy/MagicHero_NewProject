using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopupGenerator : MonoBehaviour
{
    public static DamagePopupGenerator current;
    public GameObject prefabDamagePopup;
    private GameObject popup;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;

        current = this;
    }

    private void Update()
    {
        // Popup UI camera follow
        if (popup)
        {
            popup.transform.rotation = mainCamera.transform.rotation;
        }
          
    }

    public void CreatePopup(Vector3 position, string text, Color color)
    {
        popup = Instantiate(prefabDamagePopup, position, Quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        temp.text = text;
        temp.faceColor = color;

        // Destroy timer
        Destroy(popup, 1f);
    }
}
