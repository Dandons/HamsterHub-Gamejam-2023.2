using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JosephSkill : Skill
{
    public int lvSkill;
    public override float baseDamage => 150;
    public override float baseMpUsage => 60;
    public override int LvSkill { get => lvSkill; set => lvSkill = value; }
    public override float mpUsageAmplfier => LvSkill + 1;
    public override float damageAmplfier => 1 + (LvSkill * 2);
    public override void UseSkill()
    {
        //base.UseSkill();
        Debug.Log("JOSEPHHHHHHHH");
    }
}
