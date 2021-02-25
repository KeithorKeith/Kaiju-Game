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
        AddMessage("Nothing happened...");
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
