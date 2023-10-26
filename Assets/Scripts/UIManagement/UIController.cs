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
        NewAudioManager.Instance.ToggleBGM();
    }

    public void ToggleSfx() {
        NewAudioManager.Instance.ToggleSFX();
    }

    public void BgmVolume() {
        NewAudioManager.Instance.BgmVolume(_bgmSlider.value);
    }

    public void SfxVolume() {
        NewAudioManager.Instance.SfxVolume(_sfxSlider.value);
    }

    private void LoadVolume() {
        _bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        _sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        BgmVolume();
        SfxVolume();
    }
}
