using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GiftCode : MonoBehaviour
{
    private TMP_InputField inputField;
    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
    }
    private void Update()
    {
        if(inputField.text == "Patricia_so_cute")
        {
            Player.Instance.Coin = 9999;
        }
    }
}
