using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    public UpgradeRandomizer upgradeRandomizer;
    private UpgradeManager upgradeManager;
    private CharacterModel upgradedCharacter;
    private UpgradeData upgrade;
    public static int id = 1;
    void Start()
    {
        upgradeManager = FindObjectOfType<UpgradeManager>();
        upgradedCharacter = FindObjectOfType<CharacterModel>();

        if(upgradedCharacter == null)
        {
            Debug.Log("Characracter not found");
            return;
        }
        
        if(upgradeManager == null)
        {
            Debug.Log("Upgrade not found");
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
            Debug.Log("Upgrade Description: " + GetUpgradeDescription(upgrade));
        
            upgradeRandomizer.gameObject.SetActive(false);
            Time.timeScale = 1f;
            GameEvents.current.DoorwayTriggerEnter(id);
            id++;
        }
    }

     private string GetUpgradeDescription(UpgradeData upgrade)
    {
        string description = "";
        foreach (var stat in upgrade.stats)
        {
            if(stat.upgradeValueStatic > 0)
            {
                description += stat.upgradeType.ToString() + " +" + stat.upgradeValueStatic + "\n";
            }
            else if(stat.upgradeValueStatic < 0)
            {
                description += stat.upgradeType.ToString() + " -" + stat.upgradeValueStatic + "\n";
            }

            if(stat.upgradeValuePercent != 0)
            {
                description += stat.upgradeType.ToString() + " +" + stat.upgradeValuePercent + "%\n";
            }
        }
        return description;
    }
}
