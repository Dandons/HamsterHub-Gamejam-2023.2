using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RollGacha : MonoBehaviour
{
    public GameObject resultInterface;
    public GameObject[] companionGacha;
    private int[] FilterByRarity (CompanionProperty.Rarity rarity)
    {
        int[] companionByRarity = new int[companionGacha.Length];
        for (int i = 0; i < companionGacha.Length; i++)
        {
            if (companionGacha[i].GetComponent<Companion>().rarity == rarity)
            {
                //Debug.Log()
                companionByRarity.Append(i);
            }
        }
        return companionByRarity;
    }
    private GameObject RandomCompanion()
    {
        int[] companion;
        int rate = ((int)UnityEngine.Random.Range(1f, 101f));
        if(rate > 95 )
        {
            companion = (FilterByRarity(CompanionProperty.Rarity.SuperRare));
        }
        else if(rate > 80)
        {
            companion = (FilterByRarity(CompanionProperty.Rarity.Rare));
        }
        else if(rate >50)
        {
            companion = FilterByRarity(CompanionProperty.Rarity.Uncommon);
        }
        else
        {
            companion = (FilterByRarity(CompanionProperty.Rarity.Common));
        }
        int character = (int)UnityEngine.Random.Range(0,companion.Length);
        
        return companionGacha[companion[character]];   
    }

    public void RollCompanion()
    {
        GameObject companion =  Instantiate(RandomCompanion());

        resultInterface.transform.GetChild(0).GetComponent<Image>().sprite = companion.GetComponent<SpriteRenderer>().sprite;
        resultInterface.transform.GetChild(1).GetComponent<TMP_Text>().text = companion.GetComponent<Companion>().name;


        StartCoroutine(ShowResult());

    }

    IEnumerator ShowResult()
    {
        resultInterface.SetActive(true);
        yield return new WaitUntil(()=>(getKey()!=KeyCode.None));
        resultInterface.SetActive(false);
    }
    private KeyCode getKey()
    {
        
        foreach (KeyCode keyPress in System.Enum.GetValues(typeof(KeyCode)))
        {
            
            if (Input.GetKeyDown(keyPress))
            {
                return keyPress;
            }
        }
        return KeyCode.None;
        
    }
}
