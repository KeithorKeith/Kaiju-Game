using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTerminalUI : MonoBehaviour
{

    public GameObject messagePrefab;
    public GameObject messageList;

    // Start is called before the first frame update
    void Start()
    {
        AddMessage();
        AddMessage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddMessage()
    {
        Instantiate(messagePrefab, messageList.transform);
    }
}
