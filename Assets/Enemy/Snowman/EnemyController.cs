using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 3; // maximum health of the enemy
    public float moveSpeed = 3f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float maxRange = 5f;
    public float minRange = 2f;
    public float shootDuration = 0.5f;
    public float shootCooldown = 1.5f;

    private Transform player;
    private Vector2 moveDirection;
    private float timeUntilNextShot;
    public int currentHealth; // current health of the enemy

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        moveDirection = Vector2.zero;
        timeUntilNextShot = shootCooldown;
        currentHealth = maxHealth; // set the current health to the maximum health at the start
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity *= 0.5f;
    }

    void Update()
    {
        if (player == null) return;

        Vector2 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer > maxRange)
        {
            moveDirection = directionToPlayer.normalized;
        }
        else if (distanceToPlayer < minRange)
        {
            moveDirection = Vector2.zero;
        }

        transform.position += (Vector3)moveDirection * moveSpeed * Time.deltaTime;

        if (moveDirection == Vector2.zero)
        {
            timeUntilNextShot -= Time.deltaTime;
        }

        if (timeUntilNextShot <= 0)
        {
            StartCoroutine(ShootAndWait());
            timeUntilNextShot = shootCooldown;
        }
    }

    IEnumerator ShootAndWait()
    {
        moveDirection = Vector2.zero;

        for (int i = 0; i < 3; i++)
        {
            if (player == null || bulletPrefab == null) yield break;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            if (!bullet.TryGetComponent(out Rigidbody2D bulletRigidbody)) yield break;

            Vector2 directionToPlayer = player.position - transform.position;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            float deviationAngle = Random.Range(-10f, 10f);
            angle += deviationAngle;

            Vector2 bulletDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            bulletRigidbody.velocity = bulletDirection.normalized * bulletSpeed;

            yield return new WaitForSecondsRealtime(shootDuration);
        }

        yield return new WaitForSecondsRealtime(0.2f);

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(minRange, maxRange);

        moveDirection = randomDirection;
        timeUntilNextShot = shootCooldown;

        yield return new WaitForSecondsRealtime(randomDistance / moveSpeed);

        moveDirection = Vector2.zero;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // subtract the damage from the current health

        if (currentHealth <= 0)
        {
            Die(); // if the current health is zero or less, call the Die() function
        }
    }

    void Die()
    {
        // destroy the enemy object
        Destroy(gameObject);
    }
}
