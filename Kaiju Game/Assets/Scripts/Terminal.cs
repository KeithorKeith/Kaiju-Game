using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public GameObject attachedUI;
    private GameObject player;

    private void Update()
    {
        if(player != null)
        {
            // If the player walks away remove the UI
            if(Vector3.Distance(transform.position, player.transform.position) > 5.0f)
            {
                attachedUI.SetActive(false);
                player = null;
            }
        }
    }
    public void Activate(GameObject player)
    {
        Debug.Log("Activated!");
        attachedUI.SetActive(true);
        this.player = player;
    }
}
