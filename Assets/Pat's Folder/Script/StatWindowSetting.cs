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
        CompanionSubclass companionProp = companion.GetComponent<CompanionSubclass>();
        fullSprite.sprite = companion.GetComponent<SpriteRenderer>().sprite;
        
        ResetUpgradeButton();
        SetUpgradeButton(companionProp);
        SetStatText(companionProp);

    }
    [ContextMenu("SetCoin")]
    public void SetCoin()
    {
        Player.Coin = 999999;
    }
    public void SetStatText(CompanionSubclass companionProp)
    {
        name.text = companionProp.name;
        description.text = companionProp.description;
        icon.sprite = companionProp.icon;
        statHp.text = "Hp Level : " + companionProp.companion.HpLv;
        statMp.text = "Mp Level : " + companionProp.companion.MpLv;
        statAtk.text = "Atk Level : " + companionProp.companion.AtkLv;
        statDef.text = "Def Level : " + companionProp.companion.DefLv;
        if (companionProp.skill.skillName.ToString() == "NoSkill")
        {
            statSkill.text = "character doesn't have skill";
        }
        else
        {
            statSkill.text = "Skill Level : " + companionProp.skillLv;
        }
        upHp.GetComponentInChildren<TMP_Text>().text = 10 * companionProp.companion.HpLv + " coin for next Lv.";
        upMp.GetComponentInChildren<TMP_Text>().text = 10 * companionProp.companion.MpLv + " coin for next Lv.";
        upAtk.GetComponentInChildren<TMP_Text>().text = 10 * companionProp.companion.AtkLv + " coin for next Lv.";
        upDef.GetComponentInChildren<TMP_Text>().text = 10 * companionProp.companion.DefLv + " coin for next Lv.";
        upSkill.GetComponentInChildren<TMP_Text>().text = 15 *companionProp.skillLv + " coin for next Lv.";
    }
    public void SetUpgradeButton(CompanionSubclass companionProp)
    {
        upHp.GetComponent<Button>().onClick.AddListener(() => Upgrade("hp",companionProp));
        upMp.GetComponent<Button>().onClick.AddListener(() => Upgrade("mp", companionProp));
        upAtk.GetComponent<Button>().onClick.AddListener(() => Upgrade("atk", companionProp));
        upDef.GetComponent<Button>().onClick.AddListener(() => Upgrade("def", companionProp));
        upSkill.GetComponent<Button>().onClick.AddListener(() => Upgrade("skill", companionProp));
    }
    public void ResetUpgradeButton()
    {
        upHp.GetComponent<Button>().onClick.RemoveAllListeners();
        upMp.GetComponent<Button>().onClick.RemoveAllListeners();
        upAtk.GetComponent<Button>().onClick.RemoveAllListeners();
        upDef.GetComponent<Button>().onClick.RemoveAllListeners(    );
        upSkill.GetComponent<Button>().onClick.RemoveAllListeners();
    }
    private void Upgrade(string stat , CompanionSubclass companionProp)
    {
        if(stat == "hp")
        {
            if(Player.Coin > 10 * companionProp.companion.HpLv)
            {
                Player.Coin -= 10 * companionProp.companion.HpLv;
                companionProp.companion.HpLv += 1;
            }
        }
        if(stat == "mp")
        {
            if(Player.Coin > 10* companionProp.companion.MpLv)
            {
                Player.Coin -= 10 * companionProp.companion.MpLv;
                companionProp.companion.MpLv += 1;
            }
        }
        if( stat == "atk")
        {
            if (Player.Coin > 10 * companionProp.companion.AtkLv)
            {
                Player.Coin -= 10*companionProp.companion.AtkLv;
                companionProp.companion.AtkLv += 1;
            }
        }
        if(stat == "def")
        {
            if(Player.Coin > 10*companionProp.companion.DefLv)
            {
                Player.Coin -= 10 * companionProp.companion.DefLv;
                companionProp.companion.DefLv += 1;
            }
        }
        if(stat == "skill")
        {
            if(companionProp.skill.skillName.ToString() != "NoSkill")
            {
                if (Player.Coin > 15 * companionProp.skillLv)
                {
                    Player.Coin -= 15 * companionProp.skillLv;
                    companionProp.skillLv += 1;
                }
            } 
        }
        SetStatText(companionProp);
    }
}
