using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance; 
    public UpgradeDatabase upgradeDatabase;
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
        LoadSelectedUpgrades();
    }

    public void SaveSelectedUpgrades()
    {
        foreach (var kvp in selectedUpgrades)
        {
            PlayerPrefs.SetInt(kvp.Key.ToString(), kvp.Value.upgradeID);
        }
        PlayerPrefs.Save();
    }

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

        foreach (UpgradeData upgrade in upgradeDatabase.elementalUpgrades)
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
    public void AddSelectedUpgrade(UpgradeData upgrade)
    {
        selectedUpgrades[upgrade.upgradeID] = upgrade;
        SaveSelectedUpgrades();
    }
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
