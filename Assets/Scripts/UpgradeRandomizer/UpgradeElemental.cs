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

    public List<UpgradeData> availableUpgrades = new List<UpgradeData>();
    

    // Start is called before the first frame update
    void Start()
    {
        
        upgradedCharacter = FindObjectOfType<CharacterModel>();

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
            upgradeNameText[i].text = availableUpgrades[i].upgradeName;
            
            if (availableUpgrades[i].upgradeID == 0)
            {
                upgradeRarity[i].color = new Color(86f / 255f, 139f / 255f, 200f / 255f);
                upgradeBgColor[i].color = new Color(86f / 255f, 139f / 255f, 200f / 255f);
            }
            else if (availableUpgrades[i].upgradeID == 1)
            {
                upgradeRarity[i].color = new Color(0f / 255f, 169f / 255f, 157f / 255f);
                upgradeBgColor[i].color = new Color(0f / 255f, 169f / 255f, 157f / 255f);
            }
            else if (availableUpgrades[i].upgradeID == 2)
            {
                upgradeRarity[i].color = new Color(199f / 255f, 56f / 255f, 56f / 255f);
                upgradeBgColor[i].color = new Color(199f / 255f, 56f / 255f, 56f / 255f);
            }
            else
            {
                upgradeRarity[i].color = Color.white;
                upgradeBgColor[i].color = Color.white;
            }
            upgradeNameText[i].color = Color.white;
            upgradeDescText[i].text = GetUpgradeDescription(availableUpgrades[i]);
            upgradeImage[i].sprite = availableUpgrades[i].upgradeIcon;

            UpgradeElementalButton upgradeButton = upgradeButtons[i].GetComponent<UpgradeElementalButton>();
            upgradeButton.SetUpgrade(availableUpgrades[i]);

            upgradeButtons[i].interactable = true;

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
