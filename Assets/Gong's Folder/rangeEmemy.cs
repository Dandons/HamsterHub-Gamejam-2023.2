using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangeEmemy : MonoBehaviour
{
    public EnemyRange myenemy;
    // Start is called before the first frame update
    void Start()
    {
        myenemy = new EnemyRange(transform);
        myenemy.rb = this.GetComponent<Rigidbody2D>();
        myenemy.FixedUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        myenemy.FixedUpdate();
    }
}
