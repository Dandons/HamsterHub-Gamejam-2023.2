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
    private int[] FilterByRarity(CompanionProperty.Rarity rarity)
    {
        List<int> companionByRarity = new List<int>();
        for (int i = 0; i < companionGacha.Length; i++)
        {
            if (companionGacha[i].GetComponent<Companion>().rarity == rarity)
            {
                companionByRarity.Add(i);
            }
        }
        return companionByRarity.ToArray();
    }
    private GameObject RandomCompanion()
    {
        int[] companion;
        int rate = (int)UnityEngine.Random.Range(1f, 101f);
        if (rate >= 95)
        {
            companion = FilterByRarity(CompanionProperty.Rarity.SuperRare);
        }
        else if (rate >= 80)
        {
            companion = FilterByRarity(CompanionProperty.Rarity.Rare);
        }
        else if (rate >= 50)
        {
            companion = FilterByRarity(CompanionProperty.Rarity.Uncommon);
        }
        else
        {
            companion = FilterByRarity(CompanionProperty.Rarity.Common);
        }
        int character = (int)UnityEngine.Random.Range(0, companion.Length);
        int companionIndex = companion[character];
        Debug.Log(companionGacha[companionIndex].GetComponent<Companion>().name);
        return companionGacha[companionIndex];
    }

    public void RollCompanion()
    {
        if(Player.Instance.Coin - 100 >= 0)
        {
            Player.Instance.Coin -= 100;
            GameObject companion =  Instantiate(RandomCompanion());
            resultInterface.transform.GetChild(0).GetComponent<Image>().sprite = companion.GetComponent<SpriteRenderer>().sprite;
            resultInterface.transform.GetChild(1).GetComponent<TMP_Text>().text = companion.GetComponent<Companion>().name;
            StartCoroutine(ShowResult());
        }
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
