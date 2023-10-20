using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangeEmemy : MonoBehaviour
{
    public EnemyRange myenemy;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public Animator aim;
    private float enemyHeathPoint;
    // Start is called before the first frame update
    void Start()
    {
        myenemy = new EnemyRange(transform, aim, bulletPrefab,bulletSpawnPoint);
        myenemy.rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myenemy.FixedUpdate();
        if (enemyHeathPoint < 0)
        {
            Destroy(gameObject);
        }

    }

    private void Attack()
    {
        

    }


}
