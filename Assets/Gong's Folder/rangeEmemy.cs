using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangeEmemy : MonoBehaviour
{
    public EnemyRange myenemy;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    protected float shootForce = 3f;
    // Start is called before the first frame update
    void Start()
    {
        myenemy = new EnemyRange(transform, bulletPrefab,bulletSpawnPoint);
        myenemy.rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myenemy.FixedUpdate();

    }

    private void Attack()
    {
        

    }


}
