using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    public Playerstats playerStats;
    public int damage = 1; // amount of damage that the bullet deals
    public int maxWallBounces = 3;

    public bool elasticWalls = false;
    public bool PiercingBullets = false;
    public bool test = false;

    private int wallBounces = 0;
    private Rigidbody2D rb;

    Vector3 lastVelocity;

    public GameObject particlePrefab; // Reference to the particle system prefab


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Playerstats>();
    }
    private void Start()
    {
        Invoke("DestroyObject", 10f);
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
                    enemy.TakeDamage(playerStats.bulletDamage); // call the TakeDamage function of the enemy script to deal damage to the enemy
                }
                Destroy(gameObject); // destroy the bullet object
                //Debug.Log("PB" + PiercingBullets);
            }
            if (PiercingBullets == true)
            {
                EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.TakeDamage(playerStats.bulletDamage); // call the TakeDamage function of the enemy script to deal damage to the enemy
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

        // Spawn particles
        SpawnParticles(collision.contacts[0].point, collision.contacts[0].normal, collision);
    }

    void SpawnParticles(Vector3 position, Vector3 normal, Collision2D collision)
    {
        if (particlePrefab != null)
        {
            Quaternion rotation = Quaternion.LookRotation(normal);

            if (collision.gameObject.CompareTag("Enemy"))
            {
                rotation = Quaternion.LookRotation(-normal);
            }

            GameObject particles = Instantiate(particlePrefab, position, rotation);
            Destroy(particles, 2f); // Destroy particles after 2 seconds (adjust the duration as needed)
        }
    }

    void DestroyObject()
    {
        // Zerstöre dieses GameObject
        Destroy(gameObject);
    }

}
