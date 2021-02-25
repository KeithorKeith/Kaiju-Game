using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public GameObject dangerOverlay;

    public void ToggleOverlay(bool newStatus)
    {
        dangerOverlay.SetActive(newStatus);
    }
}
