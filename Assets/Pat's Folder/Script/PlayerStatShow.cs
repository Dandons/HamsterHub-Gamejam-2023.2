using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatShow : Singleton<PlayerStatShow>
{
    public TMP_Text hp;
    public TMP_Text mp;
    public TMP_Text atk;
    public TMP_Text def;
    public TMP_Text upHp;
    public TMP_Text upMp;
    public TMP_Text upAtk;
    public TMP_Text upDef;

    private void Update()
    {
        hp.GetComponent<TMP_Text>().text = "Hp Level : " + Player.Instance.playerProperty.HpLv;
        mp.GetComponent<TMP_Text>().text = "Mp Level : " + Player.Instance.playerProperty.MpLv;
        atk.GetComponent<TMP_Text>().text = "Atk Level : " + Player.Instance.playerProperty.AtkLv;
        def.GetComponent<TMP_Text>().text = "Def Level : " + Player.Instance.playerProperty.DefLv;
        upHp.GetComponent<TMP_Text>().text = "Next Lv! (" + 10 * Player.Instance.playerProperty.HpLv + " coin)";
        upMp.GetComponent<TMP_Text>().text = "Next Lv! (" + 10 * Player.Instance.playerProperty.MpLv + " coin)";
        upAtk.GetComponent<TMP_Text>().text = "Next Lv! (" + 10 * Player.Instance.playerProperty.AtkLv + " coin)";
        upDef.GetComponent<TMP_Text>().text = "Next Lv! (" + 10 * Player.Instance.playerProperty.DefLv + " coin)";
    }
}
