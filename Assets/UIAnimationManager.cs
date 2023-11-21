using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimationManager : MonoBehaviour {
    [Header("Upgrade Animation")]
    [SerializeField] private RectTransform upgradeTitle;
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


    void Start() {
        //TransitionDeathPanel();
        

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
    public void UpgradeUIAnimation() {
        //Debug.Log("UiUpgradeAnimation");
        float duration = 1f;
        float initialY = -Screen.height - 1000f;
        float targetY = -390f;

        // Loop through each upgradeButton element and animate its position
        for (int i = 0; i < upgradeButton.Length; i++) {
            float currentTargetY = targetY;

            // Punch Rotation animation
            upgradeButton[i].DOPunchRotation(new Vector3(0f, 90f, 0f), duration, 0, 1f)
                .SetEase(Ease.OutBack);
                //.SetUpdate(true);

            // Move animation
            upgradeButton[i].DOAnchorPosY(currentTargetY, duration)
                .From(new Vector2(0f, initialY))
                .SetEase(Ease.OutQuad) // change the ease type
                .SetDelay(i * 0.2f); // adjust the delay between animations
                //.SetUpdate(true);
        }

        upgradeTitle.DOAnchorPosY(0f, 1f)
            .From(new Vector2(0f, 500f))
            .SetEase(Ease.OutBack);
            //.SetUpdate(true);
    }

    public void FadeOutUIUpgrade() {
        upgradeCanvasGroup.DOFade(0f, 0.5f).
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
}
