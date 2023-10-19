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
        hp.text = "Hp Level : " + Player.Instance.playerProperty.HpLv;
        mp.text = "Mp Level : " + Player.Instance.playerProperty.MpLv;
        atk.text = "Atk Level : " + Player.Instance.playerProperty.AtkLv;
        def.text = "Def Level : " + Player.Instance.playerProperty.DefLv;
        upHp.text = "Next Lv! (" + 10 * Player.Instance.playerProperty.HpLv + " coin)";
        upMp.text = "Next Lv! (" + 10 * Player.Instance.playerProperty.MpLv + " coin)";
        upAtk.text = "Next Lv! (" + 10 * Player.Instance.playerProperty.AtkLv + " coin)";
        upDef.text = "Next Lv! (" + 10 * Player.Instance.playerProperty.DefLv + " coin)";
    }
}
