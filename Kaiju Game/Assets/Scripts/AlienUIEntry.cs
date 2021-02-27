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
        if (alien.isUnlocked1)
        {
            terminalUI.hintBox1.text = alien.hintBox1;
            terminalUI.hintBox2.text = alien.hintBox2;
            terminalUI.hintBox3.text = alien.hintBox3;

            terminalUI.titleText.text = alien.name;
        }

    }
}
