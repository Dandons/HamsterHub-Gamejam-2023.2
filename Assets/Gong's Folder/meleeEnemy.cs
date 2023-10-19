using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeEnemy : MonoBehaviour
{

    public Animator aim;
    public EnemyMelee myenemy;
    // Start is called before the first frame update
    void Start()
    {
        myenemy = new EnemyMelee(transform,aim);
        myenemy.rb = this.GetComponent<Rigidbody2D>();
        myenemy.FixedUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        myenemy.FixedUpdate();
    }
   
}
