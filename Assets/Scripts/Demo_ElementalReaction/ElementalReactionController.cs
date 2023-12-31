using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElementalReactionController : MonoBehaviour
{
    //Popup Element
    public List<Element> elementScrptObj;
    private Element element;

    public static ElementalReactionController Instance; // Singleton instance

    public List<ElementalReaction> elementalReaction;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    
    public ElementalReaction CheckElementalReaction(ElementalType elementypeA, ElementalType elementypeB)
    {
        foreach (ElementalReaction reaction in elementalReaction)
        {
            if ((reaction.elementalTypeA == elementypeA && reaction.elementalTypeB == elementypeB) || (reaction.elementalTypeA == elementypeB && reaction.elementalTypeB == elementypeA))
            {
                return reaction;
            }
        }
        return null; // No reaction found.
    }

    public Element GetElementScrptObj(ElementalType elementalType)
    {
        foreach (Element elemen in elementScrptObj)
        {
            if (elemen.elementEnum == elementalType) 
            {
                return elemen;
            }
        }
        return null;
    }
}
