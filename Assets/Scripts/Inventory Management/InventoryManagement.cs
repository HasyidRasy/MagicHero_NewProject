using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManagement : MonoBehaviour
{
    public Transform buffIconContainer;
    public GameObject buffIconPrefab;   

    public void UpdateBuffDisplay(List<UpgradeData> activeBuffs)
    {
        foreach (Transform child in buffIconContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (UpgradeData buff in activeBuffs)
        {
            GameObject buffIcon = Instantiate(buffIconPrefab, buffIconContainer);
            buffIcon.GetComponent<Image>().sprite = buff.upgradeIcon;
        }
    }
}
