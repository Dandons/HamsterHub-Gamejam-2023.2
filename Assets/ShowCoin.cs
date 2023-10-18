using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowCoin : MonoBehaviour
{
    TMP_Text text;
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Curent coin : " + Player.Coin;
    }
}
