using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeRandomizer : MonoBehaviour
{
    public UpgradeDatabase upgradeDatabase;
    public TMP_Text[] upgradeNameText;
    public TMP_Text[] upgradeDescText;
    public Image[] upgradeImage;
    public Button upgradeButtons;
    

    private List<UpgradeData> availableUpgrades = new List<UpgradeData>();
    private UpgradeData[] randomizedUpgrades = new UpgradeData[3];

    // Start is called before the first frame update
    void Start()
    {
        availableUpgrades.AddRange(upgradeDatabase.upgrades);
        RandomizeUpgrades();
        UpdateUI();
    }

    private void RandomizeUpgrades()
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
    void UpdateUI()
    {
        for(int i = 0; i < 3;i++)
        {
            upgradeNameText[i].text = randomizedUpgrades[i].upgradeName;
            upgradeDescText[i].text = randomizedUpgrades[i].upgradeType.ToString() + " +" + randomizedUpgrades[i].upgradeValue;
            upgradeImage[i].sprite = randomizedUpgrades[i].upgradeIcon;
        }
    }
}
