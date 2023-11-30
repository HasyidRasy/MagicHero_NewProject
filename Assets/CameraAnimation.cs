using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera _camera;
    [SerializeField]private float duration;
    [SerializeField]private float from;
    [SerializeField]private AnimationCurve animationCurve;

    [SerializeField] private Image overlayImage; // Reference to the overlay image

    void Start()
    {
        NewPlayerController1.OnPlayerHurt += CameraShake;

        _camera = Camera.main;
        _camera.DOOrthoSize(20, duration)
               .SetEase(animationCurve)
               .From(from);
    }

    void OnDestroy() {
        NewPlayerController1.OnPlayerHurt -= CameraShake;
    }

    private void CameraShake() {
        _camera.DOShakePosition(0.3f, 1f);
        overlayImage.DOFade(1f, 0.3f)
                    .OnComplete(() => overlayImage.DOFade(0f, 0.3f));
    }
}
