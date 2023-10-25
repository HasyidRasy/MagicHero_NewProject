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
        //NewAudioManager.Instance.PlayBGM("MainMenu");
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
        }
    }

    public void DisableCreditPanel()
    {
        if (creditPanel != null)
        {
            creditPanel.SetActive(false);
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
        SceneManager.LoadScene(0);
    }

    public void GoToMainLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
