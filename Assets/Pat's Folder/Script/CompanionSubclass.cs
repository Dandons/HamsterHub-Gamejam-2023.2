using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CompanionSubclass : MonoBehaviour
{
    public SkillObject skill;

    //use for set base stat(one time use)
    [SerializeField] float baseAtk;
    [SerializeField] float baseHp;
    [SerializeField] float baseMp;
    [SerializeField] float baseDef;
    [SerializeField] float baseHpRegen;
    [SerializeField] float baseMpRegen;

    [SerializeField] float atkRange;
    public int skillLv;

    private int normalAttackCount = 0;

    public Companion companion;
    public Companion.Rarity rarity;
    public Companion.AttacKType attackType;
    private void Start()
    {
        companion = new Companion(baseatk: baseAtk, basehp: baseHp, basemp: baseMp, basedef: baseDef, basehpRegen: baseHpRegen, basempRegen: baseMpRegen);
        companion.rarity = rarity;
        companion.attackType = attackType;
    }
    public void TakeDamage(float damage)
    {
        companion.currentHp -= damage;
    }
    private float GetDistance(Vector3 enemyPosition)
    {
        Vector2 vectorToEnemy = enemyPosition - this.transform.position;
        return vectorToEnemy.magnitude;
    }
    private int GetNearestDistance(float[] enemyDistance)
    {
        for (int i = 0;i < enemyDistance.Length; i++)
        {
            if (enemyDistance[i] == enemyDistance.Min())
            {
                return i;
            }
        }
        return 0;
    }
    private Collider2D[] GetEnemies(Collider2D[] enemies)
    {
        List<Collider2D> enemy = new List<Collider2D>();
        foreach (Collider2D c in enemies)
        {
            if (c.tag == "Enemy")
            {
                enemy.Add(c);
            }
        }
        return enemy.ToArray();
    }
    public void Attack(string attackType)
    {
        Collider2D[] entity = Physics2D.OverlapCircleAll(this.transform.position, atkRange);
        entity = GetEnemies(entity);
        if (normalAttackCount < 2)
        {
            if (attackType == Companion.AttacKType.Melee.ToString())
            {
                for (int i = 0; i < entity.Length; i++)
                {
                    //Get Component and call TakeDamage in ENEMY
                }
                normalAttackCount += 1;
            }
            if (attackType == Companion.AttacKType.Range.ToString())
            {
                float[] enemyDistance = new float[entity.Length];
                for (int i = 0; i < entity.Length; i++)
                {
                    enemyDistance = enemyDistance.Append(GetDistance(entity[i].transform.position)).ToArray();
                }
                Collider2D enemyToHit = entity[GetNearestDistance(enemyDistance)];
                //Get Component and call TakeDamage in ENEMY
                normalAttackCount += 1;
            }
        }
        if(normalAttackCount >= 2)
        {
            SpecialAttack();
        }
    }
    private void SpecialAttack()
    {
        float usedMp = skill.skill.baseMpUsage * skill.skill.mpUsageAmplfier;
        if (usedMp < companion.currentMp) 
        {
            companion.currentMp -= usedMp;
            skill.skill.UseSkill();
        }
        
    }//skill
    public void Update()
    {
        skill.skill.UseSkill();
    }
}
