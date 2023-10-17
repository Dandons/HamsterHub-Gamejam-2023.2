using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("skillObject"))]
public class SkillObject : ScriptableObject
{
    private Skill[] allSkill;
    [HideInInspector]public Skill skill;
    private Dictionary<string, int> skillDict = new Dictionary<string, int>()
    {
        {"JosephSkill",0}
    };
    public enum SkillUser
    {
        JosephSkill
    }
    public SkillUser skillName = new SkillUser();
    private void Awake()
    {
        allSkill = new Skill[5];
        allSkill[0] = new JosephSkill();
        skill = allSkill[skillDict[skillName.ToString()]];
    }
}
