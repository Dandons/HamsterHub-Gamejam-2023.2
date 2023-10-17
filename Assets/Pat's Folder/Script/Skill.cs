using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public virtual float baseMpUsage { get; }
    public virtual float baseDamage { get; }
    public virtual int LvSkill {  get; set; }
    public virtual float damageAmplfier { get; }
    public virtual float mpUsageAmplfier { get; }

    //rn jst in case
    public virtual void UseSkill(Vector3 position, Collider2D targetEnemy , float damage,float hitRange) { }
}
