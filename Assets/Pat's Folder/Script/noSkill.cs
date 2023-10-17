using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noSkill : Skill
{
    public override string skillName { get => "No Skill"; }
    public override int LvSkill => 0;
    public override float baseMpUsage => 0;
    public override void UseSkill(Vector3 position, Collider2D enemy, float damage, float castDistance) { }
}
