using UnityEngine;

public class ProjectileSpawner_Keyboard : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawner;
    public float shotSpeed = 20f;
    public float cooldown = 1f;

    private bool canShoot = true;

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
            rb.velocity = shootDirection * shotSpeed;
        }
        else
        {
            Debug.LogWarning("Rigidbody2D component not found on projectile.");
        }

        // Starte das Cooldown
        Invoke("ResetCanShoot", cooldown);
    }

    private void ResetCanShoot()
    {
        canShoot = true;
    }
}
