using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncyclopediaUI : MonoBehaviour
{

    public GameObject alienList;
    public GameObject alienPrefab;

    private List<Monster> monsterList;
    
    void Awake()
    {
        
    }

    void Update()
    {
        
    }

    void AddMessage()
    {
        GameObject newMessage = Instantiate(alienPrefab, alienList.transform);
    }

}
