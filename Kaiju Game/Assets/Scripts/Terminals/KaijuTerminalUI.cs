using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaijuTerminalUI : MonoBehaviour
{


    public GameObject kaijuList;
    public GameObject kaijuPrefab;


    // Start is called before the first frame update
    void Start()
    {
        AddMessage();
        AddMessage();
        AddMessage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddMessage()
    {
        GameObject newMessage = Instantiate(kaijuPrefab, kaijuList.transform);
    }

}
