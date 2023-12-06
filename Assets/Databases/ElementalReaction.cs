using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementalReaction", menuName = "Elemental Reaction")]
public class ElementalReaction : ScriptableObject 
{
    public string reactionName;
    public Element elementA;
    public Element elementB;
    public ElementalType elementalTypeA;
    public ElementalType elementalTypeB;
    public string resultReaction;
    public int damageReaction;
    public float movespeedChange;
    public float reactionInterval;
    public float reactionDuration; 
    public bool stacking;
    public Sprite reactionSprite;

}
