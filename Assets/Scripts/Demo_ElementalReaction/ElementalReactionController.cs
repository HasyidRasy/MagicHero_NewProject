using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementalType
{
    Water,
    Fire,
    Wind
}

public class ElementalReactionController : MonoBehaviour
{
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
}
