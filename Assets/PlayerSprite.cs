using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public ProjectileSpawnerMouse ProjectileSpawnerMouse;

    void Start()
    {
        // Get the SpriteRenderer component attached to the player object
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Get the position of the mouse cursor in the world
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Check if the mouse cursor is on the right side of the player
        if (mousePosition.x > transform.position.x)
        {
            // Flip the sprite horizontally
            spriteRenderer.flipX = true;
        }
        else
        {
            // Reset the sprite's flipping
            spriteRenderer.flipX = false;
        }
    }

    public void CallShootMethod()
    {
        // Flip the sprite to face the selected enemy
        if (ProjectileSpawnerMouse.currentEnemyTransform != null)
        {
            Vector3 enemyPosition = ProjectileSpawnerMouse.currentEnemyTransform.position;
            if (enemyPosition.x > transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }

        // Call the Shoot method in ProjectileSpawnerMouse
        ProjectileSpawnerMouse.Shoot();
    }
}
