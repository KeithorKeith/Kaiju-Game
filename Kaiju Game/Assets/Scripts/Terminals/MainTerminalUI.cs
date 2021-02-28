using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTerminalUI : MonoBehaviour
{
    // Prefabs
    public GameObject messagePrefab;

    // References to UI Components
    public GameObject messageList;
    public GameObject[] hideDuringMessages;
    public Text titleText;
    public Text hudTurnText;

    // References to other Scene objects
    public MapTerminalUI mapTerminalUI;
    public KaijuTerminalUI kaijuTerminalUI;
    public EncyclopediaUI alienTerminalUI;

    // Variables
    private int currentTurn = 1;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Alien alien in alienTerminalUI.alienMasterList)
        {
            alien.unlockLevel = 0;
        }
        AddMessage("Game started");
        AddMessage("Start turn 1");
    }

    public void NextTurn()
    {
        foreach (GameObject gobj in hideDuringMessages)
        {
            gobj.SetActive(false);
        }
            currentTurn++;
        titleText.text = $"Report Turn {currentTurn}";
        hudTurnText.text = $"Turn {currentTurn}";

        List<string> turnMessages = new List<string>();

        turnMessages.Add($"Started turn {currentTurn}");

        List<string> mapMessages = mapTerminalUI.CheckStatus();
        foreach(string msg in mapMessages)
        {
            turnMessages.Add(msg);
        }

        switch (currentTurn)
        {
            case 3:
                mapTerminalUI.AttackCities(1);
                turnMessages.Add("1 city came under attack!");
                break;
            case 8:
                mapTerminalUI.AttackCities(1);
                turnMessages.Add("1 city came under attack!");
                break;
            case 13:
                mapTerminalUI.AttackCities(2);
                turnMessages.Add("2 cities came under attack!");
                break;
        }

        List<string> kaijuMessages = kaijuTerminalUI.AdvanceTurn();
        foreach (string msg in kaijuMessages)
        {
            turnMessages.Add(msg);
        }

        turnMessages.Add("Turn ended");
        StartCoroutine(DisplayMessages(turnMessages));
    }

    IEnumerator DisplayMessages(List<string> messages)
    {
        foreach (Transform child in messageList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        
        foreach (string msg in messages)
        {
            AddMessage(msg);
            yield return new WaitForSeconds(0.5f);
        }
        foreach (GameObject gobj in hideDuringMessages)
        {
            gobj.SetActive(true);
        }
    }

    void AddMessage(string messageText)
    {
        GameObject newMessage = Instantiate(messagePrefab, messageList.transform);
        Text newText = newMessage.GetComponentInChildren<Text>();
        if (newText)
        {
            newText.text = messageText;
        }
    }
}
