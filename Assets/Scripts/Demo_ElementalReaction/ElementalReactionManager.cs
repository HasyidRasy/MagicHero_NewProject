using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalReactionManager : MonoBehaviour
{
    public static ElementalReactionManager Instance; // Singleton instance

    public List<ElementalReaction> elementalReactions = new List<ElementalReaction>();

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

    // Function to check for an elemental reaction
    public string CheckElementalReaction(Element elementA, Element elementB)
    {
        foreach (ElementalReaction reaction in elementalReactions)
        {
            if ((reaction.elementA == elementA && reaction.elementB == elementB) || (reaction.elementA == elementB && reaction.elementB == elementA))
            {
                return reaction.resultReaction;
            }
        }
        return null; // No reaction found.
    }
}
