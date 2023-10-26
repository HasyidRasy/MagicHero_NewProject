using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    //bgm
    [Header("Background Music (BGM)")]
    [SerializeField] private AudioSource bgmBattle;
    [SerializeField] private AudioSource bgmMainMenu;
    [SerializeField] private string _bgmMainMenu;
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

    private string _audioCategory;

    //bgm
    public void PlayBgmBattle(string audioCategory) {
        _audioCategory = "bgm battle";
        if (_audioCategory == audioCategory) {
            bgmBattle.Play();
        } else {
            bgmBattle.Stop();
        }
    }

    public void PlayBgmMainMenu(string audioCategory) {
        _audioCategory = _bgmMainMenu;
        if (_audioCategory == audioCategory) {
            bgmMainMenu.Play();
        } else {
            bgmMainMenu.Stop();
        }
    }

    public void PlayBgmSafezone(string audioCategory) {
        _audioCategory = "bgm safezone";
        if (_audioCategory == audioCategory) {
            bgmSafezone.Play();
        } else {
            bgmSafezone.Stop();
        }
    }

    //sfx upgrade
    public void PlaySfxUpgradeSuccess(string audioCategory) {
        _audioCategory = "sfx upgrade success";
        if (_audioCategory == audioCategory) {
            sfxUpgradeSuccess.Play();
        } else {
            sfxUpgradeSuccess.Stop();
        }
    }

    public void PlaySfxUpgradeScreenOpened(string audioCategory) {
        _audioCategory = "sfx upgrade open";
        if (_audioCategory == audioCategory) {
            sfxUpgradeScreenOpened.Play();
        } else {  
            sfxUpgradeScreenOpened.Stop();
        }
    }

    public void PlaySfxUpgradeScreenClosed(string audioCategory) {
        _audioCategory = "sfx upgrade close";
        if (_audioCategory == audioCategory) {
            sfxUpgradeScreenClosed.Play();
        } else {
            sfxUpgradeScreenClosed.Stop();
        }
    }

    public void PlaySfxUpgradeScreenPopup(string audioCategory) {
        _audioCategory = "sfx upgrade popup";
        if (_audioCategory == audioCategory) {
            sfxUpgradeScreenPopup.Play();
        } else {
            sfxUpgradeScreenPopup.Stop();
        }
    }

    //sfx battle
    public void PlaySfxEnemyDestroy(string audioCategory) {
        _audioCategory = "sfx enemy destroy";
        if (_audioCategory == audioCategory) {
            sfxEnemyDestroy.Play();
        } else {
            sfxEnemyDestroy.Stop();
        }
    }

    public void PlaySfxFireHit(string audioCategory) {
        _audioCategory = "sfx fire hit";
        if (_audioCategory == audioCategory) {
            sfxFireHit.Play();
        } else {
            sfxFireHit.Stop();
        }
    }

    public void PlaySfxWaterHit(string audioCategory) {
        _audioCategory = "sfx water hit";
        if (_audioCategory == audioCategory) {
            sfxWaterHit.Play();
        } else {
            sfxWaterHit.Stop();
        }
    }

    public void PlaySfxAirHit(string audioCategory) {
        _audioCategory = "sfx air hit";
        if (_audioCategory == audioCategory) {
            sfxAirHit.Play();
        } else {
            sfxAirHit.Stop();
        }
    }

    //sfx player
    public void PlaySfxAtk1(string audioCategory) {
        _audioCategory = "sfx atk1";
        if (_audioCategory == audioCategory) {
            sfxAtk1.Play();
        } else {
            sfxAtk1.Stop();
        }
    }
    public void PlaySfxAtk2(string audioCategory) {
        _audioCategory = "sfx atk2";
        if (_audioCategory == audioCategory) {
            sfxAtk2.Play();
        } else {
            sfxAtk2.Stop();
        }
    }
    public void PlaySfxAtk3(string audioCategory) {
        _audioCategory = "sfx atk3";
        if (_audioCategory == audioCategory) {
            sfxAtk3.Play();
        } else {
            sfxAtk3.Stop();
        }
    }
    public void PlaySfxAtk4(string audioCategory) {
        _audioCategory = "sfx atk4";
        if (_audioCategory == audioCategory) {
            sfxAtk4.Play();
        } else {
            sfxAtk4.Stop();
        }
    }
    public void PlaySfxDash(string audioCategory) {
        _audioCategory = "sfx dash";
        if (_audioCategory == audioCategory) {
            sfxDash.Play();
        } else {
            sfxDash.Stop();
        }
    }
    public void PlaySfxDeath(string audioCategory) {
        _audioCategory = "sfx death";
        if (_audioCategory == audioCategory) {
            sfxDeath.Play();
        } else {
            sfxDeath.Stop();
        }
    }

    //sfx destrucable object
    public void PlaySfxDestructWood(Vector3 spawnPosition) {
        GameObject.Instantiate(sfxDestructWood, spawnPosition, Quaternion.identity);
    }

    public void PlaySfxDestructStone(Vector3 spawnPosition) {
        GameObject.Instantiate(sfxDestructStone, spawnPosition, Quaternion.identity);
    }

    //sfx Environment
    public void PlaySfxStepOnGrass(string audioCategory) {
        _audioCategory = "sfx step on grass";
        if (_audioCategory == audioCategory) {
            sfxStepOnGrass.Play();
        } else {  
            sfxStepOnGrass.Stop(); 
        }
    }

    public void PlaySfxStepOnWater(string audioCategory) {
        _audioCategory = "sfx step on water";
        if (_audioCategory == audioCategory) {
            sfxStepOnWater.Play();
        } else {
            sfxStepOnWater.Stop();
        }
    }

    public void PlaySfxStepOnDirt(string audioCategory) {
        _audioCategory = "sfx step on dirt";
        if (_audioCategory == audioCategory) {
            sfxStepOnDirt.Play();
        } else {  
            sfxStepOnDirt.Stop();
        }
    }

    public void PlaySfxStepOnLava(string audioCategory) {
        _audioCategory = "sfx step on lava";
        if (_audioCategory == audioCategory) {
            sfxStepOnLava.Play();
        } else {
            sfxStepOnLava.Stop();
        }
    }

    public void PlaySfxDoorOpen(string audioCategory) {
        _audioCategory = "sfx door open";
        if (_audioCategory == audioCategory) {
            sfxDoorOpen.Play();
        } else {  
            sfxDoorOpen.Stop();
        }
    }

    public void PlaySfxDoorClose(string audioCategory) {
        _audioCategory = "sfx door close";
        if (_audioCategory == audioCategory) {
            sfxDoorClose.Play();
        } else {
            sfxDoorClose.Stop();
        }
    }

    //sfx elemental reaction
    public void PlaySfxSteam(string audioCategory) {
        _audioCategory = "sfx steam";
        if (_audioCategory == audioCategory) {
            sfxSteam.Play();
        } else {
            sfxSteam.Stop();
        }
    }

    public void PlaySfxFreeze(string audioCategory) {
        _audioCategory = "sfx freeze";
        if (_audioCategory == audioCategory) {
            sfxFreeze.Play();
        } else {
            sfxFreeze.Stop();
        }
    }

    public void PlaySfxCombustion(string audioCategory) {
        _audioCategory = "sfx combustion";
        if (_audioCategory == audioCategory) {
            sfxCombustion.Play();
        } else {
            sfxCombustion.Stop();
        }
    }
}