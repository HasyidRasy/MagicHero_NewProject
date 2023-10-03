using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    //bgm
    [SerializeField] private AudioSource bgmBattle;
    [SerializeField] private AudioSource bgmMainMenu;
    [SerializeField] private AudioSource bgmSafezone;
    //sfx upgrade
    [SerializeField] private AudioSource sfxUpgradeSuccess;
    [SerializeField] private AudioSource sfxUpgradeScreenOpened;
    [SerializeField] private AudioSource sfxUpgradeScreenClosed;
    [SerializeField] private AudioSource sfxUpgradeScreenPopup;
    //sfx battle
    [SerializeField] private AudioSource sfxEnemyDestroy;
    [SerializeField] private AudioSource sfxFireHit;
    [SerializeField] private AudioSource sfxWaterHit;
    [SerializeField] private AudioSource sfxAirHit;
    //sfx player
    [SerializeField] private AudioSource sfxAtk1;
    [SerializeField] private AudioSource sfxAtk2;
    [SerializeField] private AudioSource sfxAtk3;
    [SerializeField] private AudioSource sfxAtk4;
    [SerializeField] private AudioSource sfxDash;
    [SerializeField] private AudioSource sfxDeath;
    //sfx destructable object
    [SerializeField] private GameObject sfxDestructStone;
    [SerializeField] private GameObject sfxDestructWood;
    //sfx Environment
    [SerializeField] private AudioSource sfxStepOnGrass;
    [SerializeField] private AudioSource sfxStepOnWater;
    [SerializeField] private AudioSource sfxStepOnDirt;
    [SerializeField] private AudioSource sfxDoorOpen;
    [SerializeField] private AudioSource sfxDoorClose;
    //sfx elemental reaction
    [SerializeField] private AudioSource sfxSteam;
    [SerializeField] private AudioSource sfxFreeze;
    [SerializeField] private AudioSource sfxCombustion;

    public void PlayBgmBattle() {
        bgmBattle.Play();
    }

    public void PlayBgmMainMenu() {
        bgmMainMenu.Play();
    }

    public void PlayBgmSafezone() {
        bgmSafezone.Play();
    }

    public void PlaySfxDestructWood(Vector3 spawnPosition) {
        GameObject.Instantiate(sfxDestructWood, spawnPosition, Quaternion.identity);
    }

    public void PlaySfxDestructStone(Vector3 spawnPosition) {
        GameObject.Instantiate(sfxDestructStone, spawnPosition, Quaternion.identity);
    }

    public void PlaySfxDoorOpen() {
        sfxDoorOpen.Play();
    }

    public void PlaySfxDoorClose() {
        sfxDoorClose.Play();
    }
}