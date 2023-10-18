using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty
{
    private float hp;
    private float maxHp;
    public float Hp
    {
        get { return hp; }
        set 
        { 
            hp = value;
            if (hp > maxHp)
            {
                hp = maxHp;
            }
        }
    }

}
