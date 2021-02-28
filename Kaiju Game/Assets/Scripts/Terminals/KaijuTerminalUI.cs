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
    public List<Kaiju> dnaMasterList;
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
        
        descriptionText.text = "";
        titleText.text = "";

        foreach (Kaiju kaiju in KaijuMasterList)
        {
            kaiju.isUnlocked = false;
        }

        for (int i=0; i<2; i++)
        {
            GameObject firstDNA = Instantiate(dnaMasterList[0].gameObject);
            Kaiju firstDNAScript = firstDNA.GetComponent<Kaiju>();
            currentKaiju.Add(firstDNAScript);
        }

        RenderKaijuEncyclopedia();
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


    void RenderKaijuEncyclopedia()
    {
        foreach (Transform child in kaijuList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Kaiju kaiju in KaijuMasterList)
        {
            AddKaijuToUI(kaiju);
        }
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
            entry.GetComponentInChildren<Text>().text = mykaiju.name;
            int x = new int();
            x = i;
            entry.GetComponent<Button>().onClick.AddListener(delegate { SetKaijuForBreeding(kaijuPosition, x); });
        }

    }

    public void SetKaijuForBreeding(int breedingPosition, int kaijuPosition)
    {
        breedingResultText.text = "";
        breedKaijuSelect.SetActive(false);

        if (activeBreedingKaiju[breedingPosition] != null)
        {
            currentKaiju.Add(activeBreedingKaiju[breedingPosition]);
        }

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

        string newElement = GetBredElement(activeBreedingKaiju[0], activeBreedingKaiju[1]);


        if (newElement == "invalid")
        {
            if(activeBreedingKaiju[0] != null)
            {
                currentKaiju.Add(activeBreedingKaiju[0]);
            }
            if (activeBreedingKaiju[1] != null)
            {
                currentKaiju.Add(activeBreedingKaiju[1]);
            }

            activeBreedingKaiju[0] = null;
            activeBreedingKaiju[1] = null;
            breedingKaijuButtons[0].GetComponent<Image>().sprite = null;
            breedingKaijuButtons[1].GetComponent<Image>().sprite = null;

            breedingResultText.text = "Invalid combination, kaiju have been returned to your party";
            RenderMyKaijuUI();
            return;
        }

        breedingResultText.text = "New egg created! ";
        currentEggs.Add(new Egg(2, newElement));
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

        int newKaijuIndex = 0;
        for(int i=0; i < KaijuMasterList.Count; i++)
        {
            if(KaijuMasterList[i].elementName.ToLower() == egg.element)
            {
                newKaijuIndex = i;
            }
        }
        KaijuMasterList[newKaijuIndex].isUnlocked = true;
        RenderKaijuEncyclopedia();
        GameObject newKaiju = Instantiate(KaijuMasterList[newKaijuIndex].gameObject);
        Kaiju newKaijuScript = newKaiju.GetComponent<Kaiju>();
        currentKaiju.Add(newKaijuScript);
        RenderMyKaijuUI();

        hatchPopup.SetActive(true);
        hatchImage.sprite = newKaijuScript.kaijuSprite;

    }

    private string GetBredElement(Kaiju first, Kaiju second)
    {
        string newElement = "invalid";
        string firstElement = "none";
        string secondElement = "none";
        if(first != null) { firstElement = first.elementName.ToLower(); }
        if (second != null) { secondElement = second.elementName.ToLower(); }

        //Individual elements
        if ((firstElement == "fire" && secondElement == "none") || (secondElement == "fire" && firstElement == "none"))
        {
            newElement = "fire";
        }
        else if ((firstElement == "ice" && secondElement == "none") || (secondElement == "ice" && firstElement == "none"))
        {
            newElement = "ice";
        }
        else if ((firstElement == "stone" && secondElement == "none") || (secondElement == "stone" && firstElement == "none"))
        {
            newElement = "stone";
        }
        else if ((firstElement == "electric" && secondElement == "none") || (secondElement == "electric" && firstElement == "none"))
        {
            newElement = "electric";
        }
        else if ((firstElement == "wind" && secondElement == "none") || (secondElement == "wind" && firstElement == "none"))
        {
            newElement = "wind";
        }
        // Combinations
        else if ((firstElement == "ice" && secondElement == "fire") || (secondElement == "ice" && firstElement == "fire"))
        {
            newElement = "water";
        }
        else if ((firstElement == "ice" && secondElement == "wind") || (secondElement == "ice" && firstElement == "wind"))
        {
            newElement = "blizzard";
        }
        else if ((firstElement == "electric" && secondElement == "wind") || (secondElement == "electric" && firstElement == "wind"))
        {
            newElement = "thunder";
        }
        else if ((firstElement == "fire" && secondElement == "stone") || (secondElement == "stone" && firstElement == "fire"))
        {
            newElement = "magma";
        }
        return newElement;
    }

}
