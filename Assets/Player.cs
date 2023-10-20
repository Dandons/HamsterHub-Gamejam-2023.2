using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Singleton<Player>
{
    public PlayerProperty playerProperty;
    public static Player Instance;
    [SerializeField] float baseAtk;
    [SerializeField] float baseHp;
    [SerializeField] float baseMp;
    [SerializeField] float baseDef;
    [SerializeField] float baseHpRegen;
    [SerializeField] float baseMpRegen;
    [SerializeField] float atkRange;

    public Fireball fireball;
    public Rigidbody2D fireballShape;
    public int fireballLv = 0;
    public Heal heal;
    public int healLv = 0;

    public delegate void ActiveMethod();
    public ActiveMethod activeMethod;

    public int Coin;
    public int Tear;
    public Animator aim;

    [SerializeField] float moveSpeed;
    private Vector2 input;
    private Rigidbody2D rb;

    private void Start()
    {
        Instance = this;
        playerProperty = new PlayerProperty(baseatk: baseAtk, basehp: baseHp, basemp: baseMp, basedef: baseDef, basehpRegen: baseHpRegen, basempRegen: baseMpRegen);
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void UseFireball()
    {
        float mpUsage = fireball.baseMpUsage * (1 + fireballLv);
        if(playerProperty.currentMp - mpUsage >= 0)
        {
            playerProperty.currentMp -= mpUsage;
            fireball.UseSkill();
        }
    }
    private void UseHeal()
    {
        float mpUsage = heal.baseMpUsage * (1 + healLv);
        if(playerProperty.currentMp - mpUsage >= 0)
        {
            playerProperty.currentMp -= mpUsage;
            heal.UseSkill();
        }
    }

    public void UseSkill()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            //use fireball
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            //use Heal
        }
    }

    //Below this is all u need for normal attack
    private float GetDistance(Vector3 enemyPosition)
    {
        Vector2 vectorToEnemy = enemyPosition - this.transform.position;
        return vectorToEnemy.magnitude;
    }
    private int GetNearestDistance(float[] enemyDistance)
    {
        for (int i = 0; i < enemyDistance.Length; i++)
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
    private void RotateAttackAnimation(Collider2D enemy)
    {
        Animator animator = this.GetComponent<Animator>();
        Vector3 enemyPosition = enemy.gameObject.transform.position;
        Vector2 direction = enemyPosition - transform.position;
        if(direction.x < 0)
        {
            //left attack
        }
        if(direction.x > 0)
        {
            //right attack
        }
    }
    public void NormalAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //select nearest enemy to hit
            Collider2D[] enemy = Physics2D.OverlapCircleAll(this.transform.position, atkRange);
            enemy = GetEnemies(enemy);
            if(enemy != null)
            {
                Collider2D enemyToHit = AttackNearestEnemy(enemy);
                enemyToHit.GetComponent<Enemy>().takeDamge(playerProperty.atk);
            } 
        }
    }

    //enable every player's method
    public void SetActiveMethod()
    {
        activeMethod += Regeneration;
        activeMethod += Move;
        activeMethod += SetAnimation;
    }
    //disable every player's method
    public void DisableActiveMethod()
    {
        activeMethod = null;
    }

    //just move
    private void Move()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        input.Normalize();
        rb.velocity = new Vector2(input.x * moveSpeed, input.y * moveSpeed);
    }
    //walk animation
    private void SetAnimation()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            aim.SetTrigger("fronting");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            aim.SetTrigger("lefting");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            aim.SetTrigger("righting");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            aim.SetTrigger("backing");
        }
    }

    private void Update()
    {
        activeMethod();
    }

    public void TakeDamage(float damage)
    {
        if (playerProperty.def > damage) { playerProperty.currentHp -= damage * 0.1f; }
        else { playerProperty.currentHp -= damage - playerProperty.def; }
    }
    private void Regeneration()
    {
        StartCoroutine(Regen());
    }
    IEnumerator Regen()
    {
        yield return new WaitForSeconds(1);
        playerProperty.currentHp += playerProperty.hpRegen;
        playerProperty.currentMp += playerProperty.mpRegen;
    }
}
