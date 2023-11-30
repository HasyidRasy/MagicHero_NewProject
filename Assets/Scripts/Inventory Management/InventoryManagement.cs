using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManagement : MonoBehaviour
{
    public Transform buffIconContainer;
    public Transform buffSlotsContainer;
    public List<Image> buffSlots = new List<Image>();
    public GameObject buffIconPrefab;

    public void UpdateBuffDisplay(List<UpgradeData> activeBuffs)
    {
        // Clear the list before updating
        buffSlots.Clear();

        // Get all Image components from children of buffSlotsContainer
        Image[] childImages = buffSlotsContainer.GetComponentsInChildren<Image>(true);
        buffSlots.AddRange(childImages);

        foreach (Transform child in buffIconContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < activeBuffs.Count; i++)
        {
            UpgradeData buff = activeBuffs[i];

            if (i < buffSlots.Count)
            {
                Image slot = buffSlots[i];

                if (buff.rarity == UpgradeRarity.Common)
                {
                    slot.color = new Color(0.545f, 0.761f, 0.808f);
                }
                else if (buff.rarity == UpgradeRarity.Rare)
                {
                    slot.color = new Color(0.518f, 0.157f, 0.741f);
                }
                else
                {
                    slot.color = Color.white;
                }
            }

            // Instantiate and set the buff icon
            GameObject buffIcon = Instantiate(buffIconPrefab, buffIconContainer);
            Image iconImage = buffIcon.GetComponent<Image>();
            iconImage.sprite = buff.upgradeIcon;
        }
    }
}
