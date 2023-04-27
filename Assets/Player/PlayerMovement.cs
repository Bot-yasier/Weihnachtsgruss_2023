using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 8f;
    [SerializeField] private float dashDistance = 2f;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private float directionChangeSpeed = 10f;

    private Rigidbody2D rb;
    private Vector2 currentDirection;
    private bool isDashing;
    private Coroutine currentDashCoroutine;
    private float dashEndTime;
    private Vector2 dashDirection;
    private float lastDashTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentDirection = Vector2.up;
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            rb.velocity = dashDirection * (dashDistance / dashDuration);
            float dashPercentage = (Time.time - dashEndTime) / dashDuration;
            if (dashPercentage > 1f)
            {
                isDashing = false;
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
            {
                currentDirection = Vector2.Lerp(currentDirection, new Vector2(horizontalInput, verticalInput).normalized, directionChangeSpeed * Time.fixedDeltaTime);
                rb.velocity = currentDirection * movementSpeed;
            }
            else
            {
                rb.velocity = Vector2.zero;
                currentDirection = rb.velocity;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + dashCooldown)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if ((Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f))
            {
                dashDirection = new Vector2(horizontalInput, verticalInput).normalized;
            }
            else
            {
                dashDirection = currentDirection.normalized;
            }

            isDashing = true;
            dashEndTime = Time.time + dashDuration;
            lastDashTime = Time.time;
        }

        if (isDashing)
        {
            rb.velocity = dashDirection * (dashDistance / dashDuration);
            float dashPercentage = (Time.time - dashEndTime) / dashDuration;
            if (dashPercentage > 1f)
            {
                isDashing = false;
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
            {
                currentDirection = Vector2.Lerp(currentDirection, new Vector2(horizontalInput, verticalInput).normalized, directionChangeSpeed * Time.fixedDeltaTime);
                rb.velocity = currentDirection * movementSpeed;
            }
            else
            {
                rb.velocity = Vector2.zero;
                currentDirection = rb.velocity;
            }
        }
    }
}
