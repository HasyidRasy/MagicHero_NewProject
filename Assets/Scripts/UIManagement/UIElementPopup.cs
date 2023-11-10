using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElementPopup : MonoBehaviour
{
    public GameObject elementalPopupPrefab;
    private GameObject elementalPopupInstance;
    private Camera mainCamera;
    private Vector3 offset = new Vector3(0, 6, 0);

    //UI Element
    //First PopupUI
    private GameObject firstElementPopupUI;
    private Image firstImagePopupUI;
    private Vector3 firstOffset = new Vector3(1.5f, 0, 0);

    //Secondd PopupUI
    private GameObject secondElementPopupUI;
    private Image secondImagePopupUI;
    private Vector3 secondOffset = new Vector3(-1.5f, 0, 0);

    //Middle PopupUI
    private GameObject middleElementPopupUI;
    private Image middleImagePopupUI;

    //HealthBar UI Popup
    public GameObject healtBarPopupUI;
    private Slider healtBarSlider;
    private Vector3 healthBarOffset = new Vector3(0, -2, 0);

    private float durationPopupUI = 3f;

    private void Awake()
    {
        mainCamera = Camera.main;

        elementalPopupInstance = Instantiate(elementalPopupPrefab, transform.position, Quaternion.identity, transform);
        elementalPopupInstance.transform.position += offset;
        
        //First Element
        Transform childTransformFirst = elementalPopupInstance.transform.GetChild(0);
        firstElementPopupUI = childTransformFirst.gameObject;
        firstImagePopupUI = firstElementPopupUI.GetComponent<Image>();
        firstElementPopupUI.transform.position += firstOffset;

        //Second Element
        Transform childTransformSecond = elementalPopupInstance.transform.GetChild(1);
        secondElementPopupUI = childTransformSecond.gameObject;
        secondImagePopupUI = secondElementPopupUI.GetComponent<Image>();
        secondElementPopupUI.transform.position += secondOffset;

        //Middle Element
        Transform childTransformMiddle = elementalPopupInstance.transform.GetChild(2);
        middleElementPopupUI = childTransformMiddle.gameObject;
        middleImagePopupUI = middleElementPopupUI.GetComponent<Image>();

        //HealthBar UI
        Transform healtTransformMiddle = elementalPopupInstance.transform.GetChild(3);
        healtBarPopupUI = healtTransformMiddle.gameObject;
        healtBarSlider = healtBarPopupUI.GetComponent<Slider>();
        healtBarPopupUI.transform.position += healthBarOffset;

        secondElementPopupUI.SetActive(false);
        firstElementPopupUI.SetActive(false);
        middleElementPopupUI.SetActive(false);
        healtBarPopupUI.SetActive(false);
    }


    private void Update()
    {
        if (elementalPopupInstance)
        {
            elementalPopupInstance.transform.rotation = mainCamera.transform.rotation;
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
        EnemyControllerElReactio enemyControllerElReact = GetComponent<EnemyControllerElReactio>();
        enemyControllerElReact.ResetElementalStatus();
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
