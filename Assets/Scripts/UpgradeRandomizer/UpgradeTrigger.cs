using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeTrigger : MonoBehaviour
{
    public string sceneToLoad;

    public UpgradeRandomizer upgradeRandomizer;

    public UpgradeManager upgradeManager;

    private void Start()
    {
        CharacterModel characterModel = CharacterModel.Instance;

        if(characterModel == null)
        {
            Debug.Log("Character no found");
            return;
        }

        // Load selected upgrades from PlayerPrefs when the game starts
        upgradeManager.LoadSelectedUpgrades();
        // Apply upgrades to the character model
        upgradeManager.ApplyUpgradesToCharacter(characterModel);
        
        //upgradeRandomizer.gameObject.SetActive(false);
    }

    public void OnButtonClick()
    {
        // Save selected upgrades to PlayerPrefs
        upgradeManager.SaveSelectedUpgrades();

        // Load the next scene
        SceneManager.LoadScene(sceneToLoad);//upgradeRandomizer.gameObject.SetActive(true);

        //upgradeRandomizer.onRandomizeUpgrades.Invoke();
        //upgradeRandomizer.onUpdateUI.Invoke();
    }
}
