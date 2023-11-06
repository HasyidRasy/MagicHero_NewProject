using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Element", menuName = "Element")]
public class Element : ScriptableObject
{
    public string elementName;
    public int elementID;
    public ElementalType elementEnum;
    public Sprite elementSprite;
}