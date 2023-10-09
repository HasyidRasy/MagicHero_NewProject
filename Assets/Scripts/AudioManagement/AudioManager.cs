using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    //bgm
    [Header("Background Music (BGM)")]
    [SerializeField] private AudioSource bgmBattle;
    [SerializeField] private AudioSource bgmMainMenu;
    [SerializeField] private AudioSource bgmSafezone;
    //sfx upgrade
    [Header("SFX Upgrade")]
    [SerializeField] private AudioSource sfxUpgradeSuccess;
    [SerializeField] private AudioSource sfxUpgradeScreenOpened;
    [SerializeField] private AudioSource sfxUpgradeScreenClosed;
    [SerializeField] private AudioSource sfxUpgradeScreenPopup;
    //sfx battle
    [Header("SFX Battle")]
    [SerializeField] private AudioSource sfxEnemyDestroy;
    [SerializeField] private AudioSource sfxFireHit;
    [SerializeField] private AudioSource sfxWaterHit;
    [SerializeField] private AudioSource sfxAirHit;
    //sfx player
    [Header("SFX Player")]
    [SerializeField] private AudioSource sfxAtk1;
    [SerializeField] private AudioSource sfxAtk2;
    [SerializeField] private AudioSource sfxAtk3;
    [SerializeField] private AudioSource sfxAtk4;
    [SerializeField] private AudioSource sfxDash;
    [SerializeField] private AudioSource sfxDeath;
    //sfx destructable object
    [Header("SFX Destrucable Object")]
    [SerializeField] private GameObject sfxDestructStone;
    [SerializeField] private GameObject sfxDestructWood;
    //sfx Environment
    [Header("SFX Environment")]
    [SerializeField] private AudioSource sfxStepOnGrass;
    [SerializeField] private AudioSource sfxStepOnWater;
    [SerializeField] private AudioSource sfxStepOnDirt;
    [SerializeField] private AudioSource sfxStepOnLava;
    [SerializeField] private AudioSource sfxDoorOpen;
    [SerializeField] private AudioSource sfxDoorClose;
    //sfx elemental reaction
    [Header("SFX Elemental Reaction")]
    [SerializeField] private AudioSource sfxSteam;
    [SerializeField] private AudioSource sfxFreeze;
    [SerializeField] private AudioSource sfxCombustion;

    //bgm
    public void PlayBgmBattle() {
        bgmBattle.Play();
    }

    public void PlayBgmMainMenu() {
        bgmMainMenu.Play();
    }

    public void PlayBgmSafezone() {
        bgmSafezone.Play();
    }

    //sfx upgrade
    public void PlaySfxUpgradeSuccess() {
        sfxUpgradeSuccess.Play();
    }

    public void PlaySfxUpgradeScreenOpened() {
        sfxUpgradeScreenOpened.Play();
    }

    public void PlaySfxUpgradeScreenClosed() {
        sfxUpgradeScreenClosed.Play();
    }

    public void PlaySfxUpgradeScreenPopup() {
        sfxUpgradeScreenPopup.Play();
    }

    //sfx battle
    public void PlaySfxEnemyDestroy() {
        sfxEnemyDestroy.Play();
    }

    public void PlaySfxFireHit() {
        sfxFireHit.Play();
    }

    public void PlaySfxWaterHit() {
        sfxWaterHit.Play();
    }

    public void PlaySfxAirHit() { 
        sfxAirHit.Play();
    }

    //sfx player
    public void PlaySfxAtk1() { 
        sfxAtk1.Play(); 
    }
    public void PlaySfxAtk2() { 
        sfxAtk2.Play(); 
    }
    public void PlaySfxAtk3() { 
        sfxAtk3.Play(); 
    }
    public void PlaySfxAtk4() {  
        sfxAtk4.Play(); 
    }
    public void PlaySfxDash() { 
        sfxDash.Play(); 
    }
    public void PlaySfxDeath() { 
        sfxDeath.Play(); 
    }

    //sfx destrucable object
    public void PlaySfxDestructWood(Vector3 spawnPosition) {
        GameObject.Instantiate(sfxDestructWood, spawnPosition, Quaternion.identity);
    }

    public void PlaySfxDestructStone(Vector3 spawnPosition) {
        GameObject.Instantiate(sfxDestructStone, spawnPosition, Quaternion.identity);
    }

    //sfx Environment
    public void PlaySfxStepOnGrass() { 
        sfxStepOnGrass.Play(); 
    }

    public void PlaySfxStepOnWater() { 
        sfxStepOnWater.Play(); 
    }

    public void PlaySfxStepOnDirt() { 
        sfxStepOnDirt.Play(); 
    }

    public void PlaySfxStepOnLava() { 
        sfxStepOnLava.Play(); 
    }

    public void PlaySfxDoorOpen() {
        sfxDoorOpen.Play();
    }

    public void PlaySfxDoorClose() {
        sfxDoorClose.Play();
    }

    //sfx elemental reaction
    public void PlaySfxSteam() {
        sfxSteam.Play();
    }

    public void PlaySfxFreeze() { 
        sfxFreeze.Play();
    }

    public void PlaySfxCombustion() {
        sfxCombustion.Play();
    }
}