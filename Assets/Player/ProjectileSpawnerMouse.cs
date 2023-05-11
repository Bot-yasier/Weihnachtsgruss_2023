using UnityEngine;

public class ProjectileSpawnerMouse : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject spawner;
    public float bulletSpeed = 20f;
    public float bulletSize = 0.5f;
    public string enemyTag; // Tag, nach dem wir suchen
    public int numBullets = 1;

    public float maxShootDistance = 20f;

    [SerializeField] public bool enableElasticWalls = false;
    [SerializeField] public bool enablePiercingBullets = false;
    [SerializeField] public bool enableTest = false;

    [SerializeField] private int addBounce = 3;

    private GameObject currentEnemy; // Derzeitiges Zielobjekt
    private Transform currentEnemyTransform; // Transform des derzeitigen Zielobjekts

    private void Start()
    {
        if (spawner == null)
        {
            spawner = gameObject;
        }

        InvokeRepeating("FindEnemies", 0f, 1f); // Methode, um Feinde zu finden
        InvokeRepeating("Shoot", 0f, 1f);
    }

    private void FindEnemies()
    {
        // Finde alle Feinde mit dem gegebenen Tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        // Wähle den nächsten Feind als Ziel aus
        if (enemies.Length > 0)
        {
            float closestDistance = Mathf.Infinity;

            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(spawner.transform.position, enemy.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    currentEnemy = enemy;
                    currentEnemyTransform = enemy.transform;
                }
            }
        }
        else
        {
            currentEnemy = null;
        }
    }

    private void Shoot()
    {
        if (currentEnemy != null)
        {
            float distanceToEnemy = Vector3.Distance(spawner.transform.position, currentEnemyTransform.position);

            if (distanceToEnemy <= maxShootDistance)
            {
                // Calculate shoot direction
                Vector2 shootDirection = (currentEnemyTransform.position - spawner.transform.position).normalized;

                // Calculate additional offset for even number of bullets
                float additionalOffset = 0f;
                if (numBullets % 2 == 0)
                {
                    additionalOffset = (numBullets - 1) * 10f / 2f;
                }

                for (int i = 0; i < numBullets; i++)
                {
                    // Calculate offset angle for this bullet
                    float offsetAngle = (i - (numBullets - 1) / 2f) * 10f + additionalOffset / 2f;

                    // Calculate offset vector
                    Vector2 offset = Quaternion.Euler(0f, 0f, offsetAngle) * shootDirection;

                    // Calculate position of bullet
                    Vector3 bulletPos = spawner.transform.position + new Vector3(offset.x, offset.y, 0f) * 0.5f;

                    // Spawn bullet and set its properties
                    GameObject bullet = Instantiate(bulletPrefab, bulletPos, Quaternion.identity);
                    bullet.transform.right = offset;
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.velocity = offset * bulletSpeed;
                    }
                    else
                    {
                        Debug.LogWarning("Rigidbody2D component not found on projectile.");
                    }

                    BulletPlayer bulletScript = bullet.GetComponent<BulletPlayer>();
                    if (bulletScript != null)
                    {
                        bulletScript.elasticWalls = enableElasticWalls;
                        bulletScript.PiercingBullets = enablePiercingBullets;
                        bulletScript.test = enableTest;

                        bulletScript.maxWallBounces += addBounce;
                    }
                }
            }
        }
    }


}
