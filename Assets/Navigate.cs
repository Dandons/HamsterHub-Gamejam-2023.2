using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigate : MonoBehaviour
{
    public GameObject playerInterface;
    public GameObject GachaInterface;
    public GameObject DefenceInterface;
    public void ChangeInterface(int index)
    {
        if(index == 0)
        {
            playerInterface.SetActive(true);
            GachaInterface.SetActive(false);
            DefenceInterface.SetActive(false);
        }
        if(index == 1)
        {
            playerInterface.SetActive(false);
            GachaInterface.SetActive(true);
            DefenceInterface.SetActive(false);
        }
        if(index == 2)
        {
            playerInterface.SetActive(false);
            GachaInterface.SetActive(false);
            DefenceInterface.SetActive(true);
        }
    }
}
