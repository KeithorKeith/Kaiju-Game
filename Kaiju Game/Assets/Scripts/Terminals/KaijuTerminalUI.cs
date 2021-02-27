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
    public Text breedingResultText;

    // Egg UI
    public List<Egg> currentEggs;
    public GameObject eggGrid;
    public GameObject eggDisplayPrefab;
    public GameObject hatchPopup;
    public Image hatchImage;

    void Start()
    {
        currentEggs = new List<Egg>();
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

        GameObject scndKaiju = Instantiate(KaijuMasterList[1].gameObject);
        Kaiju scndKaijuScript = scndKaiju.GetComponent<Kaiju>();
        currentKaiju.Add(scndKaijuScript);

        RenderMyKaijuUI();
    }

    public List<string> AdvanceTurn()
    {
        //TODO: Advance eggs
        List<string> result = new List<string>();

        foreach(Egg egg in currentEggs)
        {
            egg.countDown--;
            if(egg.countDown <= 0)
            {
                result.Add("An egg is ready to hatch! Open the egg UI to see the result!");
            }
        }

        RenderEggUI();

        return result;
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
        RenderMyKaijuUI();
    }

    public void AttemptBreeding()
    {
        if(activeBreedingKaiju[0] == null && activeBreedingKaiju[1] == null)
        {
            breedingResultText.text = "No kaiju selected for breeding!";
            return;
        }

        currentEggs.Add(new Egg(2, "fire"));
        activeBreedingKaiju[0] = null;
        activeBreedingKaiju[1] = null;
        breedingKaijuButtons[0].GetComponent<Image>().sprite = null;
        breedingKaijuButtons[1].GetComponent<Image>().sprite = null;
        RenderEggUI();
        RenderMyKaijuUI();
    }

    public void RenderEggUI()
    {
        // Clear out old display
        foreach (Transform child in eggGrid.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        // Render each egg
        for (int i=0; i<currentEggs.Count; i++)
        {
            GameObject newEgg = Instantiate(eggDisplayPrefab, eggGrid.transform);
            if(currentEggs[i].countDown <= 0)
            {
                newEgg.GetComponentInChildren<Text>().text = "Ready to Hatch!";
                int x = new int();
                x = i;
                newEgg.GetComponentInChildren<Button>().onClick.AddListener(delegate { HatchEgg(currentEggs[x]); });
            }
            else
            {
                newEgg.GetComponentInChildren<Text>().text = $"{currentEggs[i].countDown} turns remaining";
                newEgg.GetComponentInChildren<Button>().enabled = false;
            }
        }
    }

    public void RenderMyKaijuUI()
    {
        // Clear out old display
        foreach (Transform child in myKaijuList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        
        foreach (Kaiju kaiju in currentKaiju)
        {
            AddToMyKaijuUI(kaiju);
        }
    }

    void HatchEgg(Egg egg)
    {
        currentEggs.Remove(egg);
        RenderEggUI();
        //TODO: Check which element should spawn
        GameObject newKaiju = Instantiate(KaijuMasterList[2].gameObject);
        Kaiju newKaijuScript = newKaiju.GetComponent<Kaiju>();
        currentKaiju.Add(newKaijuScript);
        RenderMyKaijuUI();

        hatchPopup.SetActive(true);
        hatchImage.sprite = newKaijuScript.kaijuSprite;

    }

}
