using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCompanionShop : MonoBehaviour
{
    public GameObject playerUI;
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
                    playerUI.SetActive(false);
                    openedShop = Instantiate(companionShop);
                    Player.Instance.DisableActiveMethod();
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    playerUI.SetActive(true);
                    Player.Instance.SetActiveMethod();
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
