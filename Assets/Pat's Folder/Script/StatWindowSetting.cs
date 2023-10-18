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
        SetStatText(companionProp);
        SetUpgradeButton(companionProp);

    }
    void SetStatText(CompanionSubclass companionProp)
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
    }
    void SetUpgradeButton(CompanionSubclass companionProp)
    {
        upHp.GetComponent<Button>().onClick.AddListener(() => Upgrade("hp"));
    }
    void Upgrade(string stat)
    {
        if(stat == "hp")
        {

        }
    }
}
