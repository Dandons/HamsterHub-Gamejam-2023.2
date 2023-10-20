using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public float baseDamage => 300;
    public float baseMpUsage => 100;
    public void UseSkill()
    {
        float outHeal = baseDamage * (1+(Player.Instance.healLv*2));
        Player.Instance.playerProperty.currentHp += outHeal;
    }
}
