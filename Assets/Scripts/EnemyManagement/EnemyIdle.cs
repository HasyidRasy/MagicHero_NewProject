using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : MonoBehaviour {
    private Animator _animator;

    private void Awake() {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start() {
        _animator.SetBool("isIdle", true);
    }
}
