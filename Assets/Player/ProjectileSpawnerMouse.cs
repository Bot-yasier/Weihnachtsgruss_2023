using UnityEngine;

public class ProjectileSpawnerMouse : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject spawner;
    public float bulletSpeed = 20f;
    public float bulletSize = 0.5f;
    public string enemyTag; // Tag, nach dem wir suchen

    public float maxShootDistance = 20f;

    [SerializeField] private bool enableElasticWalls = false;
    [SerializeField] private bool enablePiercingBullets = false;
    [SerializeField] private bool enableTest = false;

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
                // Erstelle das Projektil am Spawner
                GameObject projectile = Instantiate(bulletPrefab, spawner.transform.position, spawner.transform.rotation);
                projectile.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);

                // Richte das Projektil auf den Gegner aus
                Vector2 shootDirection = (currentEnemyTransform.position - spawner.transform.position).normalized;
                projectile.transform.right = shootDirection;

                // Setze die Geschwindigkeit des Projektils
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = shootDirection * bulletSpeed;
                }
                else
                {
                    Debug.LogWarning("Rigidbody2D component not found on projectile.");
                }

                // Setze die Modifier des Projektils
                BulletPlayer bulletScript = projectile.GetComponent<BulletPlayer>();
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
