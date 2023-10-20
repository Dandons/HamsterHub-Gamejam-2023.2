using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Transform destinationPoint1; // Assign this in the Inspector.
    public GameObject player;

    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (CompareTag("mirror"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player.Instance.reflection = true;
                TeleportToDestination1();
                Debug.Log(1);
            }

        }
    }

    void TeleportToDestination1()
    {
        // Teleport the player to the destination point.
        player.transform.position = destinationPoint1.position;
        Debug.Log(3);
    }
}
