using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeElemental : MonoBehaviour
{
    public UpgradeDatabase upgradeDatabase;
    public UpgradeManager upgradeManager;
    public TMP_Text[] upgradeNameText;
    public TMP_Text[] upgradeDescText;
    public Image[] upgradeImage;
    public Image[] upgradeRarity;
    public Image[] upgradeBgColor;
    public Button[] upgradeButtons;
    public Button upgradeSkip;

    private CharacterModel upgradedCharacter;
    private ElementSwitchSystem elementSwitchSystem;

    public List<UpgradeData> availableUpgrades = new List<UpgradeData>();
    

    // Start is called before the first frame update
    void Start()
    {
        upgradedCharacter = FindObjectOfType<CharacterModel>();
        elementSwitchSystem = FindObjectOfType<ElementSwitchSystem>();

        if (upgradedCharacter == null)
        {
            Debug.Log("Character not found");
            return;
        }

        availableUpgrades.AddRange(upgradeDatabase.elementalUpgrades);
        UpdateUI();

        
    }

    // Update is called once per frame
    public void UpdateUI()
    {
        for (int i = 0; i < 3; i++)
        {
            UpgradeElementalButton upgradeButton = upgradeButtons[i].GetComponent<UpgradeElementalButton>();
            upgradeButton.SetUpgrade(availableUpgrades[i]);

            upgradeButtons[i].interactable = true;

            upgradeNameText[i].text = availableUpgrades[i].upgradeName;
            
            if (availableUpgrades[i].upgradeID == 0)
            {
                upgradeRarity[i].color = new Color(86f / 255f, 139f / 255f, 200f / 255f);
                upgradeBgColor[i].color = new Color(86f / 255f, 139f / 255f, 200f / 255f);
                if(elementSwitchSystem.unlockedElementInfo.isWaterUnlocked == true)
                {
                    upgradeButtons[i].interactable = false;
                }
                                       
            }
            else if (availableUpgrades[i].upgradeID == 1)
            {
                upgradeRarity[i].color = new Color(0f / 255f, 169f / 255f, 157f / 255f);
                upgradeBgColor[i].color = new Color(0f / 255f, 169f / 255f, 157f / 255f);
                if(elementSwitchSystem.unlockedElementInfo.isWindUnlocked == true)
                {
                    upgradeButtons[i].interactable = false;
                }
            }
            else if (availableUpgrades[i].upgradeID == 2)
            {
                upgradeRarity[i].color = new Color(199f / 255f, 56f / 255f, 56f / 255f);
                upgradeBgColor[i].color = new Color(199f / 255f, 56f / 255f, 56f / 255f);
                if(elementSwitchSystem.unlockedElementInfo.isFireUnlocked == true)
                {
                    upgradeButtons[i].interactable = false;
                }
            }
            else
            {
                upgradeRarity[i].color = Color.white;
                upgradeBgColor[i].color = Color.white;
            }
            upgradeNameText[i].color = Color.white;
            upgradeDescText[i].text = GetUpgradeDescription(availableUpgrades[i]);
            upgradeImage[i].sprite = availableUpgrades[i].upgradeIcon;
            
            foreach (var upgrade in availableUpgrades)
            {
                upgradeManager.AddSelectedUpgrade(upgrade);
            }
        }
    }

    public string GetUpgradeDescription(UpgradeData upgrade)
    {
        string description = "";

        if(upgrade.upgradeDesc != null)
        {
            description = upgrade.upgradeDesc;
        }
        return description;
    }
}
