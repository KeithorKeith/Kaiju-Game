using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienUIEntry : MonoBehaviour
{

    public EncyclopediaUI terminalUI;
    public Alien alien;
    public GameObject hiddenBox;


    public void AlienButtonClick()
    {
        if (alien.unlockLevel >= 1)
        {
            terminalUI.hintBox1.text = alien.hintBox1;
            terminalUI.titleText.text = alien.name;
        }
        else
        {
            terminalUI.hintBox1.text = "";
        }

        if (alien.unlockLevel >= 2)
        {
            terminalUI.hintBox2.text = alien.hintBox2;
        }
        else
        {
            terminalUI.hintBox2.text = "";
        }

        if (alien.unlockLevel >= 3)
        {
            terminalUI.hintBox3.text = alien.hintBox3;
        }
        else
        {
            terminalUI.hintBox3.text = "";
        }

    }
}
