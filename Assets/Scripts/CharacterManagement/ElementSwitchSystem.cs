using UnityEngine;
using UnityEngine.UI;

public class ElementSwitchSystem : MonoBehaviour
{
    [SerializeField] private Sprite[] elementSprites = new Sprite[3];
    [SerializeField] private Image[] elementIndicatorImages = new Image[3];
    [SerializeField] private GameObject[] elementPanel = new GameObject[3];
    [SerializeField] private Button[] elementButton = new Button[3];
    public ElementUnlockedInfo unlockedElementInfo = new ElementUnlockedInfo();
    
    private ElementalType element;
    private int elementIndex;
    private NewPlayerController1 playerController;
    
    [System.Serializable] 
    public class ElementUnlockedInfo
    {
        public bool isFireUnlocked = false;
        public bool isWaterUnlocked = false;
        public bool isWindUnlocked = false;
    }

    private void Awake()
    {
        playerController = FindObjectOfType<NewPlayerController1>();
    }

    private void Start()
    {
        elementIndex = elementPanel.Length;
    }

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
        SetElement(ElementalType.Fire);
        playerController.ResetAttackIndex();
        UpdateAttackPatternIndicator();
    }
    public void OnWaterButtonClick()
    {
        SetElement(ElementalType.Water);
        playerController.ResetAttackIndex();
        UpdateAttackPatternIndicator();
    }
    public void OnWindButtonClick()
    {
        SetElement(ElementalType.Wind);
        playerController.ResetAttackIndex();
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
        playerController.currentButtonIndex = buttonIndex;
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
        //if (elementPanel != null)
        //{
        //    for (int i = 0; i < elementIndex; i++)
        //    {
        //        elementPanel[i].SetActive(false);
        //    }
        //}
    }
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
    public void SaveElementStatus()
    {
        PlayerPrefs.SetInt("IsFireUnlocked", unlockedElementInfo.isFireUnlocked ? 1 : 0);
        PlayerPrefs.SetInt("IsWaterUnlocked", unlockedElementInfo.isWaterUnlocked ? 1 : 0);
        PlayerPrefs.SetInt("IsWindUnlocked", unlockedElementInfo.isWindUnlocked ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LoadElementStatus()
    {
        unlockedElementInfo.isFireUnlocked = PlayerPrefs.GetInt("IsFireUnlocked", 0) == 1;
        unlockedElementInfo.isWaterUnlocked = PlayerPrefs.GetInt("IsWaterUnlocked", 0) == 1;
        unlockedElementInfo.isWindUnlocked = PlayerPrefs.GetInt("IsWindUnlocked", 0) == 1;
    }
    public void SetDefaultElementStatus()
    {
        unlockedElementInfo.isFireUnlocked = true;
        unlockedElementInfo.isWaterUnlocked = false;
        unlockedElementInfo.isWindUnlocked = false;
        SaveElementStatus();
    }
}
