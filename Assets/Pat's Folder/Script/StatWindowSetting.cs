using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatWindowSetting : MonoBehaviour
{
    public static StatWindowSetting Instance;

    //Companion Info
    public TMP_Text name;
    public TMP_Text description;
    public Image icon;
    public Image fullSprite;

    //Stat Window
    public TMP_Text statHp;
    public TMP_Text statMp;
    public TMP_Text statAtk;
    public TMP_Text statDef;
    public TMP_Text statSkill;

    //Stat&Skill Upgrade Button
    public Button upHp;
    public Button upMp;
    public Button upAtk;
    public Button upDef;
    public Button upSkill;

    private void Start()
    {
        Instance = this;
    }
    public void SetWindow(GameObject companion)
    {
        Companion companionProp = companion.GetComponent<Companion>();
        fullSprite.sprite = companion.GetComponent<SpriteRenderer>().sprite;
        
        ResetUpgradeButton();
        SetUpgradeButton(companionProp);
        SetStatText(companionProp);

    }
    [ContextMenu("SetCoin")]
    public void SetCoin()
    {
        Player.Instance.Coin = 999999;
    }
    public void SetStatText(Companion companionProp)
    {
        name.text = companionProp.name;
        description.text = companionProp.description;
        icon.sprite = companionProp.icon;
        statHp.text = "Hp Level : " + companionProp.companionProperty.HpLv;
        statMp.text = "Mp Level : " + companionProp.companionProperty.MpLv;
        statAtk.text = "Atk Level : " + companionProp.companionProperty.AtkLv;
        statDef.text = "Def Level : " + companionProp.companionProperty.DefLv;
        if (companionProp.skill.skillName.ToString() == "NoSkill")
        {
            statSkill.text = "character doesn't have skill";
        }
        else
        {
            statSkill.text = "Skill Level : " + companionProp.skillLv;
        }
        upHp.GetComponentInChildren<TMP_Text>().text = 10 * companionProp.companionProperty.HpLv + " coin for next Lv.";
        upMp.GetComponentInChildren<TMP_Text>().text = 10 * companionProp.companionProperty.MpLv + " coin for next Lv.";
        upAtk.GetComponentInChildren<TMP_Text>().text = 10 * companionProp.companionProperty.AtkLv + " coin for next Lv.";
        upDef.GetComponentInChildren<TMP_Text>().text = 10 * companionProp.companionProperty.DefLv + " coin for next Lv.";
        upSkill.GetComponentInChildren<TMP_Text>().text = 15 *companionProp.skillLv + " coin for next Lv.";
    }
    public void SetUpgradeButton(Companion companionProp)
    {
        upHp.onClick.AddListener(() => Upgrade("hp",companionProp));
        upMp.onClick.AddListener(() => Upgrade("mp", companionProp));
        upAtk.onClick.AddListener(() => Upgrade("atk", companionProp));
        upDef.onClick.AddListener(() => Upgrade("def", companionProp));
        upSkill.onClick.AddListener(() => Upgrade("skill", companionProp));
    }
    public void ResetUpgradeButton()
    {
        upHp.onClick.RemoveAllListeners();
        upMp.onClick.RemoveAllListeners();
        upAtk.onClick.RemoveAllListeners();
        upDef.onClick.RemoveAllListeners(    );
        upSkill.onClick.RemoveAllListeners();
    }
    private void Upgrade(string stat , Companion companionProp)
    {
        if(stat == "hp")
        {
            if(Player.Instance.Coin > 10 * companionProp.companionProperty.HpLv)
            {
                Player.Instance.Coin -= 10 * companionProp.companionProperty.HpLv;
                companionProp.companionProperty.HpLv += 1;
            }
        }
        if(stat == "mp")    
        {
            if(Player.Instance.Coin > 10* companionProp.companionProperty.MpLv)
            {
                Player.Instance.Coin -= 10 * companionProp.companionProperty.MpLv;
                companionProp.companionProperty.MpLv += 1;
            }
        }
        if( stat == "atk")
        {
            if (Player.Instance.Coin > 10 * companionProp.companionProperty.AtkLv)
            {
                Player.Instance.Coin -= 10*companionProp.companionProperty.AtkLv;
                companionProp.companionProperty.AtkLv += 1;
            }
        }
        if(stat == "def")
        {
            if(Player.Instance.Coin > 10*companionProp.companionProperty.DefLv)
            {
                Player.Instance.Coin -= 10 * companionProp.companionProperty.DefLv;
                companionProp.companionProperty.DefLv += 1;
            }
        }
        if(stat == "skill")
        {
            if(companionProp.skill.skillName.ToString() != "NoSkill")
            {
                if (Player.Instance.Coin > 15 * companionProp.skillLv)
                {
                    Player.Instance.Coin -= 15 * companionProp.skillLv;
                    companionProp.skillLv += 1;
                }
            } 
        }
        SetStatText(companionProp);
    }
}
