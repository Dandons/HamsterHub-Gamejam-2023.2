using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    //Property for Companion's Hp
    private int hpLv;
    private float hp;
    private float maxHp;
    public float currentHp
    {
        get { return hpLv; } 
        set
        {
            hp = value;
            if(hp > maxHp)
            {
                hp = maxHp;
            }
            if(hp < 0)
            {
                hp = 0;
            }
        }
    }
    public int HpLv
    {
        get { return hpLv; }
        set
        {
            hpLv = value;
            switch (rarity)
            {
                case Rarity.Common:
                    maxHp = 100 * hpLv; break;
                case Rarity.Uncommon:
                    maxHp = 150 * hpLv; break;
                case Rarity.Rare:
                    maxHp = 200 * hpLv; break;
                case Rarity.SuperRare:
                    maxHp = 250 * hpLv; break;
            }
        }
    }

    //Property for Companion's Mp
    private float maxMp;
    private int mpLv;
    private float mp;
    public float currentMp
    {
        get { return mp; }
        set 
        { 
            mp = value; 
            if(mp > maxMp)
            {
                mp = maxMp;
            }
            if(mp < 0)
            {
                mp = 0;
            }
        }
    }
    public int MpLv
    {
        get { return mpLv; }
        set
        {
            mpLv = value;
            switch (rarity)
            {
                case Rarity.Common:
                    maxMp = 100 * mpLv; break;
                case Rarity.Uncommon:
                    maxMp = 150 * mpLv; break;
                case Rarity.Rare:
                    maxMp = 200 * mpLv; break;
                case Rarity.SuperRare:
                    maxMp = 250 * mpLv; break;
            }
        }
    }

    //Property for Companion's Atk
    [HideInInspector]public float atk;
    private int atkLv;
    public int AtkLv
    {
        get { return atkLv; }
        set
        {
            atkLv = value;
            switch(rarity)
            {
                case Rarity.Common:
                    atk = 100 * atkLv; break;
                case Rarity.Uncommon:
                    atk = 110 * atkLv; break;
                case Rarity.Rare:
                    atk = 120 *atkLv; break;
                case Rarity.SuperRare:
                    atk = 130 *atkLv; break;
            }
        }
    }

    //Property for Companion's Defence
    [HideInInspector] public float def;
    private int defLv;
    public int DefLv
    {
        get => defLv;
        set
        {
            defLv = value;
            switch(rarity)
            {
                case Rarity.Common:
                    def = 10 * defLv; break;
                case Rarity.Uncommon:
                    def = 15 * defLv; break;
                case Rarity.Rare:
                    def = 20 * defLv; break;
                case Rarity.SuperRare:
                    def = 25 * defLv; break;
            }
        }
    }

    //Attack type
    public enum AttacKType
    {
        Range,Melee
    }
    public AttacKType attackType = new AttacKType();

    //Rarity
    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        SuperRare
    }
    public Rarity rarity = new Rarity();


    private void Start()
    {
        HpLv = 1;
        MpLv = 1;
        currentHp = maxHp;
        currentMp = maxMp;
    }

}
