using UnityEngine;
using UnityEngine.UI;

public class ElementSwitchSystem : MonoBehaviour
{
    [SerializeField] private Sprite[] elementSprites = new Sprite[3]; // Array sprite untuk elemen
    [SerializeField] private Image[] elementIndicatorImages = new Image[3]; // Array Image untuk menampilkan elemen saat ini

    private ElementalType element; // Elemen button
    public GameObject elementPanel;
    private NewPlayerController1 playerController;
    public int currentButtonIndex;

    private void Awake()
    {
        // Cari dan simpan referensi ke NewPlayerController1 saat awal mulai
        playerController = FindObjectOfType<NewPlayerController1>();
    }
    //Mengatur elemen proyektil
    public void SetElement(ElementalType newElement)
    {
        element = newElement;

        switch (element)
        {
            case ElementalType.Water:
                playerController.SetAttackPattern(newElement);
                break;
            case ElementalType.Fire:
                playerController.SetAttackPattern(newElement);
                break;
            case ElementalType.Wind:
                playerController.SetAttackPattern(newElement);
                break;
        }
    }
    public void OnFireButtonClick()
    {
        // Saat tombol Fire diklik, atur elemen ke Fire.
        SetElement(ElementalType.Fire);
        UpdateAttackPatternIndicator();
    }

    public void OnWaterButtonClick()
    {
        // Saat tombol Water diklik, atur elemen ke Water.
        SetElement(ElementalType.Water);
        UpdateAttackPatternIndicator();
    }

    public void OnWindButtonClick()
    {
        // Saat tombol Wind diklik, atur elemen ke Wind.
        SetElement(ElementalType.Wind);
        UpdateAttackPatternIndicator();
    }

    public void UpdateAttackPatternIndicator()
    {
        ElementalType[] attackPattern = playerController.GetCurrentAttackPattern();
        //int currentAttackIndex = playerController.GetCurrentAttackIndex();

        for (int i = 0; i < elementIndicatorImages.Length; i++)
        {
            switch (attackPattern[i])
            {
                case ElementalType.Fire:
                    elementIndicatorImages[i].sprite = elementSprites[0];
                    break;
                case ElementalType.Water:
                    elementIndicatorImages[i].sprite = elementSprites[1];
                    break;
                case ElementalType.Wind:
                    elementIndicatorImages[i].sprite = elementSprites[2];
                    break;
            }
        }
    }
    public void EnableElementPanel(int buttonIndex)
    {
        currentButtonIndex = buttonIndex;
        Debug.Log("current button index" + currentButtonIndex);
        if (elementPanel != null)
        {
            elementPanel.SetActive(true);
            //SetElement((ElementalType)buttonIndex);
            //Debug.Log("button index " + buttonIndex);
        }
    }
    public void DisableElementPanel()
    {
        if (elementPanel != null)
        {
            elementPanel.SetActive(false);
        }
    }
}
