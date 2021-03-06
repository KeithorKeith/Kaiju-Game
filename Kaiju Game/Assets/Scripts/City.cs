using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class City : MonoBehaviour
{
    public GameObject dangerOverlay, destroyedOverlay;
    public Text healthText;
    public bool underAttack;
    public bool isDestroyed = false;
    public string cityName;

    private int health = 3;
    private int attackCountdown;

    public Kaiju defenseKaiju;
    public Alien attackingAlien;

    public void ToggleUnderAttack(bool newStatus, int countdown=0)
    {
        underAttack = newStatus;
        dangerOverlay.SetActive(newStatus);
        attackCountdown = countdown;
    }
    public void ToggleisDestroyed(bool newStatus)
    {
        isDestroyed = newStatus;
        destroyedOverlay.SetActive(newStatus);
        healthText.text = "DESTROYED";
    }

    public bool AdvanceAttack(List<string> messageList)
    {
        bool successfullDefense = false;
        
        if(defenseKaiju == null)
        {
            health--;
            if(health <= 0)
            {
                messageList.Add($"{cityName} was destroyed!");
                ToggleisDestroyed(true);
            }
            else
            {
                messageList.Add($"{cityName} took 1 damage because it wasn't defended");
                healthText.text = $"Health: {health}";
            }
        }
        else
        {
            if (attackingAlien.unlockLevel < 3)
            {
                attackingAlien.unlockLevel++;
                messageList.Add($"New information was discovered about the alien {attackingAlien.name} while defending {cityName}!");
            }

            if (defenseKaiju.elementName.ToLower() == attackingAlien.elementName.ToLower())
            {
                messageList.Add($"The Kaiju defending {cityName} appeared to be effective against the {attackingAlien.name}! The city was defended!");
                successfullDefense = true;
                ToggleUnderAttack(false);
            }
            else
            {
                messageList.Add($"The Kaiju defending {cityName} was not effective against the {attackingAlien.name} and has been defeated!");
            }

            Destroy(defenseKaiju);
            defenseKaiju = null;
        }
        attackCountdown--;

        if(underAttack && attackCountdown <= 0 && !isDestroyed)
        {
            messageList.Add($"The attack on {cityName} has subsided and it is no longer under attack.");
            ToggleUnderAttack(false);
        }
        return successfullDefense;
    }

    public bool CheckAttackStatus(List<string> messageList)
    {
        bool successfullAttack = false;
        messageList.Add($"You mount an attack on {cityName}.");

        if (attackingAlien.unlockLevel < 3)
        {
            attackingAlien.unlockLevel++;
            messageList.Add($"New information was discovered about the alien {attackingAlien.name} while attacking {cityName}!");
        }

        if (defenseKaiju.elementName.ToLower() == attackingAlien.elementName.ToLower())
        {
            messageList.Add($"The Kaiju attacking {cityName} appeared to be effective against the {attackingAlien.name}! {cityName} was destroyed!");
            successfullAttack = true;
            ToggleUnderAttack(false);
            ToggleisDestroyed(true);
        }
        else
        {
            messageList.Add($"The Kaiju attacking {cityName} was not effective against the {attackingAlien.name} and has been defeated!");
        }

        Destroy(defenseKaiju);
        defenseKaiju = null;

        return successfullAttack;
    }
}
