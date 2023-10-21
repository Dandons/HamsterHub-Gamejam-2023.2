using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIText : MonoBehaviour
{
    public TMP_Text day;
    public TMP_Text coin;
    public TMP_Text tear;

    private void Update()
    {
        day.text = "Day : " + DayCount.days;
        coin.text = "Coin : " + Player.Instance.Coin;
        tear.text = "Tear : " + Player.Instance.Tear;
    }
}
