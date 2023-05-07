using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    public int damage = 1; // amount of damage that the bullet deals
    public int maxWallBounces = 3;

    public bool elasticWalls = false;
    public bool PiercingBullets = false;
    public bool test = false;

    private int wallBounces = 0;
    private Rigidbody2D rb;

    Vector3 lastVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        lastVelocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(test);
        //Debug.Log(PiercingBullets);
        //Debug.Log(elasticWalls);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (PiercingBullets == false)
            {
                EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage); // call the TakeDamage function of the enemy script to deal damage to the enemy
                }
                Destroy(gameObject); // destroy the bullet object
                //Debug.Log("PB" + PiercingBullets);
            }
            if(PiercingBullets == true)
            {
                EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage); // call the TakeDamage function of the enemy script to deal damage to the enemy
                }
                //Debug.Log("PB" + PiercingBullets);
            }
            
        }

        if (elasticWalls == true)
        {
            if (wallBounces < maxWallBounces)
            {
                var speed = lastVelocity.magnitude;
                var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

                rb.velocity = direction * Mathf.Max(speed, 0f);

                wallBounces++;
            }
            else
            {
                Destroy(gameObject); // destroy the bullet object if it has bounced off walls maxWallBounces times
            }
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
