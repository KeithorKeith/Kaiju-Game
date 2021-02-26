using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTerminalUI : MonoBehaviour
{

    public City[] defenseCities;
    public GameObject battleScreen, kaijuSelectScreen;

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
        battleScreen.SetActive(true);
        highlightedCity = cityIndex;
    }

    public void OpenKaijuSelect()
    {
        kaijuSelectScreen.SetActive(true);
    }

    public void ChooseKaiju()
    {
        kaijuSelectScreen.SetActive(false);
        kaijuSprite.sprite = null;
        defenseCities[highlightedCity].defenseKaiju = null;
    }

    public void LockInBattle()
    {
        battleScreen.SetActive(false);
        highlightedCity = -1;
    }

}
