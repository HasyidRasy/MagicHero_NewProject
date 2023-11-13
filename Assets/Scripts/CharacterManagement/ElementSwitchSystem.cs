using UnityEngine;
using UnityEngine.UI;

public class ElementSwitchSystem : MonoBehaviour
{
    [SerializeField] private Sprite[] elementSprites = new Sprite[3]; // Array sprite untuk elemen
    [SerializeField] private Image[] elementIndicatorImages = new Image[3]; // Array Image untuk menampilkan elemen saat ini
    [SerializeField] private GameObject[] elementPanel = new GameObject[3];
    [SerializeField] private Button[] elementButton = new Button[3];
    public ElementUnlockedInfo unlockedElementInfo = new ElementUnlockedInfo();
    
    private ElementalType element;
    private int elementIndex;

    private NewPlayerController1 playerController;
    
    [HideInInspector]
    public int currentButtonIndex = 0;

    [System.Serializable] //[System.Serializable] akan dihapus setelah masa pengembangan
    public class ElementUnlockedInfo
    {
        public bool isFireUnlocked = false;
        public bool isWaterUnlocked = false;
        public bool isWindUnlocked = false;
    }

    /*
     * untuk mengubah kondisi unlock element dari script lain, dapat diatur seperti ini:
     * ElementSwitchSystem.unlockElement.isFireUnlocked = true; 
     * dan sebagainya, sesuaikan dengan kebutuhan
     */

    private void Awake()
    {
        // Cari dan simpan referensi ke NewPlayerController1 saat awal mulai
        playerController = FindObjectOfType<NewPlayerController1>();
        //GetComponent<Button>();
    }

    private void Start()
    {
        elementIndex = elementPanel.Length;
    }

    //Mengatur elemen proyektil
    public void SetElement(ElementalType newElement)
    {
        element = newElement;

        switch (element)
        {
            case ElementalType.Water:
                if (unlockedElementInfo.isWaterUnlocked) {
                    playerController.SetAttackPattern(newElement);
                }
                else {
                    Debug.Log("Element Water belum terbuka.");
                }

                break;
            case ElementalType.Fire:
                if (unlockedElementInfo.isFireUnlocked)
                {
                    playerController.SetAttackPattern(newElement);
                }
                else
                {
                    Debug.Log("Element Fire belum terbuka.");
                }
                break;
            case ElementalType.Wind:
                if (unlockedElementInfo.isWindUnlocked)
                {
                    playerController.SetAttackPattern(newElement);
                }
                else
                {
                    Debug.Log("Element Wind belum terbuka.");
                }
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
        if (elementPanel != null)
        {
            for (int i = 0; i < elementIndex; i++)
            {
                elementPanel[i].SetActive(true);
                elementUnlockedChecker();
            }
        }
    }
    public void DisableElementPanel()
    {
        if (elementPanel != null)
        {
            for (int i = 0; i < elementIndex; i++)
            {
                elementPanel[i].SetActive(false);
            }
        }
    }

    // Set interactable sesuai dengan status unlocked
    public void elementUnlockedChecker()
    {
        if (unlockedElementInfo.isFireUnlocked)
        {
            elementButton[0].interactable = true;
        } else
        {
            elementButton[0].interactable = false;
        }

        if (unlockedElementInfo.isWaterUnlocked)
        {
            elementButton[1].interactable = true;
        }
        else
        {
            elementButton[1].interactable = false;
        }

        if (unlockedElementInfo.isWindUnlocked)
        {
            elementButton[2].interactable = true;
        }
        else
        {
            elementButton[2].interactable = false;
        }
    }
}
