using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TesAppliedElement : MonoBehaviour
{
    public PlayerElementController pElementController;

    public Button FireButton;
    public Button WindButton;
    public Button WaterButton;

    public Element FireElement;
    public Element WindElement;
    public Element WaterElement;

    private void Start() 
    {
        FireButton.onClick.AddListener(ChangeFire);
        WindButton.onClick.AddListener(ChangeWind);
        WaterButton.onClick.AddListener(ChangeWater);
    }

    public void ChangeFire()
    {
       if (pElementController.currentElement == null) 
       {
            pElementController.ChangeElement(FireElement);
       }
       else
       {
            pElementController.HandleElementalInteraction(FireElement);
       }
       
    }

    public void ChangeWind()
    {
       if (pElementController.currentElement == null) 
       {
            pElementController.ChangeElement(WindElement);
       }
       else
       {
            pElementController.HandleElementalInteraction(WindElement);
       }
    }

    public void ChangeWater()
    {
        if (pElementController.currentElement == null) 
       {
            pElementController.ChangeElement(WaterElement);
       }
       else
       {
            pElementController.HandleElementalInteraction(WaterElement);
       }
    }
}
