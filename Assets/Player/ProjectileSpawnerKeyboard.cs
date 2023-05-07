using UnityEngine;

public class ProjectileSpawnerKeyboard : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawner;
    public float bulletSpeed = 20f;
    public float cooldown = 1f;
    public float bulletSize = 0.5f;

    private bool canShoot = true;

    [SerializeField] private bool enableElasticWalls = false;
    [SerializeField] private bool enablePiercingBullets = false;
    [SerializeField] private bool enableTest = false;

    [SerializeField] private int addBounce = 3;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        canShoot = false;

        // Erstelle das Projektil am Spawner
        GameObject bullet = Instantiate(bulletPrefab, spawner.position, spawner.rotation);

        // Richte das Projektil auf die Maus aus
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector2 shootDirection = (mousePos - spawner.position).normalized;
        bullet.transform.right = shootDirection;

        // Setze die Geschwindigkeit des Projektils
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = shootDirection * bulletSpeed;
        }
        else
        {
            Debug.LogWarning("Rigidbody2D component not found on projectile.");
        }

        // Setze die Modifier des Projektils
        BulletPlayer bulletScript = bullet.GetComponent<BulletPlayer>();
        if (bulletScript != null)
        {
            bulletScript.elasticWalls = enableElasticWalls;
            bulletScript.PiercingBullets = enablePiercingBullets;
            bulletScript.test = enableTest;

            bulletScript.maxWallBounces += addBounce;
        }

        // Starte das Cooldown
        Invoke("ResetCanShoot", cooldown);
    }

    private void ResetCanShoot()
    {
        canShoot = true;
    }
}
