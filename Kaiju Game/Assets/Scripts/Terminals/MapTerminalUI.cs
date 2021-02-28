using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTerminalUI : MonoBehaviour
{

    public City[] defenseCities;
    public GameObject kaijuDisplayPrefab;
    public GameObject battleScreen, kaijuSelectScreen, kaijuSelectGrid;
    public KaijuTerminalUI kaijuTerminalUI;
    public EncyclopediaUI alienTerminalUI;

    public Image kaijuSprite, alienSprite;
    public Text kaijuText, alienText;
    private int highlightedCity;

    public List<string> CheckStatus()
    {
        List<string> result = new List<string>();
        foreach (City city in defenseCities)
        {
            if (city.underAttack && !city.isDestroyed)
            {
                bool success = city.AdvanceAttack(result);
                if (success)
                {
                    int[] rewards = { 0, 0, 0 };
                    for(int i=0; i<rewards.Length; i++)
                    {
                        GameObject newDNA = Instantiate(kaijuTerminalUI.dnaMasterList[rewards[i]].gameObject);
                        kaijuTerminalUI.currentKaiju.Add(newDNA.GetComponent<Kaiju>());
                    }
                    result.Add("You gained DNA samples from defeating the alien!");
                }
            }
        }

        bool allDestroyed = true;
        foreach (City city in defenseCities)
        {
            if (!city.isDestroyed)
            {
                allDestroyed = false;
            }
        }

        if (allDestroyed)
        {
            result.Add("ALL CITIES DESTROYED!!!!!");
        }
        return result;
    }

    public void AttackCities(int count)
    {
        for(int i=0; i<defenseCities.Length; i++)
        {
            if( i < count)
            {
                if (!defenseCities[i].underAttack && !defenseCities[i].isDestroyed)
                {
                    // Only attack city if it is not already under attack
                    defenseCities[i].attackingAlien = alienTerminalUI.alienMasterList[0];
                    defenseCities[i].ToggleUnderAttack(true, 3);
                }
                else
                {
                    count++;
                }
            }
        }
    }

    public void OpenBattle(int cityIndex)
    {
        if (defenseCities[cityIndex].underAttack)
        {
            battleScreen.SetActive(true);
            highlightedCity = cityIndex;
            alienSprite.sprite = defenseCities[highlightedCity].attackingAlien.alienSprite;
            alienText.text = $"Name: {defenseCities[highlightedCity].attackingAlien.name}";

            if(defenseCities[highlightedCity].defenseKaiju != null)
            {
                kaijuSprite.sprite = defenseCities[highlightedCity].defenseKaiju.kaijuSprite;
                kaijuText.text = $"Name: {defenseCities[highlightedCity].defenseKaiju.name}";
            }
            else
            {
                kaijuSprite.sprite = null;
                kaijuText.text = "Select a Kaiju to defend";
            }
        }
    }

    public void OpenKaijuSelect()
    {
        //Clean up old ones
        foreach (Transform child in kaijuSelectGrid.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        kaijuSelectScreen.SetActive(true);
        for (int i = 0; i < kaijuTerminalUI.currentKaiju.Count; i++){
            if (!kaijuTerminalUI.currentKaiju[i].isDNA)
            {
                GameObject newKaiju = Instantiate(kaijuDisplayPrefab, kaijuSelectGrid.transform);
                newKaiju.GetComponent<Image>().sprite = kaijuTerminalUI.currentKaiju[i].kaijuSprite;
                int x = new int();
                x = i;
                newKaiju.GetComponent<Button>().onClick.AddListener(delegate { ChooseKaiju(kaijuTerminalUI.currentKaiju[x]); });
            }
        }
    }

    public void ChooseKaiju(Kaiju kaiju)
    {
        kaijuSelectScreen.SetActive(false);
        kaijuSprite.sprite = kaiju.kaijuSprite;
        kaijuText.text = $"Name: {kaiju.name}";
        
        if (defenseCities[highlightedCity].defenseKaiju != null)
        {
            kaijuTerminalUI.currentKaiju.Add(defenseCities[highlightedCity].defenseKaiju);
        }

        defenseCities[highlightedCity].defenseKaiju = kaiju;
        kaijuTerminalUI.currentKaiju.Remove(kaiju);
        kaijuTerminalUI.RenderMyKaijuUI();
    }

    public void LockInBattle()
    {
        battleScreen.SetActive(false);
        highlightedCity = -1;
    }

}
