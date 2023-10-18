using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

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

    public int skillLv = 1;

    private int normalAttackCount = 0;
    private float nextAttack = 0.1f;

    public string name;
    public string description;
    public Sprite icon;

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
        Collider2D nearestEnemy = AttackNearestEnemy(entity);
        if (normalAttackCount < normalPerSkill)
        {
            if (companion.attackType == Companion.AttacKType.Melee && Time.time > nextAttack && entity.Length > 0)
            {
                if(nearestEnemy != null)
                {
                    RotateAttackAnimation(nearestEnemy);
                    float coneAngle = 30f;
                    Vector2 direction = nearestEnemy.transform.position - transform.position;
                    Quaternion coneRotation = Quaternion.AngleAxis(-coneAngle / 2, Vector3.forward);
                    for (int i = 0; i < 360; i += 10) // You can adjust the increment for smoother or coarser results.
                    {
                        Vector2 rotatedDirection = coneRotation * direction;
                        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, rotatedDirection, atkRange);
                        for (int j = 0; j < hit.Length; j++)
                        {
                            if (hit[j].collider.tag == "Enemy")
                            {
                                hit[j].collider.gameObject.GetComponent<Enemy>().takeDamge(companion.atk);
                            }
                        }
                        coneRotation *= Quaternion.AngleAxis(10, Vector3.forward); // Rotate the direction for the next ray.
                    }


                    normalAttackCount += 1;
                    nextAttack = Time.time + cdPerAttack;
                }
            }
            if (companion.attackType == Companion.AttacKType.Range && Time.time > nextAttack) //is Range and Ready to attack
            {
                if(nearestEnemy!=null)
                {
                    RotateAttackAnimation(nearestEnemy);
                    nearestEnemy.GetComponent<Enemy>().takeDamge(companion.atk);
                    normalAttackCount += 1;
                }
                nextAttack = Time.time + cdPerAttack;
            }
            
        }
        if(normalAttackCount >= normalPerSkill && Time.time > nextAttack && entity.Length>0 && skill.skill.skillName=="NoSkill")
        {
            if(nearestEnemy!= null) 
            {
                RotateAttackAnimation(nearestEnemy);
                SpecialAttack(nearestEnemy);
                nextAttack = Time.time + cdPerAttack;
                normalAttackCount = 0;
            }  
        }
    }
    void RotateAttackAnimation(Collider2D enemy)
    {
        Animator animator = this.GetComponent<Animator>();
        Vector3 enemyPosition = enemy.gameObject.transform.position;
        Vector2 direction = enemyPosition - transform.position;
        float x = Math.Abs(direction.x);
        float y = Math.Abs(direction.y);
        if(x > y)//left or right
        {
            if(x==direction.x) //Xaxis is possitive value so RIGHT
            {
                animator.Play("Joseph_Attack_Right");
            }
            if (x == direction.x * -1)//X axis is negative value so LEFT
            {
                animator.Play("Joseph_Attack_Left");
            }
        }
        if(y>x)//up or down
        {
            if(y==direction.y) // y Axis is possitive value so BACK
            {
                animator.Play("Joseph_Attack_Back");
            }
            if(x == direction.y * -1)// Y Axis is negative value so FRONT
            {
                animator.Play("Joseph_Attack_Front");
            }
        }
    }
    private Collider2D AttackNearestEnemy(Collider2D[] entity)
    {
        float[] enemyDistance = new float[entity.Length];
        for (int i = 0; i < entity.Length; i++)
        {
            enemyDistance = enemyDistance.Append(GetDistance(entity[i].transform.position)).ToArray();
        }
        if (enemyDistance.Length > 0)
        {
            Collider2D enemyToHit = entity[GetNearestDistance(enemyDistance)];
            return enemyToHit;
        }
        return null;
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
        //skill.skill.lvSkill = skillLv;
        Attack();
        Regeneration();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, atkRange);
    }
}
