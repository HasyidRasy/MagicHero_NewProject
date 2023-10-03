using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEvent : MonoBehaviour {
    public static AudioEvent current;

    public void Awake() {
        current = this;
    }

    //Play ketika Battle dimulai
    public event Action onBattleStart;
    public void BattleStart() {
        onBattleStart?.Invoke();
    }

    //Play ketika Player Trigger Enter ke door
    public event Action onDoorTriggerEnter;
    public void DoorTriggerEnter() {
        onDoorTriggerEnter?.Invoke();
    }

    //Play ketika Player Trigger Exit dari door
    public event Action onDoorTriggerExit;
    public void DoorTriggerExit() {
        onDoorTriggerExit?.Invoke();
    }
}
