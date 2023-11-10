using System.Collections;
using System.Collections.Generic;
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
        

    }

   public void OnButtonClick()
    {
        Debug.Log("Button clicked");
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
        
            upgradeElemental.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void SetUpgrade(UpgradeData upgradeData)
    {
        upgrade = upgradeData;
    }

 

    public void OnPointerEnterButton()
    {
        // Activate the hover image here
        hoverImage.gameObject.SetActive(true);
    }

    public void OnPointerExitButton()
    {
        // Deactivate the hover image here
        hoverImage.gameObject.SetActive(false);
    }

}
