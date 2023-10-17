using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    protected int enemyHeathPoint;
    protected int attackDamage;
    protected int speed = 1;
    protected int defend;
    protected float detectRange = 100f;
    protected float attackRange = 1f;
    protected Transform target;
    protected Transform transform;

    public Enemy(Transform enemyTransform)
    {
        transform = enemyTransform;
    }


    public virtual void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectRange);
        target = null;

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
                        Vector2 direction = (target.position - transform.position).normalized;
                        Move(direction);
                    }
                }
            }
        }

     
    }

    protected void Move(Vector2 direction)
    {
        Vector3 newPosition = transform.position + new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
        transform.position = newPosition;
    }

    private void Attack()
    {

    }

    private void TakeDamage()
    {
        
    }
   
}

