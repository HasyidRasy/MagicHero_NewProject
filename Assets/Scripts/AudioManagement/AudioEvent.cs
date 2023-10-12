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

    //Play ketika berada pada main menu
    public event Action onMainMenu;
    public void MainMenu() {
        onMainMenu?.Invoke();
    }

    //Play ketika berada di safezone
    public event Action onSafezone;
    public void Safezone() {
        onSafezone?.Invoke();
    }

    //Play ketika berhasil melakukan upgrade
    public event Action onUpgradeSuccess;
    public void UpgradeSuccess() {
        onUpgradeSuccess?.Invoke();
    }

    //Play ketika membuka menu upgrade
    public event Action onUpgradeOpened;
    public void UpgradeOpened() {
        onUpgradeOpened?.Invoke();
    }

    //Play ketika menutup menu upgrade
    public event Action onUpgradeClosed;
    public void UpgradeClosed() {
        onUpgradeClosed?.Invoke();
    }

    //play ketika popup menu upgrade
    public event Action onUpgradePopup;
    public void UpgradePopup() {
        onUpgradePopup?.Invoke();
    }

    //Play ketika enemy mati
    public event Action onEnemyDestroy;
    public void EnemyDestroy() {
        onEnemyDestroy?.Invoke();
    }

    //Play ketika enemy terkena fire hit
    public event Action onFireHit;
    public void FireHit() {
        onFireHit?.Invoke();
    }

    //Play ketika enemy terkena water hit
    public event Action onWaterHit;
    public void WaterHit() {
        onWaterHit?.Invoke();
    }

    //Play ketika enemy terkena Air Hit
    public event Action onAirHit;
    public void AirHit() {
        onAirHit?.Invoke();
    }

    //Play ketika hit 1
    public event Action onHit1;
    public void Hit1() {
        onHit1?.Invoke();
    }

    //Play ketika hit 2
    public event Action onHit2;
    public void Hit2() {
        onHit2?.Invoke();
    }

    //Play ketika hit 3
    public event Action onHit3;
    public void Hit3() {
        onHit3?.Invoke();
    }

    //Play ketika hit 4
    public event Action onHit4;
    public void Hit4() {
        onHit4?.Invoke();
    }

    //Play ketika palyer melakukan dash
    public event Action onPlayerDash;
    public void PlayerDash() {
        onPlayerDash?.Invoke();
    }

    //Play ketika Player mati
    public event Action onPlayerDeath;
    public void PlayerDeath() {
        onPlayerDeath?.Invoke();
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
    public event Action onPlayerStepOnGrass;
    public void PlayerStepOnGrass() {
        onPlayerStepOnGrass?.Invoke();
    }

    //Play ketika player berjalan di air
    public event Action onPlayerStepOnWater;
    public void PlayerStepOnWater() {
        onPlayerStepOnWater?.Invoke();
    }

    //Play ketika player berjalan di tanah
    public event Action onPlayerStepOnDirt;
    public void PlayerStepOnDirt() {
        onPlayerStepOnDirt?.Invoke();
    }

    //Play ketika player berjalan di lava
    public event Action onPlayerStepOnLava;
    public void PlayerStepOnLava() {
        onPlayerStepOnLava?.Invoke();
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

    //Play ketika terjadi reaction steam
    public event Action onReactionSteam;
    public void ReactionSteam() {  
        onReactionSteam?.Invoke();
    }

    //Play ketika terjadi reaction freeze
    public event Action onReactionFreeze;
    public void ReactionFreeze() {  
        onReactionFreeze?.Invoke(); 
    }

    //Play ketika terjadi reaction combustion
    public event Action onReactionCombustion;
    public void ReactionCombustion() {
        onReactionCombustion?.Invoke();
    }
}
