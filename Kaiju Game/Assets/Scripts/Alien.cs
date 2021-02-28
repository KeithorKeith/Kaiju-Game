using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{

    [Header("Alien Description")]
    public string name;
    public string hintBox1;
    public string hintBox2;
    public string hintBox3;

    public Sprite alienSprite;

    [Header("Alien Element")]
    public Sprite elementType;
    public string elementName;

    public int unlockLevel = 0;

    public Alien()
    {
        //Constructor for creating new objects
    }
}
