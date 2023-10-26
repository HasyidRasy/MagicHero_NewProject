using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public Slider _bgmSlider;
    public Slider _sfxSlider;
    public Toggle _bgmToggle;
    public Toggle _sfxToggle;

    private void Start() {
        if (PlayerPrefs.HasKey("musicVolume")) {
            LoadVolume();
        } else {
            BgmVolume();
            SfxVolume();
        }
    }

    public void ToggleBgm() {
        if (_bgmToggle != null) {
            NewAudioManager.Instance.ToggleBGM();
        }
    }

    public void ToggleSfx() {
        if (_sfxToggle != null) {
            NewAudioManager.Instance.ToggleSFX();
        }
    }

    public void BgmVolume() {
        if (_bgmSlider != null) {
            NewAudioManager.Instance.BgmVolume(_bgmSlider.value);
        }
    }

    public void SfxVolume() {
        if (_sfxSlider != null) {
            NewAudioManager.Instance.SfxVolume(_sfxSlider.value);
        }
    }

    private void LoadVolume() {
        _bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        _sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        BgmVolume();
        SfxVolume();
    }
}
