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
            other.GetComponent<Companion>().TakeDamage(100 + 2 * (DayCount.days - 1));
            Debug.Log(other.GetComponent<Companion>().companionProperty.currentHp);
            Destroy(gameObject);
        }
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(100 + 2 * (DayCount.days - 1));
            Destroy(gameObject);
        }
    }
}
