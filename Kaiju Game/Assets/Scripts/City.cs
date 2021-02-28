using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public GameObject dangerOverlay, destroyedOverlay;
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
            messageList.Add($"{cityName} is no longer under attack");
            ToggleUnderAttack(false);
        }
        return successfullDefense;
    }
}
