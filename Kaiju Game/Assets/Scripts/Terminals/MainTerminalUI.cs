using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainTerminalUI : MonoBehaviour
{
    // Prefabs
    public GameObject messagePrefab;

    // References to UI Components
    public GameObject messageList;
    public GameObject[] hideDuringMessages;
    public Text titleText;
    public Text hudTurnText;
    public GameObject returnButton;

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
        returnButton.SetActive(false);
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

        List<string> kaijuMessages = kaijuTerminalUI.AdvanceTurn();
        foreach (string msg in kaijuMessages)
        {
            turnMessages.Add(msg);
        }

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
            case 19:
                citiesAttacked = mapTerminalUI.AttackCities(2, currentTurn);
                break;
            case 21:
                citiesAttacked = mapTerminalUI.AttackCities(1, currentTurn);
                break;
            case 23:
                citiesAttacked = mapTerminalUI.AttackCities(1, currentTurn);
                break;
            case 25:
                citiesAttacked = mapTerminalUI.AttackCities(1, currentTurn);
                break;
            case 27:
                citiesAttacked = mapTerminalUI.AttackCities(2, currentTurn);
                break;
            default:
                if(currentTurn > 30)
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



        turnMessages.Add("Turn ended");
        if (gameOver)
        {
            turnMessages.Add("Game Over! All cities were destroyed!");
            returnButton.SetActive(true);
        }
        else if (gameWon)
        {
            turnMessages.Add("YOU WIN! All alien bases were destroyed!");
            returnButton.SetActive(true);
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
            yield return new WaitForSeconds(1.0f);
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

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
