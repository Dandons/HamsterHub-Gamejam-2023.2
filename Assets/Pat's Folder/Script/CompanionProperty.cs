using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CompanionProperty
{
    //Base Stat
    private float baseHp;
    private float baseMp;
    private float baseAtk;
    private float baseDef;
    private float baseHpRegen;
    private float baseMpRegen;

    //Property for Companion's Hp
    public float hpRegen;
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
                    hpRegen = baseHpRegen+(baseHpRegen*statGrowth) * amplifier;
                    maxHp = baseHp+(baseHp*statGrowth) * amplifier; break;
                case Rarity.Uncommon:
                    hpRegen = baseHpRegen + (baseHpRegen * statGrowth) * amplifier;
                    maxHp = baseHp + (baseHp * statGrowth) * amplifier; break;
                case Rarity.Rare:
                    hpRegen = baseHpRegen + (baseHpRegen * statGrowth) * amplifier;
                    maxHp = baseHp + (baseHp * statGrowth) * amplifier; break;
                case Rarity.SuperRare:
                    hpRegen = baseHpRegen + (baseHpRegen * statGrowth) * amplifier;
                    maxHp = baseHp + (baseHp * statGrowth) * amplifier; break;
            }
        }
    }

    //Property for Companion's Mp
    private float maxMp;
    public float mpRegen;
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
                    mpRegen = baseMpRegen + (baseMpRegen * statGrowth) * amplifier;
                    maxMp = baseMp + (baseMp * statGrowth) * amplifier; break;
                case Rarity.Uncommon:
                    mpRegen = baseMpRegen + (baseMpRegen * statGrowth) * amplifier;
                    maxMp = baseMp + (baseMp * statGrowth) * amplifier; break;
                case Rarity.Rare:
                    mpRegen = baseMpRegen + (baseMpRegen * statGrowth) * amplifier;
                    maxMp = baseMp + (baseMp * statGrowth) * amplifier; break;
                case Rarity.SuperRare:
                    mpRegen = baseMpRegen + (baseMpRegen * statGrowth) * amplifier;
                    maxMp = baseMp + (baseMp * statGrowth) * amplifier; break;
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
                    atk = baseAtk + (baseAtk * statGrowth) * amplifier; break;
                case Rarity.Uncommon:
                    atk = baseAtk + (baseAtk * statGrowth) * amplifier; break;
                case Rarity.Rare:
                    atk =   baseAtk + (baseAtk * statGrowth) * amplifier; break;
                case Rarity.SuperRare:
                    atk = baseAtk + (baseAtk * statGrowth) * amplifier; break;
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
                    def = baseDef+(baseDef*statGrowth) * amplifier; break;
                case Rarity.Uncommon:
                    def = baseDef + (baseDef * statGrowth) * amplifier; break;
                case Rarity.Rare:
                    def = baseDef + (baseDef * statGrowth) * amplifier; break;
                case Rarity.SuperRare:
                    def = baseDef + (baseDef * statGrowth) * amplifier; break;
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
    private float statGrowth
    {
        get
        {
            switch (rarity)
            {
                case Rarity.Common:
                    return 0.2f;
                case Rarity.Uncommon:
                    return 0.3f;
                case Rarity.Rare:
                    return 0.4f;
                case Rarity.SuperRare:
                    return 0.5f;
            }
            return 0.2f;
        }
    }

    public CompanionProperty(float baseatk, float basehp, float basemp, float basedef, float basehpRegen, float basempRegen)
    {
        baseAtk = baseatk;
        baseHp = basehp;
        baseMp = basemp;
        baseDef = basedef;
        baseHpRegen = basehpRegen;
        baseMpRegen = basempRegen;
        HpLv = 0;
        MpLv = 0;
        AtkLv = 0;
        DefLv = 0;
        currentHp = maxHp;
        currentMp = maxMp;
    }
}
