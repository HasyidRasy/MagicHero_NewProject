using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static event Action OnRestart;

    public GameObject deathPanel;
    public GameObject creditPanel;
    public GameObject controlPanel;
    public GameObject confirmPanel;
    public GameObject switchElementPanel;
    private bool isConfirmPanelActive = false;
    private bool isSwitchElementPanelActive = false;
    private ElementSwitchSystem elementSwitchSystem;
    private NewPlayerController1 newPlayerController1;
    private InventoryManagement inventoryManagement;
    private UIAnimationManager animationManager;

    private bool isDeath = false;

    private void Start() {
        NewAudioManager.Instance.bgmSource.Stop();
        NewAudioManager.Instance.PlayBGM("MainMenu");
        elementSwitchSystem = GetComponent<ElementSwitchSystem>();
        newPlayerController1 = GetComponent<NewPlayerController1>();
        inventoryManagement = GetComponent<InventoryManagement>();
        animationManager = GetComponent<UIAnimationManager>();
    }

    private void OnEnable()
    {
        NewPlayerController1.OnPlayerDeath += EnableDeathPanel;
    }
    private void OnDisable()
    {
        NewPlayerController1.OnPlayerDeath -= EnableDeathPanel;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isConfirmPanelActive)
        {
            Pause();
            EnableConfirmPanel();
        } else if (Input.GetKeyDown(KeyCode.Escape) && isConfirmPanelActive) {
            Continue();
            DisableConfirmPanel();
        }
        if (Input.GetKeyDown(KeyCode.Tab) && !isSwitchElementPanelActive)
        {
            EnableSwitchElementPanel();
            inventoryManagement.UpdateBuffDisplay(CharacterModel.Instance.chosenUpgrades);
        }
        else if(Input.GetKeyDown(KeyCode.Tab) && isSwitchElementPanelActive)
        {
            DisableSwitchElementPanel();
        }
    }

    public void EnableDeathPanel()
    {
        if (deathPanel != null && isDeath == false)
        {
            isDeath = true;
            animationManager.TransitionDeathPanel();
            deathPanel.SetActive(true);
            NewAudioManager.Instance.bgmSource.Stop();
            NewAudioManager.Instance.PlaySFX("Death");
        }
    }

    public void EnableCreditPanel()
    {
        if (creditPanel != null)
        {
            creditPanel.SetActive(true);
            NewAudioManager.Instance.bgmSource.Stop();
            NewAudioManager.Instance.PlayBGM("Battle");
        }
    }

    public void DisableCreditPanel()
    {
        if (creditPanel != null)
        {
            creditPanel.SetActive(false);
            NewAudioManager.Instance.bgmSource.Stop();
            NewAudioManager.Instance.PlayBGM("MainMenu");
        }
    }

    public void EnableControlPanel()
    {
        if (controlPanel != null)
        {
            controlPanel.SetActive(true);
        }
    }

    public void DisableControlPanel()
    {
        if (creditPanel != null)
        {
            controlPanel.SetActive(false);
        }
    }

    public void EnableConfirmPanel()
    {
        if (confirmPanel != null)
        {
            confirmPanel.SetActive(true);
        }
    }

    public void DisableConfirmPanel()
    {
        if (confirmPanel != null)
        {
            confirmPanel.SetActive(false);
        }
    }

    public void EnableSwitchElementPanel()
    {
        if (switchElementPanel != null)
        {
            elementSwitchSystem.UpdateAttackPatternIndicator();
            switchElementPanel.SetActive(true);
            Pause();
            isSwitchElementPanelActive = true;
        }
    }
    public void DisableSwitchElementPanel()
    {
        if (switchElementPanel != null)
        {
            switchElementPanel.SetActive(false);
            elementSwitchSystem.DisableElementPanel();
            Continue();
            isSwitchElementPanelActive = false;
        }
    }


    public void Pause() {
        Time.timeScale = 0;
    }

    public void Continue() {
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        OnRestart?.Invoke();
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToStory() {
        SceneManager.LoadScene("Story");
    }

    public void GoToTutorial() {
        SceneManager.LoadScene("Tutorial");
    }

    public void GoToMainLevel()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
