using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class UpgradeData : ScriptableObject
{
   public string upgradeName;
   public int upgradeID;
   public Sprite upgradeIcon;
   public UpgradeStats[] stats;
   public UpgradeRarity rarity;
   public string upgradeDesc;
   public ElementalType upgradeElement;
}

[System.Serializable]
public class UpgradeStats
{
    public int upgradeValueStatic;
    public UpgradeType upgradeType;
}

public enum UpgradeType
{
    ElementalAttack,
    BasicAttack,
    MaxHealthPoint,
    AttackSpeed,
    Defense,
    ElementalType
}

public enum UpgradeRarity
{
    Common,
    Rare,
    Legendary
}