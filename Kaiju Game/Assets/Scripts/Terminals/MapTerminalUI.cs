using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTerminalUI : MonoBehaviour
{

    public City[] defenseCities;

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


}
