using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthbarPlayer : MonoBehaviour
{
    private CharacterModel characterModel;
    public GameObject player;
    private float maxHP;

    private Slider healtBarSlider;

    // Fading Healthbar
    private GameObject healthBarPlayerUI;
    private Slider fadeHealthSlider;
    private float lerpSpeed = 0.05f;

    private void Awake()
    {
        characterModel = player.GetComponent<CharacterModel>();
        healtBarSlider = GetComponent<Slider>();

        healthBarPlayerUI = this.gameObject;
        fadeHealthSlider = healthBarPlayerUI.transform.GetChild(0).GetComponent<Slider>();
        maxHP = characterModel.healthPoint;

    }

    private void Update()
    {
        SetHealthbarPlayer();

        if (healtBarSlider.value != fadeHealthSlider.value)
        {
            fadeHealthSlider.value = Mathf.Lerp(fadeHealthSlider.value, healtBarSlider.value, lerpSpeed);
        }
    }

    private void SetHealthbarPlayer()
    {
        healtBarSlider.value = characterModel.healthPoint / maxHP;
    }
}
