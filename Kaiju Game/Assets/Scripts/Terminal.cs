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
            if(Vector3.Distance(transform.position, player.transform.position) > PlayerController.INTERACTDISTANCE)
            {
                attachedUI.SetActive(false);
                player = null;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
    public void Activate(GameObject player)
    {
        // Activate this terminal's UI and enable the cursor
        attachedUI.SetActive(true);
        this.player = player;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
