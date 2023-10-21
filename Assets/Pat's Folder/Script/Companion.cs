using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class Companion : MonoBehaviour
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

    public int skillLv = 0;

    public bool lowTier;

    private int normalAttackCount = 0;
    private float nextAttack = 0.1f;

    public string name;
    public string description;
    public Sprite icon;

    public CompanionProperty companionProperty;
    public CompanionProperty.Rarity rarity;
    public CompanionProperty.AttacKType attackType;
    private void Start()
    {
        companionProperty = new CompanionProperty(baseatk: baseAtk, basehp: baseHp, basemp: baseMp, basedef: baseDef, basehpRegen: baseHpRegen, basempRegen: baseMpRegen);
        companionProperty.rarity = rarity;
        companionProperty.attackType = attackType;
    }
    public void TakeDamage(float damage)
    {
        if(companionProperty.def > damage) { companionProperty.currentHp -= damage * 0.1f; }
        else { companionProperty.currentHp -= damage - companionProperty.def; }
    }
    private float GetDistance(Vector3 enemyPosition)
    {
        Vector2 vectorToEnemy = enemyPosition - this.transform.position;
        return vectorToEnemy.magnitude;
    }
    private int GetNearestDistance(float[] enemyDistance)
    {
        float minDistance = float.MaxValue;
        int minIndex = 0;

        for (int i = 0; i < enemyDistance.Length; i++)
        {
            if (enemyDistance[i] < minDistance)
            {
                minDistance = enemyDistance[i];
                minIndex = i;
            }
        }

        return minIndex;
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
        Collider2D[] enemyGroup = GetEnemies(entity);
        Collider2D nearestEnemy = AttackNearestEnemy(enemyGroup);
        if (normalAttackCount < normalPerSkill)
        {
            if (companionProperty.attackType == CompanionProperty.AttacKType.Melee && Time.time > nextAttack && entity.Length > 0)
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
                                hit[j].collider.gameObject.GetComponent<Enemy>().takeDamge(companionProperty.atk);
                            }
                        }
                        coneRotation *= Quaternion.AngleAxis(10, Vector3.forward); // Rotate the direction for the next ray.
                    }


                    normalAttackCount += 1;
                    nextAttack = Time.time + cdPerAttack;
                }
            }
            if (companionProperty.attackType == CompanionProperty.AttacKType.Range && Time.time > nextAttack) //is Range and Ready to attack
            {
                if(nearestEnemy != null)
                {
                    RotateAttackAnimation(nearestEnemy);
                    
                    nearestEnemy.AddComponent<Enemy>().takeDamge(companionProperty.atk);

                    normalAttackCount += 1;
                }
                nextAttack = Time.time + cdPerAttack;
            }
            
        }
        if(normalAttackCount >= normalPerSkill && Time.time > nextAttack && entity.Length>0 && lowTier)
        {
            if(nearestEnemy != null) 
            {
                RotateAttackAnimation(nearestEnemy);
                SpecialAttack(nearestEnemy);

                nextAttack = Time.time + cdPerAttack;
                normalAttackCount = 0;
            }  
        }
      
    }
    
    private void RotateAttackAnimation(Collider2D enemy)
    {
        Animator animator = this.GetComponent<Animator>();
        Vector3 enemyPosition = enemy.gameObject.transform.position;
        Vector2 direction = enemyPosition - transform.position;
        float x = Math.Abs(direction.x);
        float y = Math.Abs(direction.y);
        if(x > y && !lowTier)//left or right
        {
            if(x==direction.x) //Xaxis is possitive value so RIGHT
            {
                animator.SetTrigger("right");
            }
            if (x == direction.x * -1)//X axis is negative value so LEFT
            {
                animator.SetTrigger("left");
            }
        }
        if(y>x && !lowTier)//up or down
        {
            if(y==direction.y) // y Axis is possitive value so BACK
            {
                animator.SetTrigger("back");
            }
            if(x == direction.y * -1)// Y Axis is negative value so FRONT
            {
                animator.SetTrigger("front");
            }
        }
        if (lowTier)
        {
            animator.SetTrigger("attack");
        }
    }
    private Collider2D AttackNearestEnemy(Collider2D[] entity)
    {
        float[] enemyDistance = new float[entity.Length];
        for (int i = 0; i < entity.Length; i++)
        {
            enemyDistance[i] = GetDistance(entity[i].transform.position);
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
        if (usedMp < companionProperty.currentMp) 
        {
            companionProperty.currentMp -= usedMp;
            skill.skill.UseSkill(this.transform.position,enemy,companionProperty.atk,atkRange,skillLv);
        }
    }//skill

    private void Regeneration()
    {
        StartCoroutine(Regen());
    }
    IEnumerator Regen()
    {
        yield return new WaitForSeconds(1);
        companionProperty.currentHp += companionProperty.hpRegen;
        companionProperty.currentMp += companionProperty.mpRegen;
    }
    public void Update()
    {
        Attack();
        Regeneration();
        if(companionProperty.currentHp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, atkRange);
    }
}
