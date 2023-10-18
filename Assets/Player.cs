using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerProperty playerProperty;

    private void Start()
    {
        playerProperty = new PlayerProperty();
    }
}
