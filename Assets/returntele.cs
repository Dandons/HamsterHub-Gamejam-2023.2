using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returntele : MonoBehaviour
{
    public Transform destinationPoint2; // Assign this in the Inspector.
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (CompareTag("mirror"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player.Instance.reflection = false;
                TeleportToDestination2();
                Debug.Log(2);
            }
        }
    }
    void TeleportToDestination2()
    {
        // Teleport the player to the destination point.
        player.transform.position = destinationPoint2.position;
        Debug.Log(4);
    }

}
