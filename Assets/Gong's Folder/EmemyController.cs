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
    }

    // Update is called once per frame
    void Update()
    {
        myenemy.Update();
    }

  

}
