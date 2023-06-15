using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterenceCollision : MonoBehaviour
{
    public DoorBlockerHandler doorBlockerHandler;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            doorBlockerHandler.PlayerCollWithEnterence();
        }
    }
}
