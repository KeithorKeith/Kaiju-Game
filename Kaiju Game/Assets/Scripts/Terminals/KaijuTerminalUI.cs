using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KaijuTerminalUI : MonoBehaviour
{


    public GameObject kaijuList;
    public GameObject kaijuPrefab;

    public List<Kaiju> KaijuMasterList;
    public Text descriptionText;
    public Text titleText;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Kaiju kaiju in KaijuMasterList)
        {
            AddKaijuToUI(kaiju);
        }

        descriptionText.text = KaijuMasterList[0].description;
        titleText.text = KaijuMasterList[0].name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddKaijuToUI(Kaiju kaiju)
    {
        GameObject newKaiju = Instantiate(kaijuPrefab, kaijuList.transform);
        Image kaijuImage = newKaiju.GetComponent<Image>();
        kaijuImage.sprite = kaiju.kaijuSprite;
        KaijuUIEntry UIEntry = newKaiju.GetComponent<KaijuUIEntry>();
        UIEntry.terminalUI = this;
        UIEntry.kaiju = kaiju;
    }

}
