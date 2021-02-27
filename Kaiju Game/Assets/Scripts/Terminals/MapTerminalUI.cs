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
    private int highlightedCity;

    public List<string> CheckStatus()
    {
        List<string> result = new List<string>();
        foreach (City city in defenseCities)
        {
            if (city.underAttack && !city.isDestroyed)
            {
                city.AdvanceAttack(result);
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
                    GameObject newAlien = Instantiate(alienTerminalUI.alienMasterList[0].gameObject);
                    defenseCities[i].attackingAlien = newAlien.GetComponent<Alien>();
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
        }
    }

    public void OpenKaijuSelect()
    {
        kaijuSelectScreen.SetActive(true);
        for (int i = 0; i < kaijuTerminalUI.currentKaiju.Count; i++){
            GameObject newKaiju = Instantiate(kaijuDisplayPrefab, kaijuSelectGrid.transform);
            newKaiju.GetComponent<Image>().sprite = kaijuTerminalUI.currentKaiju[i].kaijuSprite;
            int x = new int();
            x = i;
            newKaiju.GetComponent<Button>().onClick.AddListener(delegate { ChooseKaiju(kaijuTerminalUI.currentKaiju[x]); });
        }
    }

    public void ChooseKaiju(Kaiju kaiju)
    {
        kaijuSelectScreen.SetActive(false);
        kaijuSprite.sprite = kaiju.kaijuSprite;
        defenseCities[highlightedCity].defenseKaiju = kaiju;
    }

    public void LockInBattle()
    {
        battleScreen.SetActive(false);
        highlightedCity = -1;
    }

}
