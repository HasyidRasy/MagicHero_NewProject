using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class UIController : MonoBehaviour {
    [SerializeField] private AudioMixer _audioMixer;
    [Header("Volume")]
    public Slider _volumeSlider;
    [Header("BGM")]
    public Slider _bgmSlider;
    [Header("SFX")]
    public Slider _sfxSlider;

    private void Start() {
        if (PlayerPrefs.HasKey("bgmVolume") && PlayerPrefs.HasKey("sfxVolume") && PlayerPrefs.HasKey("volumeVolume")) {
            LoadVolume();
        } else {
            BgmVolume();
            SfxVolume();
            VolumeVolume();
        }
    }

    public void VolumeVolume() {
        float volume = _volumeSlider.value;
        _audioMixer.SetFloat("VolumeParams", Mathf.Log10(volume));
        PlayerPrefs.SetFloat("volumeVolume", volume);
    }

    public void BgmVolume() {
        float volume = _bgmSlider.value;
        _audioMixer.SetFloat("BgmParams", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("bgmVolume", volume);
    }

    public void SfxVolume() {
        float volume = _sfxSlider.value;
        _audioMixer.SetFloat("SfxParams", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void LoadVolume() {
        _bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        _sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        _volumeSlider.value = PlayerPrefs.GetFloat("volumeVolume");

        VolumeVolume();
        BgmVolume();
        SfxVolume();
    }
}
