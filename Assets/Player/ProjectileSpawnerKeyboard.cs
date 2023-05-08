using UnityEngine;

public class ProjectileSpawnerKeyboard : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawner;
    public float bulletSpeed = 20f;
    public float cooldown = 1f;
    public float bulletSize = 0.5f;
    public int numBullets = 1;
    public int mulitshot = 1;

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

        // Calculate shoot direction
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector2 shootDirection = (mousePos - spawner.position).normalized;

        for (int j = 0; j < mulitshot; j++)
        {
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
                Vector3 bulletPos = spawner.position + new Vector3(offset.x, offset.y, 0f) * 0.5f;

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

        // Start cooldown
        Invoke("ResetCanShoot", cooldown);
    }


    private void ResetCanShoot()
    {
        canShoot = true;
    }
}
