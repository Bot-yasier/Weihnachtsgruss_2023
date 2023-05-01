using UnityEngine;

public class ProjectileSpawnerScript : MonoBehaviour
{
    public float shotSpeed = 20f;
    public float shotSize = 0.5f;
    public GameObject projectilePrefab;
    public GameObject spawner;
    public string enemyTag; // Tag, nach dem wir suchen

    public float maxShootDistance = 20f;

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
                GameObject projectile = Instantiate(projectilePrefab, spawner.transform.position, spawner.transform.rotation);
                projectile.transform.localScale = new Vector3(shotSize, shotSize, shotSize);

                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 shootDirection = (currentEnemyTransform.position - spawner.transform.position).normalized;
                    rb.velocity = shootDirection * shotSpeed;
                }
                else
                {
                    Debug.LogWarning("Rigidbody2D component not found on projectile.");
                }
            }
        }
    }
}
