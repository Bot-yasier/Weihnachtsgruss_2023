using UnityEngine;
using System.Collections;

public class PlayerMovementMouse : MonoBehaviour
{
    public float stopDistance = 0.1f, dashDistance = 2f, dashDuration = 0.5f, dashCooldown = 2f;
    private Rigidbody2D rb;
    public int speed;
    private Vector2 targetPosition, dashDirection;
    private bool isDashing, isMoving;
    private float dashTimer, dashCooldownTimer;
    private Vector2 prevPosition;

    void Start() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        if (!isDashing) targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && dashCooldownTimer <= 0f && isMoving) StartCoroutine(Dash());
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            Vector2 movement = targetPosition - rb.position;
            rb.velocity = movement.magnitude < stopDistance ? Vector2.zero : movement.normalized * speed;
        }
        else
        {
            rb.velocity = dashDirection * dashDistance / dashDuration;
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                isDashing = false;
                dashCooldownTimer = dashCooldown;
            }
        }

        // Check if the player is moving
        isMoving = (rb.position - prevPosition).magnitude > 0.01f;
        prevPosition = rb.position;

        rb.freezeRotation = true;
        rb.velocity *= 0.5f;
        dashCooldownTimer -= dashCooldownTimer > 0f ? Time.deltaTime : 0f;
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
        dashCooldownTimer = dashCooldown;
    }
}
