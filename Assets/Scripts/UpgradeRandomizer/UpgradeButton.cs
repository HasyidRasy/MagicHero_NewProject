using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UpgradeButton : MonoBehaviour
{
    public UpgradeRandomizer upgradeRandomizer;
    public Image hoverImage;
    private UpgradeManager upgradeManager;
    private CharacterModel upgradedCharacter;
    private UpgradeData upgrade;
    public static int id = 1;
    private RectTransform rectTransform;
    [SerializeField] private float hoverHighValue = 10f;


    void Start()
    {
        ResetId();
        upgradeManager = FindObjectOfType<UpgradeManager>();
        upgradedCharacter = FindObjectOfType<CharacterModel>();
        rectTransform = GetComponent<RectTransform>();

        if (upgradedCharacter == null)
        {
            Debug.Log("Characracter not found");
            return;
        }
        
        if(upgradeManager == null)
        {
            Debug.Log("Upgrade not found");
            return;
        }
           
        hoverImage.gameObject.SetActive(false);
    }
    public static void ResetId() {
        id = 1;
    }
    public void SetUpgrade(UpgradeData upgradeData)
    {
        upgrade = upgradeData;
    }

    public void OnButtonClick()
    {
        if (upgrade != null)
        {
            upgradedCharacter.ApplyUpgrade(upgrade);
            upgradedCharacter.chosenUpgrades.Add(upgrade);
            Debug.Log("Upgrade Name: " + upgrade.upgradeName);
            Debug.Log("Upgrade Description: " + GetUpgradeDescription(upgrade));
            Invoke(nameof(SetFalseUpgradeCanvas), 0.5f);
            Time.timeScale = 1f;
            GameEvents.current.DoorwayTriggerEnter(id);
            id++;
        }
    }

    void SetFalseUpgradeCanvas() {
        upgradeRandomizer.gameObject.SetActive(false);
    }

    private string GetUpgradeDescription(UpgradeData upgrade)
    {
        string description = "";
        foreach (var stat in upgrade.stats)
        {
            if(stat.upgradeValueStatic > 0)
            {
                description += stat.upgradeType.ToString() + " +" + stat.upgradeValueStatic + "\n";
            }
            else if(stat.upgradeValueStatic < 0)
            {
                description += stat.upgradeType.ToString() + " -" + stat.upgradeValueStatic + "\n";
            }
        }

        if(upgrade.upgradeDesc != null)
        {
            description += upgrade.upgradeDesc;
        }
        return description;
    }

    public void OnPointerEnterButton()
    {
        // Activate the hover image here
        hoverImage.gameObject.SetActive(true);
        rectTransform.DOAnchorPosY(-390f + hoverHighValue, .5f)
                     .SetUpdate(true);
    }

    public void OnPointerExitButton()
    {
        // Deactivate the hover image here
        hoverImage.gameObject.SetActive(false);
        rectTransform.DOAnchorPosY(-390f, .5f)
                     .SetUpdate(true);
    }

}
