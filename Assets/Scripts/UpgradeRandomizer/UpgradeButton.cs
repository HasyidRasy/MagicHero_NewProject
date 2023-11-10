using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public UpgradeRandomizer upgradeRandomizer;
    public Image hoverImage;
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
        
        if(upgrade.rarity == UpgradeRarity.Common)
        {
            hoverImage.color = new Color(0.545f, 0.761f, 0.808f);
        }
        else if(upgrade.rarity == UpgradeRarity.Rare)
        {
            hoverImage.color = new Color(0.518f, 0.157f, 0.741f);
        }

        hoverImage.gameObject.SetActive(false);
    }

    public void SetUpgrade(UpgradeData upgradeData)
    {
        upgrade = upgradeData;
    }

    public void OnButtonClick()
    {
        Debug.Log("Button clicked");
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
        }

        if(upgrade.upgradeDesc != null)
        {
            description += upgrade.upgradeDesc;
        }
        return description;
    }

    public void OnPointerEnterButton()
    {
        // Activate the hover image here
        hoverImage.gameObject.SetActive(true);
    }

    public void OnPointerExitButton()
    {
        // Deactivate the hover image here
        hoverImage.gameObject.SetActive(false);
    }

}
