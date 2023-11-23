using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera _camera;
    [SerializeField]private float duration;
    [SerializeField]private float from;
    [SerializeField]private AnimationCurve animationCurve;
    void Start()
    {
        _camera = Camera.main;
        _camera.DOOrthoSize(20, duration)
               .SetEase(animationCurve)
               .From(from);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
