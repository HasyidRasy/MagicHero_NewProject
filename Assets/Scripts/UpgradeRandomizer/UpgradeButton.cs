using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    public UpgradeRandomizer upgradeRandomizer;

    private CharacterModel upgradedCharacter;

    private UpgradeData upgrade;

    void Start()
    {
        upgradedCharacter = FindObjectOfType<CharacterModel>();

        if(upgradedCharacter == null)
        {
            Debug.Log("Characracter not found");
            return;
        }

    }

    public void SetUpgrade(UpgradeData upgradeData)
    {
        upgrade = upgradeData;
    }

    public void OnButtonClick()
    {
        if (upgrade != null)
        {
            UpgradeData selectedUpgrade = UpgradeManager.instance.GetSelectedUpgrade(upgrade.upgradeID);

            upgradedCharacter.ApplyUpgrade(upgrade);

            Debug.Log("Upgrade Name: " + upgrade.upgradeName);
            Debug.Log("Upgrade Description: " + upgrade.upgradeType.ToString() + " " + upgrade.upgradeValue);
        
            upgradeRandomizer.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
