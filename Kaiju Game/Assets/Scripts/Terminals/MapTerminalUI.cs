using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTerminalUI : MonoBehaviour
{

    public City[] defenseCities;

    // Start is called before the first frame update
    void Start()
    {
        defenseCities[0].ToggleOverlay(true);
    }


}
