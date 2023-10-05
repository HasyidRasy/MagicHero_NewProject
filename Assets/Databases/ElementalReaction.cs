using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementalReaction", menuName = "Elemental Reaction")]
public class ElementalReaction : ScriptableObject 
{
    public string reactionName;
    public Element elementA;
    public Element elementB;
    public string resultReaction;   
}
