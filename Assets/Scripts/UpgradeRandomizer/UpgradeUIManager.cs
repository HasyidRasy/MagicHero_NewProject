using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIManager : MonoBehaviour
{
    public UpgradeRandomizer upgradeRandomizer;
    public UpgradeElemental upgradeElemental;

    private void Start()
    {

    }

    public void TriggerUI()
    {   
        // Activate the UpgradeRandomizer UI
        upgradeRandomizer.gameObject.SetActive(true);
        upgradeRandomizer.StartUpgrade();    
        Time.timeScale = 0f;

        NewAudioManager.Instance.bgmSource.Stop();
        NewAudioManager.Instance.PlayBGM("Safezone"); // Play BGM Safezone
    }

    public void TriggerElementalUI()
    {
        upgradeElemental.gameObject.SetActive(true);
        upgradeElemental.UpdateUI();
    }
}
