using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewAudioManager : MonoBehaviour {
    public static NewAudioManager Instance;

    public Sound[] bgmAudio, sfxAudio, playerAudio, atkAudio, stepAudio, enemyAtkAudio, enemySfxAudio, upgradeAudio;
    public AudioSource bgmSource, sfxSource, playerSource, atkSource, stepSource, enemyAtkSource, enemySfxSource, upgradeSource;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        sfxSource.volume = 0.5f;
    }

    public void PlayBGM(string name) {
        Sound bgm = Array.Find(bgmAudio, x => x.names == name);
        if (bgm == null) {
            Debug.Log("BGM Not Found!");
        } else {
            bgmSource.clip = bgm.clip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void PlaySFX(string name) {
        Sound sfx = Array.Find(sfxAudio, x => x.names == name);
        if (sfx == null) {
            Debug.Log("SFX Not Found!");
        } else {
            sfxSource.PlayOneShot(sfx.clip);
            //sfxSource.clip = sfx.clip;
            //sfxSource.Play();
        }
    }

    public void PlayPlayerSFX(string name) {
        Sound playerSfx = Array.Find(playerAudio, x => x.names == name);
        if (playerSfx == null) {
            Debug.Log("Player SFX Not Found!");
        }
        else
        {
            playerSource.PlayOneShot(playerSfx.clip);
        }
    }

    public void PlayStepSFX(string name) {
        Sound playerSfx = Array.Find(stepAudio, x => x.names == name);
        if (playerSfx == null) {
            Debug.Log("Player SFX Not Found!");
        } else {
            stepSource.PlayOneShot(playerSfx.clip);
        }
    }

    public void PlayAtkSFX(string name) {
        Sound playerSfx = Array.Find(atkAudio, x => x.names == name);
        if (playerSfx == null) {
            Debug.Log("Player SFX Not Found!");
        } else {
            atkSource.PlayOneShot(playerSfx.clip);
        }
    }

    public void PlayEnemyAtkSFX(string name) {
        Sound playerSfx = Array.Find(enemyAtkAudio, x => x.names == name);
        if (playerSfx == null) {
            Debug.Log("Player SFX Not Found!");
        } else {
            enemyAtkSource.PlayOneShot(playerSfx.clip);
        }
    }

    public void PlayEnemySFX(string name) {
        Sound playerSfx = Array.Find(enemySfxAudio, x => x.names == name);
        if (playerSfx == null) {
            Debug.Log("Player SFX Not Found!");
        } else {
            enemySfxSource.PlayOneShot(playerSfx.clip);
        }
    }

    public void PlayUpgradeSFX(string name) {
        Sound playerSfx = Array.Find(upgradeAudio, x => x.names == name);
        if (playerSfx == null) {
            Debug.Log("Player SFX Not Found!");
        } else {
            upgradeSource.PlayOneShot(playerSfx.clip);
        }
    }

    public void ToggleBGM() {
        bgmSource.mute = !bgmSource.mute;
    }

    public void ToggleSFX() {
        sfxSource.mute = !sfxSource.mute;
    }

    public void BgmVolume(float volume) {
        bgmSource.volume = volume;
        PlayerPrefs.SetFloat("bgmVolume", volume);
    }

    public void SfxVolume(float volume) {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
}
