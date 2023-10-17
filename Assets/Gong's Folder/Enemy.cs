using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    protected float enemyHeathPoint;
    protected float attackDamage;
    protected float speed;
    protected float defend;
    protected float detectRange;
    protected float attackRange;
    protected float allDamge;

    protected Transform target;
    protected Transform transform;
    public Rigidbody2D rb;
    

    public Enemy(Transform enemyTransform)
    {
        transform = enemyTransform;
    
}


    public virtual void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectRange);


        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Companion"))
            {
                if (target == null || Vector2.Distance(transform.position, collider.transform.position) < Vector2.Distance(transform.position, target.position))
                {
                    target = collider.transform;
                }
                if(target != null)
                {
                    float distanceToTarget = Vector2.Distance(transform.position, target.position);

                    if (distanceToTarget <= attackRange)
                    {
                        Attack();
                    }
                    else
                    {
                        // Move towards the targe
                        Vector2 direction = (target.position - transform.position);
                        rb.velocity = direction * speed * Time.deltaTime;

                    }
                }
            }
        }

     
    }

    protected void Move(Vector2 direction)
    {   
        
        Vector3 newPosition = new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
        transform.position = newPosition;
      
    }

    private void Attack()
    {
        Debug.Log("Attacking" + target.name);
    }
    private void takeDamge(float atkplayer)
    {
        if(defend >= atkplayer*0.9)
        {
            allDamge = atkplayer - defend;
        }
        else
        {
            allDamge = atkplayer - defend;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //takeDamge(atkplayer);
            enemyHeathPoint -= allDamge;

        }
    }
    
}

public class EnemyMelee : Enemy
{
    public EnemyMelee(Transform enemyTransform) : base(enemyTransform)
    {
        attackRange = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per
    private void Update()
    {
        enemyHeathPoint = 1 + 2 * (DayCount.days - 1);
        attackDamage = 1 + 1 * (DayCount.days - 1);
        defend = 1 + 2 * (DayCount.days - 1);
    }

}

public class EnemyRange : Enemy
{
    public EnemyRange(Transform enemyTransform) : base(enemyTransform)
    {
        attackRange = 7f;

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per
    private void Update()
    {
        enemyHeathPoint = 1 + 1 * (DayCount.days - 1);
        attackDamage = 1 + 2 * (DayCount.days - 1);
        defend = 1 + 1 * (DayCount.days - 1);
    }

}

