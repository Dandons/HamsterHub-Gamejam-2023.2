using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noSkill : Skill
{
    public override string skillName { get => "No Skill"; }
    public override float baseMpUsage => 0;
}
