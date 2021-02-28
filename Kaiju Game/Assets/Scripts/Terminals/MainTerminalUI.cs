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
    public static bool gameOver = false;
    public static bool gameWon = false;

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
        if(gameOver || gameWon)
        {
            return;
        }
        PlayerController.canMove = false;
        foreach (GameObject gobj in hideDuringMessages)
        {
            gobj.SetActive(false);
        }
        currentTurn++;
        titleText.text = $"Report Turn {currentTurn}";
        hudTurnText.text = $"Turn {currentTurn}";

        List<string> turnMessages = new List<string>();

        turnMessages.Add($"Started turn {currentTurn}");

        List<string> mapMessages = mapTerminalUI.CheckStatus(currentTurn);
        foreach(string msg in mapMessages)
        {
            turnMessages.Add(msg);
        }
        int citiesAttacked = 0;
        switch (currentTurn)
        {
            case 3:
                citiesAttacked = mapTerminalUI.AttackCities(1, currentTurn);
                break;
            case 8:
                citiesAttacked = mapTerminalUI.AttackCities(1, currentTurn);
                break;
            case 13:
                citiesAttacked = mapTerminalUI.AttackCities(2, currentTurn);
                break;
            case 15:
                citiesAttacked = mapTerminalUI.AttackCities(2, currentTurn);
                break;
            case 17:
                citiesAttacked = mapTerminalUI.AttackCities(2, currentTurn);
                break;
            default:
                if(currentTurn > 20)
                {
                    citiesAttacked = mapTerminalUI.AttackCities(3, currentTurn);
                }
                break;
        }

        if(citiesAttacked > 0)
        {
            if(citiesAttacked == 1)
            {
                turnMessages.Add($"{citiesAttacked} city came under attack!");
            }
            else
            {
                turnMessages.Add($"{citiesAttacked} cities came under attack!");
            }
        }

        List<string> kaijuMessages = kaijuTerminalUI.AdvanceTurn();
        foreach (string msg in kaijuMessages)
        {
            turnMessages.Add(msg);
        }

        turnMessages.Add("Turn ended");
        if (gameOver)
        {
            turnMessages.Add("Game Over! All cities were destroyed!");
        }else if (gameWon)
        {
            turnMessages.Add("YOU WIN! All alien bases were destroyed!");
        }
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
        PlayerController.canMove = true;
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
