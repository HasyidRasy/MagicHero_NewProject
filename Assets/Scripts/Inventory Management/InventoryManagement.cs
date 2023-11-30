using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManagement : MonoBehaviour
{
    public Transform buffIconContainer;
    public GameObject buffIconPrefab;   
    public TMP_Text upgradeName;
    public TMP_Text upgradeText;

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
            string upgradeName = buff.upgradeName;
            string upgradeDesc = buff.upgradeDesc;

             // Add an EventTrigger component to the buffIcon
            EventTrigger trigger = buffIcon.AddComponent<EventTrigger>();

            // Create a new entry for PointerEnter event
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((data) => OnPointerEnterButton(buff));
            trigger.triggers.Add(entry);
        }
    }

    public void OnPointerEnterButton(UpgradeData hoveredUpgrade)
    {
        NewAudioManager.Instance.PlaySFX("Hover");

        upgradeName.text = hoveredUpgrade.upgradeName;
        upgradeText.text = GetUpgradeDescription(hoveredUpgrade);
    }

    private string GetUpgradeDescription(UpgradeData upgrade)
    {
        string description = "";

        if(upgrade.upgradeDesc != null)
        {
            description = upgrade.upgradeDesc;
        }

        foreach (var stat in upgrade.stats)
        {
            if(stat.upgradeValueStatic > 0)
            {
                description = stat.upgradeType.ToString() + " +" + stat.upgradeValueStatic + "\n";
            }
            else if(stat.upgradeValueStatic < 0)
            {
                description = stat.upgradeType.ToString() + " -" + stat.upgradeValueStatic + "\n";
            }
        }
        return description;
    }

}
