using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaiju : MonoBehaviour
{

    [Header("Kaiju Description")]
    public string name;
    public string description;
    public Sprite kaijuSprite;

    [Header("Kaiju Element")]
    public Sprite elementType;
    public string elementName;

    public bool isUnlocked;
    public bool isDNA;

    public Kaiju()
    {
        //Constructor for creating new objects
    }
}
