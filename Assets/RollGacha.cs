using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RollGacha : MonoBehaviour
{
    public GameObject resultInterface;
    public GameObject[] companionGacha;
    private GameObject[] FilterByRarity (CompanionProperty.Rarity rarity)
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
    private GameObject RandomCompanion()
    {
        GameObject[] companion;
        int rate = ((int)UnityEngine.Random.Range(1f, 101f));
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
        int character = (int)UnityEngine.Random.Range(0,companion.Length);
        return companion[character];   
    }

    public void RollCompanion()
    {
        GameObject companion =  Instantiate(RandomCompanion());
        resultInterface.SetActive(true);

    }
    IEnumerable ShowResult()
    {
        //yield return new WaitUntil(()=>Input.Get);
    }
    private string getKey()
    {
        foreach (KeyCode keyPress in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyPress))
            {
                string key = keyPress.ToString();
                String[] spearator = { "Alpha" };
                String[] strlist = key.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                return strlist[0];
            }
        }
        return null;
    }
}
