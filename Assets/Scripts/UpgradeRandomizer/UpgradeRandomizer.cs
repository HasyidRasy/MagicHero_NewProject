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

        RandomizeUpgrades();
        UpdateUI();
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
        randomizedUpgrades.Add(selectedUpgrade);
    }
}

    private UpgradeRarity GetRandomRarityWithWeightedDistribution()
    {
        float randomValue = Random.Range(0f, 1f);
        
        // 75% chance for common, 25% chance for rare
        if (randomValue <= 0.75f)
        {
            return UpgradeRarity.Common;
        }
        else
        {
            return UpgradeRarity.Rare;
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
            if (randomizedUpgrades[i].rarity == UpgradeRarity.Common)
            {
                upgradeNameText[i].color = Color.green;
            }
            else if (randomizedUpgrades[i].rarity == UpgradeRarity.Rare)
            {
                upgradeNameText[i].color = Color.blue;
            }
            else
            {
                upgradeNameText[i].color = Color.white;
            }
            upgradeDescText[i].text = randomizedUpgrades[i].upgradeType.ToString() + " +" + randomizedUpgrades[i].upgradeValue;
            upgradeImage[i].sprite = randomizedUpgrades[i].upgradeIcon;

            UpgradeButton upgradeButton = upgradeButtons[i].GetComponent<UpgradeButton>();
            upgradeButton.SetUpgrade(randomizedUpgrades[i]);

            upgradeButtons[i].interactable = true;

            foreach (var upgrade in randomizedUpgrades)
            {
                upgradeManager.AddSelectedUpgrade(upgrade);
            }
        }
    }
}
