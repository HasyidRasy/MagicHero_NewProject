using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeElementalButton : MonoBehaviour
{
    public Image hoverImage;
    public int id;
    private UpgradeManager upgradeManager;
    //private CharacterModel upgradedCharacter;
    private UpgradeData upgrade;
    private UpgradeElemental upgradeElemental;
    private ElementSwitchSystem elementSwitchSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        upgradeElemental = FindObjectOfType<UpgradeElemental>();
        upgradeManager = FindObjectOfType<UpgradeManager>();
        elementSwitchSystem = FindObjectOfType<ElementSwitchSystem>();

        if(elementSwitchSystem == null)
        {
            Debug.Log("System not found");
            return;
        }
        
        if(upgradeManager == null)
        {
            Debug.Log("Upgrade not found");
            return;
        }

        if(upgrade.upgradeElement == ElementalType.Water)
        {
            hoverImage.color = new Color(86f / 255f, 139f / 255f, 200f / 255f);
        }
        else if(upgrade.upgradeElement == ElementalType.Wind)
        {
            hoverImage.color = new Color(0f / 255f, 169f / 255f, 157f / 255f);
        }
        else if(upgrade.upgradeElement == ElementalType.Fire)
        {
            hoverImage.color = new Color(199f / 255f, 56f / 255f, 56f / 255f);
        }

        hoverImage.gameObject.SetActive(false);
    }

   public void OnButtonClick()
    {
        Debug.Log("Button clicked");
        upgradeElemental.gameObject.SetActive(false);
        if (upgrade != null)
        {
            if (id == 0)
            {
                elementSwitchSystem.unlockedElementInfo.isWaterUnlocked = true;
            }
            else if (id == 1)
            {
                elementSwitchSystem.unlockedElementInfo.isWindUnlocked = true;               
            }
            else if (id == 2)
            {
                elementSwitchSystem.unlockedElementInfo.isFireUnlocked = true;
            }
        
            Debug.Log("Upgrade Name: " + upgrade.upgradeName);
            Debug.Log("Upgrade Description: " + upgradeElemental.GetUpgradeDescription(upgrade));
        
            Time.timeScale = 1f;
        }
    }

    public void SetUpgrade(UpgradeData upgradeData)
    {
        upgrade = upgradeData;
    }

 

    public void OnPointerEnterButton()
    {
        if((elementSwitchSystem.unlockedElementInfo.isWaterUnlocked == true && id == 0) ||
        (elementSwitchSystem.unlockedElementInfo.isWindUnlocked == true && id == 1) ||
        (elementSwitchSystem.unlockedElementInfo.isFireUnlocked == true && id == 2))
        {
            return;
        }
        else
        {
            hoverImage.gameObject.SetActive(true);
        }
    }

    public void OnPointerExitButton()
    {
        // Deactivate the hover image here
        hoverImage.gameObject.SetActive(false);
    }

}
