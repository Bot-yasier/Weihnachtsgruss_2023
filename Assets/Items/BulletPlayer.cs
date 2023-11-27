using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    public Playerstats playerStats;

    public UpgradeHandler upgradeHandler;
    public float damage = 1; // amount of damage that the bullet deals
    public int maxWallBounces = 3;

    public Transform bulletPlayer;

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

      

        upgradeHandler = GameObject.FindGameObjectWithTag("ModifireManager").GetComponent<UpgradeHandler>();
        if (upgradeHandler.BigShot == 1)
        {

        }
        if(upgradeHandler.BigShot == 2)
        {
            Vector3 currentSize = bulletPlayer.transform.localScale;
            bulletPlayer.transform.localScale = new Vector3(currentSize.x + 0.25f, currentSize.y + 0.25f, currentSize.z + 0.25f);
        }
        if (upgradeHandler.BigShot == 3)
        {
            Vector3 currentSize = bulletPlayer.transform.localScale;
            bulletPlayer.transform.localScale = new Vector3(currentSize.x + 0.3f, currentSize.y + 0.3f, currentSize.z + 0.3f);
        }
        if (upgradeHandler.BigShot == 4)
        {
            Vector3 currentSize = bulletPlayer.transform.localScale;
            bulletPlayer.transform.localScale = new Vector3(currentSize.x + 0.4f, currentSize.y + 0.4f, currentSize.z + 0.4f);
        }
        if (upgradeHandler.BigShot == 5)
        {
            Vector3 currentSize = bulletPlayer.transform.localScale;
            bulletPlayer.transform.localScale = new Vector3(currentSize.x + 0.5f, currentSize.y + 0.5f, currentSize.z + 0.5f);
        }
        if (upgradeHandler.BigShot == 6)
        {
            Vector3 currentSize = bulletPlayer.transform.localScale;
            bulletPlayer.transform.localScale = new Vector3(currentSize.x + 0.6f, currentSize.y + 0.6f, currentSize.z + 0.6f);
        }
        if (upgradeHandler.BigShot == 7)
        {
            Vector3 currentSize = bulletPlayer.transform.localScale;
            bulletPlayer.transform.localScale = new Vector3(currentSize.x + 0.7f, currentSize.y + 0.7f, currentSize.z + 0.7f);
        }
        if (upgradeHandler.BigShot == 8)
        {
            Vector3 currentSize = bulletPlayer.transform.localScale;
            bulletPlayer.transform.localScale = new Vector3(currentSize.x + 0.8f, currentSize.y + 0.8f, currentSize.z + 0.8f);
        }

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
