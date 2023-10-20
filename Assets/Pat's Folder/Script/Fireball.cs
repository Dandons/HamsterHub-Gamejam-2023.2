using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float baseDamage => 100;
    public float baseMpUsage => 30;
    public void UseSkill()
    {
        float outDamage = baseDamage * (1 + (Player.Instance.fireballLv * 2));
        Rigidbody2D fireball = Instantiate(Player.Instance.fireballShape);
        


    }
}

