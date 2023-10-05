using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElementController : MonoBehaviour
{
    public UIPlayerStatus uiStatusElement;

    public Element currentElement;

    // Function to change the player's current element
    public void ChangeElement(Element newElement)
    {
        currentElement = newElement;
        if (newElement == null)
        {
            uiStatusElement.appliedElement.text = " ";
        }
        uiStatusElement.appliedElement.text = currentElement.elementName;
    }

    // Function to handle elemental interactions
    public void HandleElementalInteraction(Element otherElement)
    {
        // Check for an elemental reaction between the player's element and the other element
        string resultReaction = ElementalReactionManager.Instance.CheckElementalReaction(currentElement, otherElement);

        if (resultReaction != null)
        {
            // Handle the reaction, e.g., apply damage, change visuals, etc.
            // You can define specific logic for each reaction in this function.
            HandleReaction(resultReaction);
        }
    }

    // Function to handle the reaction result
    private void HandleReaction(string resultReaction)
    {
        // Implement logic for the reaction, e.g., change player's appearance, apply effects, etc.
        uiStatusElement.resultReactName.text = resultReaction;
        ChangeElement(null);
    }
}
