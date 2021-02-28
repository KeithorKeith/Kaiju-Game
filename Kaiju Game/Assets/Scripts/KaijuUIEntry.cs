using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaijuUIEntry : MonoBehaviour
{

    public KaijuTerminalUI terminalUI;
    public Kaiju kaiju;
    public GameObject hiddenBox;


    public void KaijuButtonClick()
    {
        if (kaiju.isUnlocked)
        {
            terminalUI.descriptionText.text = kaiju.description;
            terminalUI.titleText.text = kaiju.name;
            terminalUI.elementImage.sprite = kaiju.elementType;
        }

    }
}
