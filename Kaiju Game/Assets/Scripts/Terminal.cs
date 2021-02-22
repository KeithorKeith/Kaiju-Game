using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public GameObject attachedUI;

    public void Activate()
    {
        Debug.Log("Activated!");
        attachedUI.SetActive(true);
    }
}
