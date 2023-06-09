using UnityEngine;
using System.Collections;

public class PlayerMovementMouse : MonoBehaviour
{
    private Playerstats playerStats;
    public float stopDistance = 0.1f, dashDistance = 2f, dashDuration = 0.5f, dashCooldown = 2f;
    private Rigidbody2D rb;
    //public int speed;
    private Vector2 targetPosition, dashDirection;
    private bool isDashing, isMoving, isStanding;
    private float dashTimer; //dashCooldownTimer;
    private Vector2 prevPosition;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = FindObjectOfType<Playerstats>();
    }

    void Update()
    {
        if (!isDashing) targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && playerStats.dashCooldown <= 0f && isMoving) StartCoroutine(Dash());

        // Check if the player is standing
        isStanding = rb.velocity.magnitude <= 0f;
        animator.SetBool("isStanding", isStanding);
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            Vector2 movement = targetPosition - rb.position;
            rb.velocity = movement.magnitude < stopDistance ? Vector2.zero : movement.normalized * playerStats.movementSpeed;
        }
        else
        {
            rb.velocity = dashDirection * dashDistance / dashDuration;
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                isDashing = false;
                playerStats.dashCooldown = dashCooldown;
            }
        }

        // Check if the player is moving
        isMoving = (rb.position - prevPosition).magnitude > 0.01f;
        prevPosition = rb.position;

        rb.freezeRotation = true;
        rb.velocity *= 0.5f;
        playerStats.dashCooldown -= playerStats.dashCooldown > 0f ? Time.deltaTime : 0f;
    }

    IEnumerator Dash()
    {
        // Start the dash
        isDashing = true;
        dashTimer = dashDuration;
        dashDirection = rb.velocity.normalized;

        // Disable collision detection during the dash
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;

        // Wait for the dash to finish
        yield return new WaitForSeconds(dashDuration);

        // Enable collision detection after the dash
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        // Finish the dash
        isDashing = false;
        playerStats.dashCooldown = dashCooldown;
    }
}
