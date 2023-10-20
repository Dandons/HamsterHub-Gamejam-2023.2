using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Singleton<Player>
{
    public PlayerProperty playerProperty;
    public static Player Instance;
    private float baseAtk = 120;
    private float baseHp = 150;
    private float baseMp = 100;
    private float baseDef = 100;
    private float baseHpRegen = 1.5f;
    private float baseMpRegen = 0.7f;
    public float atkRange;
    public bool reflection;
    

    private Fireball fireball;
    public Rigidbody2D fireballShape;
    [HideInInspector] public int fireballLv = 0;
    private Heal heal;
    [HideInInspector] public int healLv = 0;

    [HideInInspector] public delegate void ActiveMethod();
    [HideInInspector] public ActiveMethod activeMethod;

    [HideInInspector] public int Coin;
    [HideInInspector] public int Tear;
    public Animator aim;

    [SerializeField] float moveSpeed;
    private Vector2 input;
    private Rigidbody2D rb;

    private void Start()
    {
        heal = GetComponent<Heal>();
        fireball = GetComponent<Fireball>();
        SetActiveMethod();
        Instance = this;
        playerProperty = new PlayerProperty(baseatk: baseAtk, basehp: baseHp, basemp: baseMp, basedef: baseDef, basehpRegen: baseHpRegen, basempRegen: baseMpRegen);
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void UseFireball(Vector2 direction)
    {
        float mpUsage = fireball.baseMpUsage * (1 + fireballLv);
        if(playerProperty.currentMp - mpUsage >= 0)
        {
            playerProperty.currentMp -= mpUsage;
            fireball.UseSkill(direction);
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
        Collider2D[] enemy = Physics2D.OverlapCircleAll(this.transform.position, atkRange);
        enemy = GetEnemies(enemy);
        
        
        if (Input.GetKeyDown(KeyCode.Alpha1)&&enemy.Length!=0)
        {
            Collider2D enemyToHit = AttackNearestEnemy(enemy);
            Vector2 direction = enemyToHit.transform.position - this.transform.position;
            UseFireball(direction);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            UseHeal();
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
            if(enemy.Length != 0)
            {
                Collider2D enemyToHit = AttackNearestEnemy(enemy);
                RotateAttackAnimation(enemyToHit);
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
        activeMethod += NormalAttack;
        activeMethod += UseSkill;

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
        aim.SetBool("reflect", reflection);
        //FRONT
        if (Input.GetKeyDown(KeyCode.S))
        {
            aim.SetTrigger("fronting_");
            aim.SetBool("fronting",true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            aim.SetBool("fronting", false);
        }
        //LEFT
        if (Input.GetKeyDown(KeyCode.A))
        {
            aim.SetTrigger("lefting_");
            aim.SetBool("lefting", true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            aim.SetBool("lefting", false);
        }
        //RIGHT
        if (Input.GetKeyDown(KeyCode.D))
        {
            aim.SetTrigger("righting_");
            aim.SetBool("righting", true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            aim.SetBool("righting", false);
        }
        //BACK
        if (Input.GetKeyDown(KeyCode.W))
        {
            aim.SetTrigger("backing_");
            aim.SetBool("backing", true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            aim.SetBool("backing", false);
        }
    }

    private void Update()
    {
        activeMethod();
        if(playerProperty.currentHp <= 0)
        {
            //reset world
        }
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
