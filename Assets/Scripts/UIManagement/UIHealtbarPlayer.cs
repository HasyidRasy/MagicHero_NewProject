using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealtbarPlayer : MonoBehaviour
{
    private CharacterModel characterModel;
    public GameObject player;
    private float maxHP;

    private Slider healtBarSlider;

    private void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //characterModel = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterModel>();
        characterModel = player.GetComponent<CharacterModel>();
        healtBarSlider = GetComponent<Slider>();
        maxHP = characterModel.healthPoint;
    }

    private void Update()
    {
        SetHealthbarPlayer();
        //Debug.Log(maxHP);
    }

    private void SetHealthbarPlayer()
    {
        healtBarSlider.value = characterModel.healthPoint / maxHP;
    }
}
