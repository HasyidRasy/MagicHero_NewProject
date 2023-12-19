using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDeathManager : MonoBehaviour
{
    public InventoryManagement inventoryManagement;
    public void DeathUICall(){
        inventoryManagement.UpdateBuffDisplay(UpgradeList.Instance.chosenUpgrades);
    }
}
