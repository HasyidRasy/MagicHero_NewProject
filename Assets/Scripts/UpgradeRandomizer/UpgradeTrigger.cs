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
        upgradeManager.LoadSelectedUpgrades();
        upgradeManager.ApplyUpgradesToCharacter(characterModel);;
    }

    public void OnButtonClick()
    {
        upgradeManager.SaveSelectedUpgrades();
        SceneManager.LoadScene(sceneToLoad);
    }
}
