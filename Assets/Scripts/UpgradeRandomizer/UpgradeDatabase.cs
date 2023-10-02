using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Database",menuName ="Upgrade Database")]
public class UpgradeDatabase : ScriptableObject
{
    public List<UpgradeData> upgrades = new List<UpgradeData>();

    public UpgradeData GetUpgradeByID(int id)
    {
        return upgrades.Find(upgrade => upgrade.upgradeID == id);
    }

}
