using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaijuUIEntry : MonoBehaviour
{

    public KaijuTerminalUI terminalUI;
    public Kaiju kaiju;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KaijuButtonClick()
    {
        terminalUI.descriptionText.text = kaiju.description;
        terminalUI.titleText.text = kaiju.name;
    }
}
