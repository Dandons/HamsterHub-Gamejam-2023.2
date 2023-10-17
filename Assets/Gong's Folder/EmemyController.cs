using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyController : MonoBehaviour
{
    public Enemy myenemy;
    // Start is called before the first frame update
    void Start()
    {
        myenemy = new Enemy(transform);
        myenemy.rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        myenemy.FixedUpdate();
    }
    
   
  

}
