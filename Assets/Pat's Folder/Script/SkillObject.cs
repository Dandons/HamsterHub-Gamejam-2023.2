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
        {"JosephSkill",0},{"NoSkill",1}
    };
    public enum SkillUser
    {
        JosephSkill,NoSkill
    }
    public SkillUser skillName = new SkillUser();
    private void Awake()
    {
        allSkill = new Skill[5];
        allSkill[0] = new JosephSkill();
        allSkill[1] = new noSkill();
        skill = allSkill[skillDict[skillName.ToString()]];
    }
}
