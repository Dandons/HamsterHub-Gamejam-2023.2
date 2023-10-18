using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        // Handle collision with other objects (e.g., enemies)
        if (other.CompareTag("Companion"))
        {
            // You can add logic here for what happens when the bullet hits an enemy
            // For example, reduce the enemy's health or score points.

            Destroy(gameObject);// Destroy the bullet

        }

    }
}
