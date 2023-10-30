using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject deathPanel;
    public GameObject creditPanel;
    public GameObject controlPanel;
    public GameObject confirmPanel;
    private bool isConfirmPanelActive = false;

    private void Start() {
        NewAudioManager.Instance.bgmSource.Stop();
        NewAudioManager.Instance.PlayBGM("MainMenu");
    }

    private void OnEnable()
    {
        PlayerController.OnPlayerDeath += EnableDeathPanel;
    }
    private void OnDisable()
    {
        PlayerController.OnPlayerDeath -= EnableDeathPanel;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isConfirmPanelActive)
        {
            Time.timeScale = 0;
            EnableConfirmPanel();
        }
    }

    public void EnableDeathPanel()
    {
        if (deathPanel != null)
        {
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
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
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
