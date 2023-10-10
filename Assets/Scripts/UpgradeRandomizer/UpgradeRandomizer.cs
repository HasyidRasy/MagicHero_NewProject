using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class UpgradeRandomizer : MonoBehaviour
{
    public static UpgradeRandomizer Instance {get; private set;}
    public UpgradeDatabase upgradeDatabase;
    public UpgradeManager upgradeManager;
    public TMP_Text[] upgradeNameText;
    public TMP_Text[] upgradeDescText;
    public Image[] upgradeImage;
    public Button[] upgradeButtons;
    public Button upgradeSkip;


    public UnityEvent onUITrigger;

    private List<UpgradeData> availableUpgrades = new List<UpgradeData>();
    private UpgradeData[] randomizedUpgrades = new UpgradeData[3];
    private CharacterModel upgradedCharacter;

    // Start is called before the first frame update
    void Start()
    {
        upgradedCharacter = FindObjectOfType<CharacterModel>();

        if(upgradedCharacter == null)
        {
            Debug.Log("Characracter not found");
            return;
        }

        gameObject.SetActive(false);

        availableUpgrades.AddRange(upgradeDatabase.upgrades);
        RandomizeUpgrades();
        UpdateUI();
    }

    public void RandomizeUpgrades()
    {   
        if (availableUpgrades.Count < 3)
        {
            Debug.LogError("Not enough unique upgrades available.");
            return;
        }

        // Shuffle the available upgrades list.
        ShuffleList(availableUpgrades);

        // Select the first three upgrades from the shuffled list.
        for (int i = 0; i < 3; i++)
        {
            randomizedUpgrades[i] = availableUpgrades[i];
        }

        foreach (var upgrade in randomizedUpgrades)
        {
            upgradedCharacter.ApplyUpgrade(upgrade);
        }

        onUITrigger.Invoke();
    }

    private void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = 0; i < n; i++)
        {
            int r = i + Random.Range(0, n - i);
            T temp = list[i];
            list[i] = list[r];
            list[r] = temp;
        }
    }


    // Update is called once per frame
    public void UpdateUI()
    {
        for(int i = 0; i < 3;i++)
        {
            upgradeNameText[i].text = randomizedUpgrades[i].upgradeName;
            upgradeDescText[i].text = randomizedUpgrades[i].upgradeType.ToString() + " +" + randomizedUpgrades[i].upgradeValue;
            upgradeImage[i].sprite = randomizedUpgrades[i].upgradeIcon;
        
            UpgradeButton upgradeButton = upgradeButtons[i].GetComponent<UpgradeButton>();
            upgradeButton.SetUpgrade(randomizedUpgrades[i]);

            upgradeButtons[i].interactable = true;
            
            onUITrigger.Invoke();

            foreach(var upgrade in randomizedUpgrades)
            {
                upgradeManager.AddSelectedUpgrade(upgrade);
            }
        }
    }
}
