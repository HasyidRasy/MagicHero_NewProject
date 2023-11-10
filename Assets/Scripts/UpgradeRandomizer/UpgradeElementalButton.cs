using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeElementalButton : MonoBehaviour
{
    public Image hoverImage;
    private UpgradeManager upgradeManager;
    private CharacterModel upgradedCharacter;
    private UpgradeData upgrade;
    public UpgradeElemental upgradeElemental;
    
    // Start is called before the first frame update
    void Start()
    {
        upgradeManager = FindObjectOfType<UpgradeManager>();
        upgradedCharacter = FindObjectOfType<CharacterModel>();

        if(upgradedCharacter == null)
        {
            Debug.Log("Characracter not found");
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
            UpgradeData selectedUpgrade = UpgradeManager.instance.GetSelectedUpgrade(upgrade.upgradeID);

            upgradedCharacter.ApplyUpgrade(upgrade);

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
