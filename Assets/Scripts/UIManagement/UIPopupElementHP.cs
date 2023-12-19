using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIPopupElementHP : MonoBehaviour
{
    public GameObject elementalPopupPrefab;
    private GameObject elementalPopupInstance;
    private Camera mainCamera;
    private Vector3 offset = new Vector3(0, 6, 0); //Adjust Popup UI Position above enemy
    private Vector3 scale = new Vector3(0.01f, 0.01f, 0.01f); //Adjust Popup UI Scale
    private Vector3 elementScale = new Vector3(0.5f, 0.5f, 0.5f); // Change element UI Scale

    //UI Element
    //First PopupUI
    private GameObject firstElementPopupUI;
    private Image firstImagePopupUI;
    private Vector3 firstOffset = new Vector3(1.0f, -0.75f, 0);

    //Secondd PopupUI
    private GameObject secondElementPopupUI;
    private Image secondImagePopupUI;
    private Vector3 secondOffset = new Vector3(-1.0f, -0.75f, 0);

    //Middle PopupUI
    private GameObject middleElementPopupUI;
    private Image middleImagePopupUI;
    private Vector3 middleOffset = new Vector3(0, -0.75f, 0);

    //Reaction PopupUI
    private GameObject reactionPopupUI;
    private Image reactionImageUI;

    //HealthBar UI Popup
    private GameObject healtBarPopupUI;
    private Slider healtBarSlider;
    private Vector3 healthBarOffset = new Vector3(0, -2, 0);

    //Fading Healthbar
    private Slider fadeHealthSlider;
    private float lerpSpeed = 0.05f; //HP fade animation duration

    private float durationPopupUI = 3f; //Element PopupUI Duration

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
        middleElementPopupUI.transform.position += middleOffset;
        middleElementPopupUI.transform.localScale -= elementScale;

        //Reaction PopupUI
        Transform childTransformReaction = elementalPopupInstance.transform.GetChild(3);
        reactionPopupUI = childTransformReaction.gameObject;
        reactionImageUI = reactionPopupUI.GetComponent<Image>();
        reactionPopupUI.transform.position += middleOffset;
        reactionPopupUI.transform.localScale -= elementScale;

        //HealthBar UI
        Transform healtTransformMiddle = elementalPopupInstance.transform.GetChild(4);
        healtBarPopupUI = healtTransformMiddle.gameObject;
        healtBarSlider = healtBarPopupUI.GetComponent<Slider>();
        healtBarPopupUI.transform.position += healthBarOffset;
        //Fading Healthbar
        fadeHealthSlider = healtBarPopupUI.transform.GetChild(0).gameObject.GetComponent<Slider>();

        //Hide PopupUI
        secondElementPopupUI.SetActive(false);
        firstElementPopupUI.SetActive(false);
        middleElementPopupUI.SetActive(false);
        reactionPopupUI.SetActive(false);
        healtBarPopupUI.SetActive(false);
    }


    private void Update()
    {
        // Popup UI camera follow
        if (elementalPopupInstance)
        {
            elementalPopupInstance.transform.rotation = mainCamera.transform.rotation;
        }

        // Fading Health when damaged
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

        middleImagePopupUI.rectTransform.DOScale(0.5f, 0.5f)
                                        .SetEase(Ease.InOutBack)
                                        .From(0f);

        middleImagePopupUI.rectTransform.DOScale(0f, 0.5f)
                                        .SetEase(Ease.InOutBack)
                                        .SetDelay(durationPopupUI - 0.6f);
    }

    //Show two elemental reaction Popup
    public void ShowReactionPopupUI(Sprite firstElementSprite, Sprite secondElementSprite)
    {
        firstElementPopupUI.SetActive(true);
        secondElementPopupUI.SetActive(true);
        firstImagePopupUI.sprite = firstElementSprite;
        secondImagePopupUI.sprite = secondElementSprite;

        //Popup Animation

        firstImagePopupUI.rectTransform.anchoredPosition = new Vector2(0f, firstImagePopupUI.rectTransform.anchoredPosition.y);
        firstImagePopupUI.rectTransform.DOAnchorPosX(33f, 0.5f)
                                       .SetEase(Ease.InOutBack);

        firstImagePopupUI.rectTransform.DOScale(0.5f,0f);
        secondImagePopupUI.rectTransform.DOScale(0.5f, 0.5f)
                                        .SetEase(Ease.InOutBack)
                                        .From(0f);

        secondImagePopupUI.rectTransform.anchoredPosition = new Vector2(0f, secondImagePopupUI.rectTransform.anchoredPosition.y);
        secondImagePopupUI.rectTransform.DOAnchorPosX(-33f, 0.5f)
                                        .SetEase(Ease.InOutBack);

        //DepopupAnimation
        secondImagePopupUI.rectTransform.DOScale(0f, 0.5f)
                                        .SetEase(Ease.InOutBack)
                                        .SetDelay(1.3f);

        firstImagePopupUI.rectTransform.DOScale(0f, 0.5f)
                                        .SetEase(Ease.InOutBack)
                                        .SetDelay(1.5f);
    }

    public void ShowReactionUI(Sprite elementalSprite, float reactDuration)
    {
        reactionPopupUI.SetActive(true);
        reactionImageUI.sprite = elementalSprite;

        reactionImageUI.rectTransform.DOScale(0.5f, 0.5f)
                                        .SetEase(Ease.InOutBack)
                                        .From(0f);

        reactionImageUI.rectTransform.DOScale(0f, 0.5f)
                                        .SetEase(Ease.InOutBack)
                                        .SetDelay(reactDuration - 0.6f);
    }

    public IEnumerator ElementPopupDuration()
    {
        yield return new WaitForSeconds(durationPopupUI);
        ResetPopupUI();
        EnemyController enemyController = GetComponent<EnemyController>();
        enemyController.ResetElementalStatus();
    }

    public IEnumerator ResetReactionPopupUI()
    {
        yield return new WaitForSeconds(2f);
        ResetPopupUI();
    }

    public void ResetPopupUI()
    {
        secondElementPopupUI.SetActive(false);
        firstElementPopupUI.SetActive(false);
        middleElementPopupUI.SetActive(false);
    }

    public void ResetReactionUI()
    {
        reactionPopupUI.SetActive(false);
    }

    public void ResetHealthUI()
    {
        healtBarPopupUI.SetActive(false);
    }
}