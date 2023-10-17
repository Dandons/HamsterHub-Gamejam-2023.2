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
    [SerializeField] int normalPerSkill;
    [SerializeField] float atkRange;
    [SerializeField] float cdPerAttack;
    public int skillLv;

    private int normalAttackCount = 0;
    private float nextAttack = 0.1f;

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
        if(companion.def > damage) { companion.currentHp -= damage * 0.1f; }
        else { companion.currentHp -= damage - companion.def; }
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
    public void Attack()
    {
        Collider2D[] entity = Physics2D.OverlapCircleAll(this.transform.position, atkRange);
        entity = GetEnemies(entity);
        if (normalAttackCount < normalPerSkill)
        {
            if (companion.attackType == Companion.AttacKType.Melee && Time.time > nextAttack && entity.Length > 0)
            {
                for (int i = 0; i < entity.Length; i++)
                {
                    //Get Component and call TakeDamage in ENEMY
                }
                normalAttackCount += 1;
                nextAttack = Time.time + cdPerAttack;
            }
            if (companion.attackType == Companion.AttacKType.Range && Time.time > nextAttack)
            {
                float[] enemyDistance = new float[entity.Length];
                for (int i = 0; i < entity.Length; i++)
                {
                    enemyDistance = enemyDistance.Append(GetDistance(entity[i].transform.position)).ToArray();
                }
                if(enemyDistance.Length > 0) 
                {
                    Collider2D enemyToHit = entity[GetNearestDistance(enemyDistance)];
                    //Get Component and call TakeDamage in ENEMY
                    normalAttackCount += 1;
                }
                nextAttack = Time.time + cdPerAttack;
            }
            
        }
        if(normalAttackCount >= normalPerSkill && Time.time > nextAttack && entity.Length>0)
        {
            float[] enemyDistance = new float[entity.Length];
            for (int i = 0; i < entity.Length; i++)
            {
                enemyDistance = enemyDistance.Append(GetDistance(entity[i].transform.position)).ToArray();
            }
            if (enemyDistance.Length > 0)
            {
                Collider2D enemyToHit = entity[GetNearestDistance(enemyDistance)];
                SpecialAttack(enemyToHit);
                nextAttack = Time.time + cdPerAttack;
                normalAttackCount = 0;
            }  
        }
    }
    private void SpecialAttack(Collider2D enemy)
    {
        float usedMp = skill.skill.baseMpUsage * skill.skill.mpUsageAmplfier;
        if (usedMp < companion.currentMp) 
        {
            companion.currentMp -= usedMp;
            skill.skill.UseSkill(this.transform.position,enemy,companion.atk,atkRange);
        }
        
    }//skill

    private void Regeneration()
    {
        StartCoroutine(Regen());
    }
    IEnumerator Regen()
    {
        yield return new WaitForSeconds(1);
        companion.currentHp += companion.hpRegen;
        companion.currentMp += companion.mpRegen;
    }
    public void Update()
    {
        Attack();
        Regeneration();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, atkRange);
    }
}
