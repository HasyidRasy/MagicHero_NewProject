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
    public event Action<string> onBattleStart;
    public void BattleStart(string audioCategory) {
        onBattleStart?.Invoke(audioCategory);
    }

    //Play ketika berada pada main menu
    public event Action<string> onMainMenu;
    public void MainMenu(string audioCategory) {
        onMainMenu?.Invoke(audioCategory);
    }

    //Play ketika berada di safezone
    public event Action<string> onSafezone;
    public void Safezone(string audioCategory) {
        onSafezone?.Invoke(audioCategory);
    }

    //Play ketika berhasil melakukan upgrade
    public event Action<string> onUpgradeSuccess;
    public void UpgradeSuccess(string audioCategory) {
        onUpgradeSuccess?.Invoke(audioCategory);
    }

    //Play ketika membuka menu upgrade
    public event Action<string> onUpgradeOpened;
    public void UpgradeOpened(string audioCategory) {
        onUpgradeOpened?.Invoke(audioCategory);
    }

    //Play ketika menutup menu upgrade
    public event Action<string> onUpgradeClosed;
    public void UpgradeClosed(string audioCategory) {
        onUpgradeClosed?.Invoke(audioCategory);
    }

    //play ketika popup menu upgrade
    public event Action<string> onUpgradePopup;
    public void UpgradePopup(string audioCategory) {
        onUpgradePopup?.Invoke(audioCategory);
    }

    //Play ketika enemy mati
    public event Action<string> onEnemyDestroy;
    public void EnemyDestroy(string audioCategory) {
        onEnemyDestroy?.Invoke(audioCategory);
    }

    //Play ketika enemy terkena fire hit
    public event Action<string> onFireHit;
    public void FireHit(string audioCategory) {
        onFireHit?.Invoke(audioCategory);
    }

    //Play ketika enemy terkena water hit
    public event Action<string> onWaterHit;
    public void WaterHit(string audioCategory) {
        onWaterHit?.Invoke(audioCategory);
    }

    //Play ketika enemy terkena Air Hit
    public event Action<string> onAirHit;
    public void AirHit(string audioCategory) {
        onAirHit?.Invoke(audioCategory);
    }

    //Play ketika hit 1
    public event Action<string> onHit1;
    public void Hit1(string audioCategory) {
        onHit1?.Invoke(audioCategory);
    }

    //Play ketika hit 2
    public event Action<string> onHit2;
    public void Hit2(string audioCategory) {
        onHit2?.Invoke(audioCategory);
    }

    //Play ketika hit 3
    public event Action<string> onHit3;
    public void Hit3(string audioCategory) {
        onHit3?.Invoke(audioCategory);
    }

    //Play ketika hit 4
    public event Action<string> onHit4;
    public void Hit4(string audioCategory) {
        onHit4?.Invoke(audioCategory);
    }

    //Play ketika palyer melakukan dash
    public event Action<string> onPlayerDash;
    public void PlayerDash(string audioCategory) {
        onPlayerDash?.Invoke(audioCategory);
    }

    //Play ketika Player mati
    public event Action<string> onPlayerDeath;
    public void PlayerDeath(string audioCategory) {
        onPlayerDeath?.Invoke(audioCategory);
    }

    //Play ketika menghancurkan object batu
    public event Action onPlayerDestructStone;
    public void PlayerDestructStone() {
        onPlayerDestructStone?.Invoke();
    }

    //Play ketika menghancurkan object kayu
    public event Action onPlayerDestructWood;
    public void PlayerDestructWood() {
        onPlayerDestructWood?.Invoke();
    }

    //Play ketika palyer berjalan di rumput
    public event Action<string> onPlayerStepOnGrass;
    public void PlayerStepOnGrass(string audioCategory) {
        onPlayerStepOnGrass?.Invoke(audioCategory);
    }

    //Play ketika player berjalan di air
    public event Action<string> onPlayerStepOnWater;
    public void PlayerStepOnWater(string audioCategory) {
        onPlayerStepOnWater?.Invoke(audioCategory);
    }

    //Play ketika player berjalan di tanah
    public event Action<string> onPlayerStepOnDirt;
    public void PlayerStepOnDirt(string audioCategory) {
        onPlayerStepOnDirt?.Invoke(audioCategory);
    }

    //Play ketika player berjalan di lava
    public event Action<string> onPlayerStepOnLava;
    public void PlayerStepOnLava(string audioCategory) {
        onPlayerStepOnLava?.Invoke(audioCategory);
    }

    //Play ketika Player Trigger Enter ke door
    public event Action<string> onDoorTriggerEnter;
    public void DoorTriggerEnter(string audioCategory) {
        onDoorTriggerEnter?.Invoke(audioCategory);
    }

    //Play ketika Player Trigger Exit dari door
    public event Action<string> onDoorTriggerExit;
    public void DoorTriggerExit(string audioCategory) {
        onDoorTriggerExit?.Invoke(audioCategory);
    }

    //Play ketika terjadi reaction steam
    public event Action<string> onReactionSteam;
    public void ReactionSteam(string audioCategory) {  
        onReactionSteam?.Invoke(audioCategory);
    }

    //Play ketika terjadi reaction freeze
    public event Action<string> onReactionFreeze;
    public void ReactionFreeze(string audioCategory) {  
        onReactionFreeze?.Invoke(audioCategory); 
    }

    //Play ketika terjadi reaction combustion
    public event Action<string> onReactionCombustion;
    public void ReactionCombustion(string audioCategory) {
        onReactionCombustion?.Invoke(audioCategory);
    }
}
