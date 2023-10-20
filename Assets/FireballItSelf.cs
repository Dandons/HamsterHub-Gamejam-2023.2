using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballItSelf : MonoBehaviour
{
    public float atkRange;

    [HideInInspector]public float damage;
    void Update()
    {
        Collider2D[] entity = Physics2D.OverlapCircleAll(this.transform.position, atkRange);
        foreach(Collider2D collider in entity)
        {
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<Enemy>().takeDamge(damage);
            }
        }
    }
    private void OnDestroy()
    {
        Collider2D[] entity = Physics2D.OverlapCircleAll(this.transform.position, atkRange+2);
        foreach (Collider2D collider in entity)
        {
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<Enemy>().takeDamge(damage*0.3f);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, atkRange);
    }
}
