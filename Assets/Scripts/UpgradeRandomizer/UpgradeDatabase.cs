using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Database",menuName ="Upgrade Database")]
public class UpgradeDatabase : ScriptableObject
{
    public List<UpgradeData> commonUpgrades = new List<UpgradeData>();
    public List<UpgradeData> rareUpgrades = new List<UpgradeData>();  
    public List<UpgradeData> legendUpgrades = new List<UpgradeData>();  

    public UpgradeData GetUpgradeByID(int id)
    {
        UpgradeData upgrades = commonUpgrades.Find(upgrade => upgrade.upgradeID == id);
        if (upgrades = null)
        {
            upgrades = rareUpgrades.Find(upgrade => upgrade.upgradeID == id);
        }
        return upgrades;
    }

}
