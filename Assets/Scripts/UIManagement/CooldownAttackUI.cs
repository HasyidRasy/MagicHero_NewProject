using UnityEngine;
using UnityEngine.UI;
using System;

public class CooldownAttackUI : MonoBehaviour
{

    private ElementalType element;
    [SerializeField] private Slider attackCooldownSlider;
    [SerializeField] private Image Element;
    [SerializeField] private Image backgroundElement;
    [SerializeField] private Sprite[] elementActive = new Sprite[3];
    [SerializeField] private Sprite[] backgroundElementActive = new Sprite[3];
    private NewPlayerController1 playerController1;

    public static Action OnWaterNext;
    public static Action OnFireNext;
    public static Action OnWindNext;
    void Start() {
        playerController1 = FindObjectOfType<NewPlayerController1>();
    }

    void Update() {
        attackCooldownSlider.value = Mathf.Lerp(0f, 1f, 1f - (playerController1.attackCooldown/ 
                                                              playerController1.timeBetweenAttacks));
    }
    public void SetElement(ElementalType currentElement) {
        element = currentElement;
        switch (element) {
            case ElementalType.Water:
                Element.sprite = elementActive[0];
                backgroundElement.sprite = backgroundElementActive[0];
                OnWaterNext?.Invoke();
                break;
            case ElementalType.Fire:
                Element.sprite = elementActive[1];
                backgroundElement.sprite = backgroundElementActive[1];
                OnFireNext?.Invoke();
                break;
            case ElementalType.Wind:
                Element.sprite = elementActive[2];
                backgroundElement.sprite = backgroundElementActive[2];
                OnWindNext?.Invoke();
                break;
        }
    }
}
