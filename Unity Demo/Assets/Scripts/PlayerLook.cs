using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform player;
    private void Update()
    {
        transform.position = player.position;   //Set cam position
    }
}
