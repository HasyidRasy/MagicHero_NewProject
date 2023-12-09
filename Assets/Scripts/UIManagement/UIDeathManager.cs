using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDeathManager : MonoBehaviour
{
    public InventoryManagement inventoryManagement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DeathUICall(){
        inventoryManagement.UpdateBuffDisplay(UpgradeList.Instance.chosenUpgrades);
    }
}
