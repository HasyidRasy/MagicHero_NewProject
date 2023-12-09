using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHealthbarPlayer : MonoBehaviour
{
    private CharacterModel characterModel;
    public GameObject player;
    private float maxHP;
    public TMP_Text hpText;
    private string hpCurrent;
    private string hpMax;

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
        hpText = healthBarPlayerUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        //maxHP = characterModel.maxHealthPoint;
        hpCurrent = characterModel.healthPoint.ToString();
        hpMax = characterModel.maxHealthPoint.ToString();
    }

    private void Update()
    {
        SetHealthbarPlayer();

        if (healtBarSlider.value != fadeHealthSlider.value)
        {
            fadeHealthSlider.value = Mathf.Lerp(fadeHealthSlider.value, healtBarSlider.value, lerpSpeed);
        }

        SetAmountHP();
    }

    private void SetHealthbarPlayer()
    {
        healtBarSlider.value = characterModel.healthPoint / characterModel.maxHealthPoint;
    }

    private void SetAmountHP()
    {
        hpCurrent = characterModel.healthPoint.ToString();
        hpMax = characterModel.maxHealthPoint.ToString();
        if(characterModel.healthPoint < 0){
            hpCurrent = "0";
        }
        hpText.text = hpCurrent + " / " + hpMax;
    }
    
}
