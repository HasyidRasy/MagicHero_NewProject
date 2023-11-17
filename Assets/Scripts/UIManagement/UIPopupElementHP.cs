using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupElementHP : MonoBehaviour
{
    public GameObject elementalPopupPrefab;
    private GameObject elementalPopupInstance;
    private Camera mainCamera;
    private Vector3 offset = new Vector3(0, 6, 0);
    private Vector3 scale = new Vector3(0.01f, 0.01f, 0.01f); //Adjust Popup UI Scale
    private Vector3 elementScale = new Vector3(0.5f, 0.5f, 0.5f); // Change element UI Scale

    //UI Element
    //First PopupUI
    private GameObject firstElementPopupUI;
    private Image firstImagePopupUI;
    private Vector3 firstOffset = new Vector3(1.0f, 0, 0);

    //Secondd PopupUI
    private GameObject secondElementPopupUI;
    private Image secondImagePopupUI;
    private Vector3 secondOffset = new Vector3(-1.0f, 0, 0);

    //Middle PopupUI
    private GameObject middleElementPopupUI;
    private Image middleImagePopupUI;

    //HealthBar UI Popup
    private GameObject healtBarPopupUI;
    private Slider healtBarSlider;
    private Vector3 healthBarOffset = new Vector3(0, -2, 0);

    //Fading Healthbar
    private Slider fadeHealthSlider;
    private float lerpSpeed = 0.05f;

    private float durationPopupUI = 3f;

    private void Awake()
    {
        mainCamera = Camera.main;

        elementalPopupInstance = Instantiate(elementalPopupPrefab, transform.position, Quaternion.identity, transform);
        elementalPopupInstance.transform.position += offset;
        elementalPopupInstance.transform.localScale = scale;

        //First Element
        Transform childTransformFirst = elementalPopupInstance.transform.GetChild(0);
        firstElementPopupUI = childTransformFirst.gameObject;
        firstImagePopupUI = firstElementPopupUI.GetComponent<Image>();
        firstElementPopupUI.transform.position += firstOffset;
        firstElementPopupUI.transform.localScale -= elementScale;

        //Second Element
        Transform childTransformSecond = elementalPopupInstance.transform.GetChild(1);
        secondElementPopupUI = childTransformSecond.gameObject;
        secondImagePopupUI = secondElementPopupUI.GetComponent<Image>();
        secondElementPopupUI.transform.position += secondOffset;
        secondElementPopupUI.transform.localScale -= elementScale;

        //Middle Element
        Transform childTransformMiddle = elementalPopupInstance.transform.GetChild(2);
        middleElementPopupUI = childTransformMiddle.gameObject;
        middleImagePopupUI = middleElementPopupUI.GetComponent<Image>();
        middleElementPopupUI.transform.localScale -= elementScale;

        //HealthBar UI
        Transform healtTransformMiddle = elementalPopupInstance.transform.GetChild(3);
        healtBarPopupUI = healtTransformMiddle.gameObject;
        healtBarSlider = healtBarPopupUI.GetComponent<Slider>();
        healtBarPopupUI.transform.position += healthBarOffset;
        //Fading Healthbar
        fadeHealthSlider = healtBarPopupUI.transform.GetChild(0).gameObject.GetComponent<Slider>();

        secondElementPopupUI.SetActive(false);
        firstElementPopupUI.SetActive(false);
        middleElementPopupUI.SetActive(false);
        healtBarPopupUI.SetActive(false);
        Debug.Log(fadeHealthSlider.value);
    }


    private void Update()
    {
        if (elementalPopupInstance)
        {
            elementalPopupInstance.transform.rotation = mainCamera.transform.rotation;
        }

        if (healtBarSlider.value != fadeHealthSlider.value)
        {
            fadeHealthSlider.value = Mathf.Lerp(fadeHealthSlider.value, healtBarSlider.value, lerpSpeed);
        }
    }

    //Show HealthBar Popup
    public void ShowUpdateHealthBarUI(float currentHP, float maxHP)
    {
        healtBarPopupUI.SetActive(true);
        healtBarSlider.value = currentHP / maxHP;
    }

    //Show one elemental Popup
    public void ShowElementalPopup(Sprite elementalSprite)
    {
        middleElementPopupUI.SetActive(true);
        middleImagePopupUI.sprite = elementalSprite;
    }

    //Show two elemental reaction Popup
    public void ShowReactionPopupUI(Sprite firstElementSprite, Sprite secondElementSprite)
    {
        firstElementPopupUI.SetActive(true);
        secondElementPopupUI.SetActive(true);
        firstImagePopupUI.sprite = firstElementSprite;
        secondImagePopupUI.sprite = secondElementSprite;
    }

    //Applied element duration
    public IEnumerator ElementPopupDuration()
    {
        yield return new WaitForSeconds(durationPopupUI);
        ResetPopupUI();
        EnemyController enemyController = GetComponent<EnemyController>();
        enemyController.ResetElementalStatus();
    }

    //Reaction Popup Duration
    public IEnumerator ResetReactionPopupUI()
    {
        yield return new WaitForSeconds(1f);
        ResetPopupUI();
    }

    //Hide Popup UI
    public void ResetPopupUI()
    {
        secondElementPopupUI.SetActive(false);
        firstElementPopupUI.SetActive(false);
        middleElementPopupUI.SetActive(false);
    }
}