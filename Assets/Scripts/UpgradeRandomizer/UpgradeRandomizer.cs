using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using Unity.VisualScripting;

public class UpgradeRandomizer : MonoBehaviour
{
    public static UpgradeRandomizer Instance { get; private set; }
    public UpgradeDatabase upgradeDatabase;
    public UpgradeManager upgradeManager;
    public TMP_Text[] upgradeNameText;
    public TMP_Text[] upgradeDescText;
    public Image[] upgradeImage;
    public Image[] upgradeRarity;
    public Image[] upgradeBgColor;
    public Button[] upgradeButtons;
    public Button upgradeSkip;

    private List<UpgradeData> availableUpgrades = new List<UpgradeData>();
    [SerializeField] List<UpgradeData> randomizedUpgrades = new List<UpgradeData>();
    private CharacterModel upgradedCharacter;

    private Dictionary<UpgradeRarity, int> rarityCounts = new Dictionary<UpgradeRarity, int>();

    // Start is called before the first frame update
    void Start()
    {
        upgradedCharacter = FindObjectOfType<CharacterModel>();

        if (upgradedCharacter == null)
        {
            Debug.Log("Character not found");
            return;
        }

        StartUpgrade();
    }

    public void TestTrigger()
    {
        if (Input.GetKeyDown("space"))
        {
            StartUpgrade();
        }
    }

    public void StartUpgrade()
    {
        availableUpgrades.Clear();
        availableUpgrades.AddRange(upgradeDatabase.commonUpgrades);
        availableUpgrades.AddRange(upgradeDatabase.rareUpgrades);

        InitializeRarityCounts();
        RandomizeUpgrades();
        UpdateUI();
    }

    private void InitializeRarityCounts()
    {
        rarityCounts.Clear();

        foreach (var rarity in System.Enum.GetValues(typeof(UpgradeRarity)))
        {
            rarityCounts[(UpgradeRarity)rarity] = 0;
            
        }

        foreach (var upgrade in availableUpgrades)
        {
            rarityCounts[upgrade.rarity]++;
        }
    }


   public void RandomizeUpgrades()
    {
    randomizedUpgrades.Clear();

    if (rarityCounts[UpgradeRarity.Common] < 1 || rarityCounts[UpgradeRarity.Rare] < 1)
    {
        Debug.Log("Not enough common or rare upgrades available.");
        return;
    }

    for (int i = 0; i < 3; i++)
    {
        UpgradeRarity selectedRarity = GetRandomRarityWithWeightedDistribution();
        UpgradeData selectedUpgrade = GetRandomUpgrade(selectedRarity);

        // Ensure that the selected upgrade is not already in the list
        while (randomizedUpgrades.Contains(selectedUpgrade))
        {
            selectedUpgrade = GetRandomUpgrade(selectedRarity);
        }

        randomizedUpgrades.Add(selectedUpgrade);
    }
}

    private UpgradeRarity GetRandomRarityWithWeightedDistribution()
    {
        /*Amount Randomizer
        int commonAmount = 0;
        int rareAmount = 0;
        int legendAmount = 0;
    
        foreach(var upgrades in upgradeDatabase.commonUpgrades)
        {
            commonAmount += 6;
        }

        foreach(var upgrades in upgradeDatabase.rareUpgrades)
        {
            rareAmount += 3;
        }

        foreach(var upgrades in upgradeDatabase.legendUpgrades)
        {
            legendAmount += 1;
        }

        int totalAmount = commonAmount + rareAmount + legendAmount;

        int randVal = Random.Range(1, totalAmount);

        if(randVal >= 1 && randVal <= commonAmount)
        {
            return UpgradeRarity.Common;
        }
        else if(randVal > commonAmount && randVal <= (commonAmount + rareAmount))
        {
            return UpgradeRarity.Rare;
        }
        else 
        {
            return UpgradeRarity.Legendary;
        }
        */
        
        //|| Percent randomizer;
        float randomValue = Random.Range(0f, 1f);
        
        // 60% chance for common, 30% chance for rare, 10% change for legend
        if (randomValue <= 0.60f)
        {
            return UpgradeRarity.Common;
        }
        else if (randomValue > 0.60f && randomValue <= 1f)
        {
            return UpgradeRarity.Rare;
        }
        else
        {
            return UpgradeRarity.Common;
        }
    }
    private UpgradeData GetRandomUpgrade(UpgradeRarity rarity)
    {
        List<UpgradeData> eligibleUpgrades = availableUpgrades.FindAll(upgrade => upgrade.rarity == rarity);

        if (eligibleUpgrades.Count > 0)
        {
            int randomIndex = Random.Range(0, eligibleUpgrades.Count);
            return eligibleUpgrades[randomIndex];
        }

        return null;
    }

    // Update is called once per frame
    public void UpdateUI()
    {
        for (int i = 0; i < 3; i++)
        {
            upgradeNameText[i].text = randomizedUpgrades[i].upgradeName;
            
            // Use Color values between 0 and 1
            if (randomizedUpgrades[i].rarity == UpgradeRarity.Common)
            {
                //upgradeNameText[i].color = new Color(0.545f, 0.761f, 0.808f); // Cyan color
                upgradeRarity[i].color = new Color(0.545f, 0.761f, 0.808f);
                upgradeBgColor[i].color = new Color(0.545f, 0.761f, 0.808f);
            }
            else if (randomizedUpgrades[i].rarity == UpgradeRarity.Rare)
            {
                //upgradeNameText[i].color = new Color(0.518f, 0.157f, 0.741f); // Purple color
                upgradeRarity[i].color = new Color(0.518f, 0.157f, 0.741f);
                upgradeBgColor[i].color = new Color(0.518f, 0.157f, 0.741f);
            }
            else
            {
                //upgradeNameText[i].color = Color.white;
                upgradeRarity[i].color = Color.white;
                upgradeBgColor[i].color = Color.white;
            }
            upgradeNameText[i].color = Color.white;
            upgradeDescText[i].text = GetUpgradeDescription(randomizedUpgrades[i]);
            upgradeImage[i].sprite = randomizedUpgrades[i].upgradeIcon;

            UpgradeButton upgradeButton = upgradeButtons[i].GetComponent<UpgradeButton>();
            upgradeButton.SetUpgrade(randomizedUpgrades[i]);
            Debug.Log(randomizedUpgrades[i]);

            upgradeButtons[i].interactable = true;

            foreach (var upgrade in randomizedUpgrades)
            {
                upgradeManager.AddSelectedUpgrade(upgrade);
            }
        }
    }

    private string GetUpgradeDescription(UpgradeData upgrade)
    {
        string description = "";

        if(upgrade.upgradeDesc != null)
        {
            description = upgrade.upgradeDesc;
        }

        foreach (var stat in upgrade.stats)
        {
            if(stat.upgradeValueStatic > 0)
            {
                description = stat.upgradeType.ToString() + " +" + stat.upgradeValueStatic + "\n";
            }
            else if(stat.upgradeValueStatic < 0)
            {
                description = stat.upgradeType.ToString() + " -" + stat.upgradeValueStatic + "\n";
            }
        }
        return description;
    }

}
