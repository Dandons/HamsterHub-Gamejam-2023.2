using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCompanionShop : MonoBehaviour
{
    public GameObject companionShop;
    private GameObject openedShop;
    public float detectRange;
    void Update()
    {
        Collider2D[] entity = Physics2D.OverlapCircleAll(transform.position, detectRange);
        foreach(Collider2D collider in entity)
        {
            if(collider.tag == "Player")
            {
                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    openedShop = Instantiate(companionShop);
                    //disable ative method delegate in player
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Destroy(openedShop);
                }
                
            }
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
