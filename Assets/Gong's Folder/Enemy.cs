using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy 
{
    protected float enemyHeathPoint;
    protected float attackDamage;
    protected float allDamge;
    protected float speed = 10f;
    protected float defend;
    protected float detectRange = 100f;
    protected float attackRange; 
    protected Vector2 direction; 
    protected Transform transform;
    protected Transform target;
    public Rigidbody2D rb;
    private Animator aim;
    private float DelayTime = 5f;
    private float lastShotTime = 5f;


    public Enemy(Transform enemyTransform, Animator aim)
    {
        transform = enemyTransform;
        this.aim = aim;
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
                if (target != null)
                {
                    float distanceToTarget = Vector2.Distance(transform.position, target.position);

                    if (distanceToTarget <= attackRange)
                    {
                        rb.velocity =  Vector2.zero * speed * Time.deltaTime;
                        if (Time.time - lastShotTime >= DelayTime)
                        {
                            Attack();

                            aim.SetTrigger("attacking");
                            lastShotTime = Time.time;
                        }
                    }
                    else
                    {
                        target = collider.transform;
                        direction = (target.position - transform.position);
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

    public virtual void Attack()
    {
        Debug.Log("Attacking" + target.name);
    }
    public void takeDamge(float atkplayer)
    {
        if(defend >= atkplayer*0.9)
        {
            allDamge = atkplayer - defend;
            enemyHeathPoint -= allDamge;
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

    private float DelayTime = 5f;
    private float lastShotTime = 5f;
    public EnemyMelee(Transform enemyTransform, Animator aim) : base(enemyTransform,aim)
    {
        attackRange = 2f;
        enemyHeathPoint = 1 + 2 * (DayCount.days - 1);
        attackDamage = 1 + 1 * (DayCount.days - 1);
        defend = 1 + 2 * (DayCount.days - 1);
        
    }
    public override void Attack()
    {

        Debug.Log("meleeAttacking");

    }
   
}

    

public class EnemyRange : Enemy
{
    protected float shootForce = 1f;
    private GameObject bulletPrefab;
    private Transform bulletSpawnPoint;

    public EnemyRange(Transform enemyTransform, Animator aim, GameObject bulletPrefab, Transform bulletSpawnPoint) : base(enemyTransform,aim)
    {
        attackRange = 7f;
        enemyHeathPoint = 1 + 1 * (DayCount.days - 1);
        attackDamage = 1 + 2 * (DayCount.days - 1);
        defend = 1 + 1 * (DayCount.days - 1);
        this.bulletPrefab = bulletPrefab;
        this.bulletSpawnPoint = bulletSpawnPoint;
    }
    public override void Attack()
    {
        
       Debug.Log("RangeAttacking");

        GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        // Add force to the bullet to make it move forward
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = direction * shootForce;
            Debug.Log("1234");
        }
    }

        // Start is called before the first frame update
        void Start()
    {

    }

    // Update is called once per
    private void Update()
    {
    }

}

