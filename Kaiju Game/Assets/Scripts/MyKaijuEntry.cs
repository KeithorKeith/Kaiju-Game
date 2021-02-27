using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyKaijuEntry : MonoBehaviour
{

    public Image sprite;
    public Text nameText, elementText;
 
    public void SetKaiju(Kaiju kaiju)
    {
        sprite.sprite = kaiju.kaijuSprite;
        nameText.text = kaiju.name;
        elementText.text = kaiju.elementName;
    }
}
