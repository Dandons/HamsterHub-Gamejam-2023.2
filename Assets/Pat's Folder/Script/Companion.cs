using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Companion
{
    //Base Stat
    private float baseHp;
    private float baseMp;
    private float baseAtk;
    private float baseDef;
    private float baseHpRegen;
    private float baseMpRegen;

    //Property for Companion's Hp
    private float hpRegen;
    private int hpLv;
    private float hp;
    private float maxHp;
    public float currentHp
    {
        get { return hp; } 
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
            float amplifier = 1 + (hpLv * 2);
            switch (rarity)
            {
                case Rarity.Common:
                    hpRegen = baseHpRegen * amplifier;
                    maxHp = baseHp * amplifier; break;
                case Rarity.Uncommon:
                    hpRegen = baseHpRegen * amplifier;
                    maxHp = baseHp * amplifier; break;
                case Rarity.Rare:
                    hpRegen = baseHpRegen * amplifier;
                    maxHp = baseHp * amplifier; break;
                case Rarity.SuperRare:
                    hpRegen = baseHpRegen * amplifier;
                    maxHp = baseHp * amplifier; break;
            }
        }
    }

    //Property for Companion's Mp
    private float maxMp;
    private float mpRegen;
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
            float amplifier = 1 + (mpLv * 2);
            switch (rarity)
            {
                case Rarity.Common:
                    mpRegen = baseMpRegen * amplifier;
                    maxMp = baseMp * amplifier; break;
                case Rarity.Uncommon:
                    mpRegen = baseMpRegen * amplifier;
                    maxMp = baseMp * amplifier; break;
                case Rarity.Rare:
                    mpRegen = baseMpRegen * amplifier;
                    maxMp = baseMp * amplifier; break;
                case Rarity.SuperRare:
                    mpRegen = baseMpRegen * amplifier;
                    maxMp = baseMp * amplifier; break;
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
            float amplifier = 1 + (atkLv * 2);
            switch(rarity)
            {
                case Rarity.Common:
                    atk = baseAtk * amplifier; break;
                case Rarity.Uncommon:
                    atk = baseAtk * amplifier; break;
                case Rarity.Rare:
                    atk = baseAtk * amplifier; break;
                case Rarity.SuperRare:
                    atk = baseAtk * amplifier; break;
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
            float amplifier = 1+(defLv * 2);
            switch(rarity)
            {
                case Rarity.Common:
                    def = baseDef * amplifier; break;
                case Rarity.Uncommon:
                    def = baseDef * amplifier; break;
                case Rarity.Rare:
                    def = baseDef * amplifier; break;
                case Rarity.SuperRare:
                    def = baseDef * amplifier; break;
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
    public Companion(float baseatk, float basehp, float basemp, float basedef, float basehpRegen, float basempRegen)
    {
        baseAtk = baseatk;
        baseHp = basehp;
        baseMp = basemp;
        baseDef = basedef;
        baseHpRegen = basehpRegen;
        baseMpRegen = basempRegen;
        HpLv = 1;
        MpLv = 1;
        AtkLv = 1;
        DefLv = 1;
        currentHp = maxHp;
        currentMp = maxMp;
    }
}
