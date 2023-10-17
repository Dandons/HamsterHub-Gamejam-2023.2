using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    protected int enemyHeathPoint;
    protected int attackDamage;
    protected float speed = 100f;
    protected int defend;
    protected float detectRange = 100f;
    protected float attackRange = 1f;
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

    protected void TakeDamage()
    {

    }
   
}

public class EnemyMelee : Enemy
{
    public EnemyMelee(Transform enemyTransform) : base(enemyTransform)
    {
        enemyHeathPoint = 5;
        attackDamage = 10;
        speed = 2;
        defend = 5;

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
}

