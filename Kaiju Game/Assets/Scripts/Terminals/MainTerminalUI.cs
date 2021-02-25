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
    public Text titleText;

    // References to other Scene objects
    public MapTerminalUI mapTerminalUI;

    // Variables
    private int currentTurn = 1;

    // Start is called before the first frame update
    void Start()
    {
        AddMessage("Game started");
        AddMessage("Start turn 1");
    }

    public void NextTurn()
    {
        currentTurn++;
        titleText.text = $"Report Turn {currentTurn}";
        AddMessage($"Started turn {currentTurn}");

        List<string> mapMessages = mapTerminalUI.CheckStatus();
        foreach(string msg in mapMessages)
        {
            AddMessage(msg);
        }

        switch (currentTurn)
        {
            case 3:
                mapTerminalUI.AttackCities(1);
                AddMessage("1 city came under attack!");
                break;
            case 8:
                mapTerminalUI.AttackCities(1);
                AddMessage("1 city came under attack!");
                break;
            case 13:
                mapTerminalUI.AttackCities(2);
                AddMessage("2 cities came under attack!");
                break;
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
