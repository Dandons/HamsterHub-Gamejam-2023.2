using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RollGacha : MonoBehaviour
{
    public GameObject[] companionGacha;
    public GameObject[] FilterByRarity (CompanionProperty.Rarity rarity)
    {
        GameObject[] companionByRarity = null;
        for (int i = 0; i < companionGacha.Length; i++)
        {
            if (companionGacha[i].GetComponent<Companion>().rarity == rarity)
            {
                companionByRarity.Append(companionGacha[i]);
            }
        }
        return companionByRarity;
    }
    void RecruitCompanion()
    {
        GameObject[] companion;
        int rate = ((int)Random.Range(1f, 101f));
        if(rate > 95 )
        {
            companion = FilterByRarity(CompanionProperty.Rarity.SuperRare);
        }
        else if(rate > 80)
        {
            companion = FilterByRarity(CompanionProperty.Rarity.Rare);
        }
        else if(rate >50)
        {
            companion = FilterByRarity(CompanionProperty.Rarity.Uncommon);
        }
        else
        {
            companion = FilterByRarity(CompanionProperty.Rarity.Common);
        }
        int character = (int)Random.Range(0,companion.Length);
        
    }
}
