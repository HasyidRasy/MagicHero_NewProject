using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewAudioManager : MonoBehaviour {
    public static NewAudioManager Instance;

    public Sound[] bgmAudio, sfxAudio;
    public AudioSource bgmSource, sfxSource;

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
