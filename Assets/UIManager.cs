using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject deathPanel;

    private void OnEnable()
    {
        PlayerController.OnPlayerDeath += EnabledeathPanel;
    }
    private void OnDisable()
    {
        PlayerController.OnPlayerDeath -= EnabledeathPanel;
    }

    public void EnabledeathPanel()
    {
        deathPanel.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
