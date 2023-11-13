using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance; // Singleton pattern

    public UpgradeDatabase upgradeDatabase;

    // Create a dictionary to store selected upgrades
    private Dictionary<int, UpgradeData> selectedUpgrades = new Dictionary<int, UpgradeData>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Load selected upgrades from PlayerPrefs when the game starts
        LoadSelectedUpgrades();
    }

    // Save selected upgrades to PlayerPrefs
    public void SaveSelectedUpgrades()
    {
        foreach (var kvp in selectedUpgrades)
        {
            PlayerPrefs.SetInt(kvp.Key.ToString(), kvp.Value.upgradeID);
        }
        PlayerPrefs.Save();
    }

    // Load selected upgrades from PlayerPrefs
    public void LoadSelectedUpgrades()
    {
        selectedUpgrades.Clear();
        foreach (UpgradeData upgrade in upgradeDatabase.commonUpgrades)
        {
            if (PlayerPrefs.HasKey(upgrade.upgradeID.ToString()))
            {
                int upgradeID = PlayerPrefs.GetInt(upgrade.upgradeID.ToString());
                UpgradeData selectedUpgrade = upgradeDatabase.GetUpgradeByID(upgradeID);
                if (selectedUpgrade != null)
                {
                    selectedUpgrades.Add(upgradeID, selectedUpgrade);
                }
            }
        }

         foreach (UpgradeData upgrade in upgradeDatabase.rareUpgrades)
        {
            if (PlayerPrefs.HasKey(upgrade.upgradeID.ToString()))
            {
                int upgradeID = PlayerPrefs.GetInt(upgrade.upgradeID.ToString());
                UpgradeData selectedUpgrade = upgradeDatabase.GetUpgradeByID(upgradeID);
                if (selectedUpgrade != null)
                {
                    selectedUpgrades.Add(upgradeID, selectedUpgrade);
                }
            }
        }
    }

    // Add a selected upgrade
    public void AddSelectedUpgrade(UpgradeData upgrade)
    {
        selectedUpgrades[upgrade.upgradeID] = upgrade;
        SaveSelectedUpgrades();
    }

    // Get a selected upgrade
    public UpgradeData GetSelectedUpgrade(int upgradeID)
    {
        if (selectedUpgrades.ContainsKey(upgradeID))
        {
            return selectedUpgrades[upgradeID];
        }
        return null;
    }

     public void ApplyUpgradesToCharacter(CharacterModel characterModel)
    {
        foreach (var kvp in selectedUpgrades)
        {
            UpgradeData upgrade = kvp.Value;
            characterModel.ApplyUpgrade(upgrade);
        }
    }
}
