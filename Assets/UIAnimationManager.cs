using DG.Tweening;
using UnityEngine;

public class UIAnimationManager : MonoBehaviour {
    [Header("Upgrade Animation")]
    [SerializeField] private RectTransform upgradeTitle;
    [SerializeField] private RectTransform[] upgradeButton;
    [SerializeField] private CanvasGroup upgradeCanvasGroup;
    void Start() {
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
                .SetEase(Ease.OutBack)
                .SetUpdate(true);

            // Move animation
            upgradeButton[i].DOAnchorPosY(currentTargetY, duration)
                .From(new Vector2(0f, initialY))
                .SetEase(Ease.OutQuad) // change the ease type
                .SetDelay(i * 0.2f) // adjust the delay between animations
                .SetUpdate(true);
        }

        upgradeTitle.DOAnchorPosY(0f, 1f)
            .From(new Vector2(0f, 500f))
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }

    public void FadeOutUIUpgrade() {
        upgradeCanvasGroup.DOFade(0f, 0.5f).
            OnComplete(() => {
                upgradeCanvasGroup.DOFade(1f, 0f);
            })
           ;
    }
}
