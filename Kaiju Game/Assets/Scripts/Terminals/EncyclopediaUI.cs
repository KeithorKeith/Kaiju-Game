using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaUI : MonoBehaviour
{

    public GameObject alienList;
    public GameObject alienPrefab;

    public Text titleText;
    public Text hintBox1;
    public Text hintBox2;
    public Text hintBox3;

    public List<Alien> alienMasterList;
    
    void Awake()
    {
        foreach (Alien alien in alienMasterList)
        {
            AddAlienToUI(alien);
        }
    }

    void Update()
    {
        
    }

    void AddMessage()
    {
        GameObject newMessage = Instantiate(alienPrefab, alienList.transform);
    }

    void AddAlienToUI(Alien alien)
    {
        GameObject newAlien = Instantiate(alienPrefab, alienList.transform);
        Image alienImage = newAlien.GetComponent<Image>();
        alienImage.sprite = alien.alienSprite;
        AlienUIEntry UIEntry = newAlien.GetComponent<AlienUIEntry>();
        UIEntry.terminalUI = this;
        UIEntry.alien = alien;
    }

}
