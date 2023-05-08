using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSnowman : MonoBehaviour
{
    public int damage = 1; // amount of damage that the bullet deals

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealthController player = collision.gameObject.GetComponent<PlayerHealthController>();
            if (player != null)
            {
                player.TakeDamage(damage); // call the TakeDamage function of the enemy script to deal damage to the enemy
            }
            Destroy(gameObject); // destroy the bullet object
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
