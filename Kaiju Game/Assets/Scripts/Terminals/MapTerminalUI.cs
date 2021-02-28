using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTerminalUI : MonoBehaviour
{

    public City[] defenseCities;
    public City[] attackBases;
    private City[] activeList;
    public GameObject kaijuDisplayPrefab;
    public GameObject battleScreen, kaijuSelectScreen, kaijuSelectGrid;
    public KaijuTerminalUI kaijuTerminalUI;
    public EncyclopediaUI alienTerminalUI;

    public Image kaijuSprite, alienSprite;
    public Text kaijuText, alienText;
    private int highlightedCity;

    public void Start()
    {
        SetupAttackCities();

    }

    public void SetupAttackCities()
    {
        int[] attackingAliens = { 5, 6, 7, 8 };
        for (int i=0; i < attackBases.Length; i++)
        {
            City attackCity = attackBases[i];
            attackCity.ToggleUnderAttack(true);
            attackCity.attackingAlien = alienTerminalUI.alienMasterList[attackingAliens[i]];
        }
    }

    public List<string> CheckStatus(int turnNum)
    {
        List<string> result = new List<string>();
        foreach (City city in defenseCities)
        {
            if (city.underAttack && !city.isDestroyed)
            {
                bool success = city.AdvanceAttack(result);
                int[] rewards = { 0, 1 };
                if (success)
                {
                    rewards = GetRewards(turnNum);
                    result.Add("You gained DNA samples from defeating the alien!");
                }
                else
                {
                    result.Add("You were able to recover some DNA from your fallen kaiju after your loss.");
                }
                for (int i = 0; i < rewards.Length; i++)
                {
                    GameObject newDNA = Instantiate(kaijuTerminalUI.dnaMasterList[rewards[i]].gameObject);
                    kaijuTerminalUI.currentKaiju.Add(newDNA.GetComponent<Kaiju>());
                    kaijuTerminalUI.RenderMyKaijuUI();
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
            MainTerminalUI.gameOver = true;
        }

        // Attack Start
        foreach (City city in attackBases)
        {
            if (city.underAttack && !city.isDestroyed && city.defenseKaiju != null)
            {
                bool success = city.CheckAttackStatus(result);
                if (success)
                {
                    int[] rewards = GetAttackRewards(turnNum);
                    for (int i = 0; i < rewards.Length; i++)
                    {
                        GameObject newDNA = Instantiate(kaijuTerminalUI.dnaMasterList[rewards[i]].gameObject);
                        kaijuTerminalUI.currentKaiju.Add(newDNA.GetComponent<Kaiju>());
                        kaijuTerminalUI.RenderMyKaijuUI();
                    }
                    result.Add("You gained rare DNA samples from defeating the alien base!");
                }
            }
        }
        bool allAttackDestroyed = true;
        foreach (City city in attackBases)
        {
            if (!city.isDestroyed)
            {
                allAttackDestroyed = false;
            }
        }
        if (allAttackDestroyed)
        {
            result.Add("ALL ALIEN BASES DESTROYED. YOU WIN");
            MainTerminalUI.gameWon = true;
        }
        return result;
    }

    public int AttackCities(int count, int currentTurn)
    {
        int citiesAttacked = 0;

        for(int i=0; i< defenseCities.Length; i++)
        {
            if( i < count)
            {
                if (!defenseCities[i].underAttack && !defenseCities[i].isDestroyed)
                {
                    // Only attack city if it is not already under attack
                    int alienIndex = GetAlienIndex(currentTurn);
                    defenseCities[i].attackingAlien = alienTerminalUI.alienMasterList[alienIndex];
                    defenseCities[i].ToggleUnderAttack(true, 3);
                    citiesAttacked++;
                }
                else
                {
                    count++;
                }
            }
        }
        return citiesAttacked;
    }

    public void OpenBattle(int cityIndex)
    {
        if (defenseCities[cityIndex].underAttack && !defenseCities[cityIndex].isDestroyed)
        {
            battleScreen.SetActive(true);
            highlightedCity = cityIndex;
            activeList = defenseCities;
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

    public void OpenAttackBattle(int cityIndex)
    {
        if (!attackBases[cityIndex].isDestroyed)
        {
            battleScreen.SetActive(true);
            highlightedCity = cityIndex;
            activeList = attackBases;
            alienSprite.sprite = attackBases[highlightedCity].attackingAlien.alienSprite;
            alienText.text = $"Name: {attackBases[highlightedCity].attackingAlien.name}";

            if (attackBases[highlightedCity].defenseKaiju != null)
            {
                kaijuSprite.sprite = attackBases[highlightedCity].defenseKaiju.kaijuSprite;
                kaijuText.text = $"Name: {attackBases[highlightedCity].defenseKaiju.name}";
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
        
        if (activeList[highlightedCity].defenseKaiju != null)
        {
            kaijuTerminalUI.currentKaiju.Add(activeList[highlightedCity].defenseKaiju);
        }

        activeList[highlightedCity].defenseKaiju = kaiju;
        kaijuTerminalUI.currentKaiju.Remove(kaiju);
        kaijuTerminalUI.RenderMyKaijuUI();
    }

    public void LockInBattle()
    {
        battleScreen.SetActive(false);
        highlightedCity = -1;
    }

    private int[] GetRewards(int turnNum)
    {
        if(turnNum < 13)
        {
            return new int[] { 0, 0, 0, 1, 1 };
        }else if(turnNum < 20)
        {
            return new int[] { 0, 0, 0, 1, 1, 1, 2, 2 };
        }
        else if (turnNum < 30)
        {
            return new int[] { 0, 0, 0, 1, 1, 1, 2, 2, 3, 3 };
        }
        else
        {
            return new int[] { 0, 0, 1, 1, 2, 2, 3, 3, 3, 4, 4 };
        }
        
    }
    private int[] GetAttackRewards(int turnNum)
    {
        if (turnNum < 13)
        {
            return new int[] { 0, 0, 1, 1, 2, 2 };
        }
        else if (turnNum < 20)
        {
            return new int[] { 1, 1, 2, 2, 3, 3 };
        }
        else
        {
            return new int[] { 0, 0, 1, 1, 2, 2, 3, 3, 3, 4, 4 };
        }

    }


    private int GetAlienIndex(int currentTurn)
    {
        if (currentTurn < 5) { return 0; }
        else if (currentTurn < 10) { return 1; }
        else if (currentTurn < 15) { return Random.Range(0, 3); }
        else if(currentTurn < 30) { return Random.Range(0, 4); }
        else
        {
            return Random.Range(0, alienTerminalUI.alienMasterList.Count);
        }
    }

}
