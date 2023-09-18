using UnityEngine;
using System.Collections;
using Pathfinding;

public class EnemyController : MonoBehaviour
{

    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private AIDestinationSetter aiDestinationSetter;
    private bool isMoving = false;
    private bool isShooting = false;
    private float animationDuration = 1.0f;
    public float shootDelay = 2.0f;
    public LevelCounter levelCounter;

    public delegate void EnemyDeathEventHandler(EnemyController enemy);
    public static event EnemyDeathEventHandler EnemyDeathEvent;

    public int maxHealth;
    public float moveSpeed = 3f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float maxRange = 5f;
    public float minRange = 2f;
    public float shootDuration = 0.5f;
    public float shootCooldown = 1.5f;
    public UpgradeHandler upgradeHandler;

    private Transform player;
    //private Vector2 moveDirection;
    private Vector2 randomPosition;
    private float timeUntilNextShot;
    public int currentHealth; // current health of the enemy
    private bool hasCollided = false;



    void Start()
    {
        levelCounter = FindObjectOfType<LevelCounter>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        //moveDirection = Vector2.zero;
        timeUntilNextShot = shootCooldown;
        currentHealth = maxHealth;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity *= 0.5f;
        aiDestinationSetter = GetComponent<AIDestinationSetter>();



        //StartCoroutine(EnemyLoop()); // Start the MoveAndWait coroutine
        if(levelCounter.Levelint == 1)
        {
            maxHealth = 5;
        }
        if (levelCounter.Levelint == 2)
        {
            maxHealth = 6;
        }
        if (levelCounter.Levelint == 3)
        {
            maxHealth = 8;
        }
        if (levelCounter.Levelint == 4)
        {
            maxHealth = 10;
        }
        if (levelCounter.Levelint == 5)
        {
            maxHealth = 13;
        }
    }


    public IEnumerator EnemyLoop()
    {

        // Trigger the "shoot" animation
        animator.SetTrigger("shoot");

        int count = 0;
        while (count < 3)
        {

            animator.SetTrigger("shoot");
            count++;
        }

        // Wait for a certain duration before shooting
        yield return new WaitForSeconds(shootDelay);
        Debug.Log("shootDelay");
        animator.SetTrigger("stopshoot");
        isShooting = false;


        yield return null; // Optional: Wait for one frame to allow the animation to start playing

    }





    void Update()
    {
        if (player == null) return;

        // Get the AIDestinationSetter script component
        if (aiDestinationSetter == null)
        {
            aiDestinationSetter = GetComponent<AIDestinationSetter>();
        }

        // Get the move direction and current target position from AIDestinationSetter
        Vector2 moveDirection = aiDestinationSetter.MoveDirection;
        Vector2 currentTargetPosition = aiDestinationSetter.CurrentTargetPosition;

        // Check if the AI is moving
        isMoving = moveDirection.magnitude > 0 && Vector2.Distance(transform.position, currentTargetPosition) > 0.1f;

        // Update animation parameters
        Vector2 playerToEnemy = player.position - transform.position;
        animator.SetFloat("MoveX", currentTargetPosition.x - transform.position.x);
        animator.SetFloat("MoveY", currentTargetPosition.y - transform.position.y);
        animator.SetFloat("PlayerX", playerToEnemy.x);
        animator.SetFloat("PlayerY", playerToEnemy.y);
        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsShooting", isShooting);

        // Flip the sprite based on movement direction
        if (isMoving)
        {
            if (moveDirection.x < 0)
                spriteRenderer.flipX = false;
            else if (moveDirection.x > 0)
                spriteRenderer.flipX = true;
        }


        if (isShooting)
        {
            if (playerToEnemy.x < 0)
                spriteRenderer.flipX = false;
            else if (playerToEnemy.x > 0)
                spriteRenderer.flipX = true;
        }

        /*
        if (hasReachedRandomPosition && isMoving)
        {
            // Check for collisions while moving
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
            if (colliders.Length > 1) // Check if there are any colliders other than the enemy itself
            {
                // Collision detected
                StopAllCoroutines(); // Stop the current movement

                // Trigger a new movement by starting the MoveAndWait coroutine again
                StartCoroutine(MoveAndWait());
            }
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasCollided = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyMarker"))
        {
            StartCoroutine(EnemyLoop());
            isMoving = false;
            isShooting = true;
        }
    }



    private bool hasReachedRandomPosition = false; // Add this variable to track the reached position

    /*
    IEnumerator MoveAndWait()
    {
        isMoving = true;

        // Find all objects with the "EnemyPath" tag
        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("EnemyPath");

        if (groundObjects.Length > 0)
        {
            // Get a random ground object
            GameObject randomGround = groundObjects[Random.Range(0, groundObjects.Length)];

            // Get the collider of the ground object
            Collider2D groundCollider = randomGround.GetComponent<Collider2D>();

            if (groundCollider != null)
            {
                // Calculate a random position within the ground object's bounds
                Vector3 randomPosition = new Vector3(
                    Random.Range(groundCollider.bounds.min.x, groundCollider.bounds.max.x),
                    Random.Range(groundCollider.bounds.min.y, groundCollider.bounds.max.y),
                    transform.position.z
                );

                // Log the random position
                Debug.Log("Random Position: " + randomPosition);

                // Calculate direction towards the random spot
                moveDirection = ((Vector2)randomPosition - (Vector2)transform.position).normalized;

                // Move towards the random spot
                bool reachedDestination = false;
                while (!reachedDestination && !hasCollided)
                {
                    transform.position += (Vector3)moveDirection * moveSpeed * Time.deltaTime;

                    // Clamp the position within the ground object's bounds
                    float clampedX = Mathf.Clamp(transform.position.x, groundCollider.bounds.min.x, groundCollider.bounds.max.x);
                    float clampedY = Mathf.Clamp(transform.position.y, groundCollider.bounds.min.y, groundCollider.bounds.max.y);
                    transform.position = new Vector3(clampedX, clampedY, transform.position.z);

                    if (Vector2.Distance(transform.position, randomPosition) <= 0.1f)
                    {
                        reachedDestination = true;
                    }

                    yield return null;
                }

                isMoving = false; // Update the isMoving flag after reaching the destination

                if (hasCollided)
                {
                    hasCollided = false; // Reset the collision flag
                    StartCoroutine(MoveAndWait()); // Start a new MoveAndWait coroutine
                }
            }
            else
            {
                Debug.LogError("No Collider2D component found on the ground object");
            }
        }
        else
        {
            Debug.LogError("No objects found with the 'EnemyPath' tag");
        }
    }
    */


    public void Shoot()
    {
        Debug.Log("Shoot");


        if (player == null || bulletPrefab == null)
        {
            Debug.Log("Player or bulletPrefab is null. Cannot shoot.");
            return;
        }

        Debug.Log("Shooting...");

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        if (!bullet.TryGetComponent(out Rigidbody2D bulletRigidbody))
        {
            Debug.Log("Bullet does not have Rigidbody2D component. Cannot shoot.");
            return;
        }

        Vector2 directionToPlayer = player.position - transform.position;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        float deviationAngle = Random.Range(-10f, 10f);
        angle += deviationAngle;

        Vector2 bulletDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        bulletRigidbody.velocity = bulletDirection.normalized * bulletSpeed;

        Debug.Log("Shoot complete.");

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
        // Raise the enemy death event
        if (EnemyDeathEvent != null)
        {
            EnemyDeathEvent(this);
        }

        Destroy(gameObject);
    }
}
