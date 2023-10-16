using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private Vector3 _offset;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 _currentVelocity = Vector3.zero;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioEvent audioEvent;
    [SerializeField] private string _audioCategory;

    private void Start() {
        //subs audioEvent
        audioEvent.onBattleStart += OnBattle;
        //audioManager.PlayBgmBattle();
        audioEvent.BattleStart(_audioCategory);
        audioEvent.MainMenu(_audioCategory);
    }

    private void Awake() {
        _offset = transform.position - target.position;
    }

    //Follow player logic
    private void LateUpdate() {
        Vector3 targetPosition = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
    }

    private void OnBattle(string audioCategory) {
        //play audio
        if (audioCategory == _audioCategory) {
            audioManager.PlayBgmBattle(_audioCategory);
            audioManager.PlayBgmMainMenu(_audioCategory);
        }
    }

    private void OnDestroy() {
        //unsubs audioEvent
        audioEvent.onBattleStart -= OnBattle;
    }
}