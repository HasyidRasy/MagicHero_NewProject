using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UpgradeButton : MonoBehaviour
{
    public UpgradeRandomizer upgradeRandomizer;
    private GraphicRaycaster canvasRaycast;

    public Image hoverImage;
    private UpgradeManager upgradeManager;
    private CharacterModel upgradedCharacter;
    private UpgradeList upgradeList;
    private UpgradeData upgrade;
    public static int id = 1;
    private RectTransform rectTransform;
    [SerializeField] private float hoverHighValue = 10f;


    void Start()
    {
        ResetId();
        upgradeManager = FindObjectOfType<UpgradeManager>();
        upgradedCharacter = FindObjectOfType<CharacterModel>();
        rectTransform = GetComponent<RectTransform>();
        upgradeList = FindObjectOfType<UpgradeList>();

        canvasRaycast= GetComponentInParent<GraphicRaycaster>();

        if (upgradedCharacter == null)
        {
            Debug.Log("Characracter not found");
            return;
        }
        
        if(upgradeManager == null)
        {
            Debug.Log("Upgrade not found");
            return;
        }
           
        hoverImage.gameObject.SetActive(false);
    }
    public static void ResetId() {
        id = 1;
    }
    public void SetUpgrade(UpgradeData upgradeData)
    {
        upgrade = upgradeData;
    }

    public void OnButtonClick()
    {
        if (upgrade != null)
        {
            NewAudioManager.Instance.PlayUpgradeSFX("UpgradeSuccess");
            upgradedCharacter.ApplyUpgrade(upgrade);
            upgradeList.chosenUpgrades.Add(upgrade);
            Debug.Log("Upgrade Name: " + upgrade.upgradeName);
            Debug.Log("Upgrade Description: " + GetUpgradeDescription(upgrade));

            canvasRaycast.enabled = false;

            Invoke(nameof(SetFalseUpgradeCanvas), 1f);
            ClickAnimation();

            Invoke(nameof(EnableCanvasAfterDelay), 1f);

            Time.timeScale = 1f;
            GameEvents.current.DoorwayTriggerEnter(id);
            id++;
        }
    }
    void EnableCanvasAfterDelay() {
        canvasRaycast.enabled = true;
        Debug.Log("canvas activated again");
    }
        public void ClickAnimation() {
        rectTransform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f)
                     .OnComplete(() => {
                         rectTransform.DOScale(new Vector3(1f, 1f, 1f), 0f);
                     });
        rectTransform.DOPunchRotation(new Vector3(0f, 180f, 0f), 1f, 0, 1f)
                     .SetEase(Ease.OutBack)
                     .OnComplete(() => {
                     rectTransform.DOPunchRotation(new Vector3(0f, 0f, 0f), 0f, 0, 0f);
                     });
    }

    void SetFalseUpgradeCanvas() {
        upgradeRandomizer.gameObject.SetActive(false);
    }

    private string GetUpgradeDescription(UpgradeData upgrade)
    {
        string description = "";
        foreach (var stat in upgrade.stats)
        {
            if(stat.upgradeValueStatic > 0)
            {
                description += stat.upgradeType.ToString() + " +" + stat.upgradeValueStatic + "\n";
            }
            else if(stat.upgradeValueStatic < 0)
            {
                description += stat.upgradeType.ToString() + " -" + stat.upgradeValueStatic + "\n";
            }
        }

        if(upgrade.upgradeDesc != null)
        {
            description += upgrade.upgradeDesc;
        }
        return description;
    }

    public void OnPointerEnterButton()
    {
        NewAudioManager.Instance.PlaySFX("Hover");
        hoverImage.gameObject.SetActive(true);
        rectTransform.DOAnchorPosY(-390f + hoverHighValue, .5f)
                     .SetUpdate(true);
    }

    public void OnPointerExitButton()
    {
        hoverImage.gameObject.SetActive(false);
        rectTransform.DOAnchorPosY(-390f, .5f)
                     .SetUpdate(true);
    }

}
