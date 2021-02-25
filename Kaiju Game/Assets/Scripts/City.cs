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

    public void AdvanceAttack(List<string> messageList)
    {
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
        attackCountdown--;

        if(attackCountdown <= 0 && !isDestroyed)
        {
            messageList.Add($"{cityName} is no longer under attack");
            ToggleUnderAttack(false);
        }
    }
}
