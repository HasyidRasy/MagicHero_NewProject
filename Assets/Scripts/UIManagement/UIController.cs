using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public Slider _bgmSlider;
    public Slider _sfxSlider;

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
}
