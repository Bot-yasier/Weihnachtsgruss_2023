using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public ProjectileSpawnerMouse ProjectileSpawnerMouse;
    public bool FlipToEnemy = false;
    public UpgradeHandler upgradeHandler;

    private Transform nullObject;
    private Vector3 previousPosition;
    private float velocityThreshold = 0.1f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // Find the parent null object by name
        nullObject = transform.parent.Find("Player");
        previousPosition = nullObject.position;
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject closestEnemy = FindClosestEnemy();

        if (FlipToEnemy)
        {
            if (closestEnemy != null)
            {
                Vector3 enemyDirection = closestEnemy.transform.position - nullObject.position;

                if (enemyDirection.x > 0)
                {
                    spriteRenderer.flipX = true;
                }
                else
                {
                    spriteRenderer.flipX = false;
                }
            }
        }
        else
        {
            if (mousePosition.x > nullObject.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }

        // Calculate the velocity based on the null object's movement
        float velocity = Vector3.Distance(previousPosition, nullObject.position) / Time.deltaTime;
        previousPosition = nullObject.position;

        // Set the value of the "Velocity" parameter in the animator based on the calculated velocity
        //animator.SetFloat("Velocity", velocity);

        // Check if the velocity is below the threshold to play the standing animation
        if (velocity < velocityThreshold)
        {
            // Play the standing animation
            // animator.Play("Standing Animation Name");
        }
        else
        {
            // Play the walking animation
            // animator.Play("Walking Animation Name");
        }
    }

    public void CallShootMethod()
    {
        FlipToEnemy = true;
        ProjectileSpawnerMouse.Shoot();

        StartCoroutine(wait());

    }

    public void FlipToMouse()
    {
        FlipToEnemy = false;
    }

    private GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        Vector3 playerPosition = nullObject.position;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(playerPosition, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }


    private IEnumerator wait()
    {
        if(upgradeHandler.NumBullets == 1)
        {
            yield return new WaitForSeconds(0.05f);
            FlipToEnemy = true;
            ProjectileSpawnerMouse.Shoot();
        }
        if (upgradeHandler.NumBullets == 2)
        {
            yield return new WaitForSeconds(0.05f);
            FlipToEnemy = true;
            ProjectileSpawnerMouse.Shoot();
            yield return new WaitForSeconds(0.05f);
            FlipToEnemy = true;
            ProjectileSpawnerMouse.Shoot();
        }

        if (upgradeHandler.NumBullets == 3)
        {
            yield return new WaitForSeconds(0.05f);
            FlipToEnemy = true;
            ProjectileSpawnerMouse.Shoot();
            yield return new WaitForSeconds(0.05f);
            FlipToEnemy = true;
            ProjectileSpawnerMouse.Shoot();
            yield return new WaitForSeconds(0.05f);
            FlipToEnemy = true;
            ProjectileSpawnerMouse.Shoot();
        }
        if (upgradeHandler.NumBullets == 4)
        {
            yield return new WaitForSeconds(0.05f);
            FlipToEnemy = true;
            ProjectileSpawnerMouse.Shoot();
            yield return new WaitForSeconds(0.05f);
            FlipToEnemy = true;
            ProjectileSpawnerMouse.Shoot();
            yield return new WaitForSeconds(0.05f);
            FlipToEnemy = true;
            ProjectileSpawnerMouse.Shoot();
            yield return new WaitForSeconds(0.05f);
            FlipToEnemy = true;
            ProjectileSpawnerMouse.Shoot();
        }
        if (upgradeHandler.NumBullets == 5)
        {
            yield return new WaitForSeconds(0.05f);
            FlipToEnemy = true;
            ProjectileSpawnerMouse.Shoot();
            yield return new WaitForSeconds(0.05f);
            FlipToEnemy = true;
            ProjectileSpawnerMouse.Shoot();
            yield return new WaitForSeconds(0.05f);
            FlipToEnemy = true;
            ProjectileSpawnerMouse.Shoot();
            yield return new WaitForSeconds(0.05f);
            FlipToEnemy = true;
            ProjectileSpawnerMouse.Shoot();
            yield return new WaitForSeconds(0.05f);
            FlipToEnemy = true;
            ProjectileSpawnerMouse.Shoot();
        }
    }
}
