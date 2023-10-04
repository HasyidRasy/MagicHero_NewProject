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
   public int upgradeValue;
   public UpgradeType upgradeType;
   
}

public enum UpgradeType
{
    ElementalAttack,
    BasicAttack,
    HealthPoint,
    AttackSpeed,
    Defense
}
