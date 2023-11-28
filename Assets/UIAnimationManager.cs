using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimationManager : MonoBehaviour {
    private UIManager manager;

    [Header("Start Transiton")]
    [SerializeField] private float duration;

    [Header("Player Panel Hurt Animation")]
    [SerializeField] private RectTransform playerStatusBar;
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeStrenght;

    [Header("Upgrade Animation")]
    [SerializeField] private RectTransform upgradeTitle;
    [SerializeField] private Image upgradeBg;
    [SerializeField] private RectTransform[] upgradeButton;
    [SerializeField] private CanvasGroup upgradeCanvasGroup;

    [Header("Death Panel Animation")]
    [SerializeField] private RectTransform leftDeathTitle;
    [SerializeField] private RectTransform rightDeathTitle;
    [SerializeField] private TextMeshProUGUI deathTitle;
    [SerializeField] private float transitionDuration;
    [SerializeField] private float transitionDuration2;
    [SerializeField] private Image vignette1;
    [SerializeField] private Image vignette2;
    [SerializeField] private CanvasGroup deathCanvasGroup;

    [Header("Attribute tab animation")]
    [SerializeField] private RectTransform attributeTab;
    [SerializeField] private RectTransform textChangeElement;
    [SerializeField] private Image backgroundAttribute;
    [SerializeField] private Image backgroundDownAttribute;
    [SerializeField] private float PopupTabDuration;




    private void OnEnable() {
        NewPlayerController1.OnPlayerHurt += HpBarShake;
        LoadLevelOnCollision.OnTeleport += FadeIn;
    }

    private void OnDisable() {
        NewPlayerController1.OnPlayerHurt -= HpBarShake;
        LoadLevelOnCollision.OnTeleport -= FadeIn;
    }
    void Start() {
        //TransitionDeathPanel();
        StartTransition();
        manager = GetComponent<UIManager>();

    }

    void StartTransition() {
        vignette2.DOFade(0f, duration)
                 .From(1f);
    }

    void Update() {
        RestartAnimation();
    }


    void RestartAnimation() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            //UpgradeUIAnimation();
            TransitionDeathPanel();
        }
    }

    [ContextMenu("HpBarShake")]
    public void HpBarShake() {
        playerStatusBar.DOShakeAnchorPos(shakeDuration, new Vector3(shakeStrenght, shakeStrenght, 0.0f), 10, 90);
    }
    [ContextMenu("UpgradeUIAnimation")]
    public void UpgradeUIAnimation() {
        //Debug.Log("UiUpgradeAnimation");
        float duration = 1f;
        float initialY = -Screen.height - 1000f;
        float targetY = -390f;

        upgradeBg.DOFade(1f, duration)
                 .From(0f)
                 .SetUpdate(true);

        // Loop through each upgradeButton element and animate its position
        for (int i = 0; i < upgradeButton.Length; i++) {
            float currentTargetY = targetY;

            // Punch Rotation animation
            upgradeButton[i].DOPunchRotation(new Vector3(0f, 90f, 0f), duration, 0, 1f)
                .SetEase(Ease.OutBack)
                .SetUpdate(true);

            // Move animation
            upgradeButton[i].DOAnchorPosY(currentTargetY, duration)
                .From(new Vector2(0f, initialY))
                .SetEase(Ease.OutQuad) // change the ease type
                .SetDelay(i * 0.2f)// adjust the delay between animations
                .SetUpdate(true);
        }

        upgradeTitle.DOAnchorPosY(0f, 1f)
            .From(new Vector2(0f, 500f))
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }

    public void FadeOutUIUpgrade() {
        upgradeCanvasGroup.DOFade(0f, 1f).
            OnComplete(() => {
                upgradeCanvasGroup.DOFade(1f, 0f);
            })
           ;
    }

    public void DeathPanelAnimation() {
        float duration = 1f;

        //Set first state
        deathCanvasGroup.DOFade(1f, 0f);
        vignette1.DOFade(0f, 0f);
        deathTitle.DOFade(0f, 0f);

        //Do animation
        vignette2.DOFade(0f, transitionDuration)
                 .From(1f);
        leftDeathTitle.DOAnchorPosX(480f, duration)
            .From(new Vector2(1100f, -200f))
            .SetEase(Ease.InOutBack);
        rightDeathTitle.DOAnchorPosX(-480f, duration)
            .From(new Vector2(-1100f, -200f))
            .SetEase(Ease.InOutBack)
            .OnComplete(() => {
                deathTitle.DOFade(1f, duration)
                            .From(0f);
            });
    }
    [ContextMenu("DeathPanelTransition")]
    public void TransitionDeathPanel() {
        deathCanvasGroup.DOFade(0f, 0f);

        vignette1.DOFade(1f, transitionDuration)
                 .From(0f)
                 .OnComplete(() => {
                     vignette2.DOFade(1f, transitionDuration2)
                               .From(0f).OnComplete(() => {
                                   DeathPanelAnimation();
                               });
                 });
    }

    public void FadeIn() {
    vignette2.DOFade(1f, duration)
                    .From(0f);
    }

    public void PopupAttribute() {
        backgroundAttribute.DOFade(1f, PopupTabDuration)
                           .From(0f)
                           .SetUpdate(true);

        backgroundDownAttribute.DOFade(1f, PopupTabDuration + 0.5f)
                           .From(0f)
                           .SetUpdate(true);

        textChangeElement.DOAnchorPosX(-50f, PopupTabDuration)
                         .From(new Vector2(920f, -430f))
                         .SetEase(Ease.OutSine)
                         .SetUpdate(true);

        attributeTab.DOAnchorPosY(0f, PopupTabDuration)
                    .From(new Vector2(0, -1100f))
                    .SetEase(Ease.OutSine)
                    .SetUpdate(true);
    }

    public void DepopupAttribute() {
        backgroundAttribute.DOFade(0f, PopupTabDuration)
                           .From(1f)
                           .SetUpdate(true);

        backgroundDownAttribute.DOFade(0f, PopupTabDuration + 0.5f)
                           .From(1f)
                           .SetUpdate(true);

        textChangeElement.DOAnchorPosX(920f, PopupTabDuration)
                         .From(new Vector2(-50f, -430f))
                         .SetEase(Ease.OutSine)
                         .SetUpdate(true);

        attributeTab.DOAnchorPosY(-1100f, PopupTabDuration)
                    .From(new Vector2(0, 0))
                    .SetEase(Ease.OutSine)
                    .SetUpdate(true).OnComplete(() => {
                        manager.DisableSwitchElementPanel();
                    });
    }

}
