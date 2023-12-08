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

          // Tes Damage Popup
        if(Input.GetKeyDown(KeyCode.F10))
        {
            Vector3 randomness = new Vector3(Random.Range(0f, 0.25f), Random.Range(0f, 0.25f), Random.Range(0f, 0.25f));
            CreatePopup(Vector3.one + randomness, Random.Range(0, 1000).ToString(), new Color32(0xFF, 0xBA, 0x06, 0xFF));
        } 
          
    }

    public void CreatePopup(Vector3 position, string text, Color color)
    {
        popup = Instantiate(prefabDamagePopup, position, Quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        temp.text = text;
        temp.faceColor = color;

        //Debug.Log(position);

        // Destroy timer
        Destroy(popup, 1f);
    }
}
