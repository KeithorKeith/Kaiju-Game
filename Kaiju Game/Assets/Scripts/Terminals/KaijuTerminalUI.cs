using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KaijuTerminalUI : MonoBehaviour
{


    public GameObject kaijuList, myKaijuList;
    public GameObject kaijuPrefab, myKaijuEntryPrefab;

    public List<Kaiju> KaijuMasterList;
    public List<Kaiju> currentKaiju;
    public Text descriptionText;
    public Text titleText;

    // Breeding UI
    public GameObject breedKaijuEntryPrefab;
    public GameObject breedKaijuSelect, breedKaijuList;
    public GameObject[] breedingKaijuButtons;
    private Kaiju[] activeBreedingKaiju;

    void Start()
    {
        activeBreedingKaiju = new Kaiju[breedingKaijuButtons.Length];
        foreach (Kaiju kaiju in KaijuMasterList)
        {
            AddKaijuToUI(kaiju);
        }

        descriptionText.text = KaijuMasterList[0].description;
        titleText.text = KaijuMasterList[0].name;

        GameObject firstKaiju = Instantiate(KaijuMasterList[0].gameObject);
        Kaiju firstKaijuScript = firstKaiju.GetComponent<Kaiju>();
        currentKaiju.Add(firstKaijuScript);
        AddToMyKaijuUI(firstKaijuScript);

        GameObject scndKaiju = Instantiate(KaijuMasterList[1].gameObject);
        Kaiju scndKaijuScript = scndKaiju.GetComponent<Kaiju>();
        currentKaiju.Add(scndKaijuScript);
        AddToMyKaijuUI(scndKaijuScript);
    }


    void AddKaijuToUI(Kaiju kaiju)
    {
        GameObject newKaiju = Instantiate(kaijuPrefab, kaijuList.transform);
        Image kaijuImage = newKaiju.GetComponent<Image>();
        kaijuImage.sprite = kaiju.kaijuSprite;
        KaijuUIEntry UIEntry = newKaiju.GetComponent<KaijuUIEntry>();
        UIEntry.terminalUI = this;
        UIEntry.kaiju = kaiju;
        if (kaiju.isUnlocked)
        {
            UIEntry.hiddenBox.SetActive(false);
        }
    }

    void AddToMyKaijuUI(Kaiju kaiju)
    {
        GameObject newKaiju = Instantiate(myKaijuEntryPrefab, myKaijuList.transform);
        MyKaijuEntry entryScript = newKaiju.GetComponent<MyKaijuEntry>();
        entryScript.SetKaiju(kaiju);
        
    }

    public void BreedButtonClick(int kaijuPosition)
    {
        breedKaijuSelect.SetActive(true);
        foreach (Transform child in breedKaijuList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i=0; i < currentKaiju.Count; i++)
        {
            Kaiju mykaiju = currentKaiju[i];
            GameObject entry = Instantiate(breedKaijuEntryPrefab, breedKaijuList.transform);
            entry.GetComponent<Image>().sprite = mykaiju.kaijuSprite;
            int x = new int();
            x = i;
            entry.GetComponent<Button>().onClick.AddListener(delegate { SetKaijuForBreeding(kaijuPosition, x); });
        }

    }

    public void SetKaijuForBreeding(int breedingPosition, int kaijuPosition)
    {
        breedKaijuSelect.SetActive(false);
        breedingKaijuButtons[breedingPosition].GetComponent<Image>().sprite = currentKaiju[kaijuPosition].kaijuSprite;
        activeBreedingKaiju[breedingPosition] = currentKaiju[kaijuPosition];
        currentKaiju.Remove(currentKaiju[kaijuPosition]);
        Debug.Log($"Chose kaiju {kaijuPosition} for breeding in {breedingPosition}");
    }

}
