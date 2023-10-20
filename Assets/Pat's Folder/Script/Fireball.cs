using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float baseDamage => 100;
    public float baseMpUsage => 30;
    public void UseSkill(Vector2 direction)
    {
        


        float outDamage = baseDamage * (1 + (Player.Instance.fireballLv * 2));
        Rigidbody2D fireball = Instantiate(Player.Instance.fireballShape);
        fireball.GetComponent<FireballItSelf>().damage = outDamage;
        fireball.AddForce(direction * 2);
        Destroy( fireball , 3);

    }
}

